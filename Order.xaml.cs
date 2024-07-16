using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CaterHub
{
    /// <summary>
    /// Interaction logic for Order.xaml
    /// </summary>
    public partial class Order : UserControl
    {
        public Order()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-J7PRN5P\SQLEXPRESS;Initial Catalog=CaterHub;Integrated Security=True;Encrypt=False");

        private void FillDataGrid(string tableName, DataGrid dataGrid)
        {
            SqlCommand cmd = new SqlCommand("SELECT firstName, lastName FROM Customers", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable(tableName);
            da.Fill(dataTable);

            var customers = new List<dynamic>();

            foreach (DataRow row in dataTable.Rows)
            {
                customers.Add(new
                {
                    FirstName = row[0],
                    LastName = row[1],
                });
            }

            customerDataGrid.ItemsSource = customers;
        }

        private void FillMenuDataGrid(DataGrid dataGrid)
        {
            //All four tables in one datagrid
            string[] tableNames = { "Snacks_Menu", "Indian_Menu", "Chinese_Menu", "Dessert_Menu" };

            DataTable combinedTable = new DataTable();
            // Ensure combinedTable has the necessary columns
            combinedTable.Columns.Add("menuName", typeof(string));
            combinedTable.Columns.Add("price", typeof(decimal));

            foreach (var tableName in tableNames)
            {
                SqlCommand cmd = new SqlCommand($"SELECT menuName, price FROM {tableName}", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                da.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    combinedTable.ImportRow(row);
                }
            }

            var menuItems = new List<dynamic>();

            foreach (DataRow row in combinedTable.Rows)
            {
                menuItems.Add(new
                {
                    Item = row["menuName"],
                    Price = row["price"],
                });
            }

            menuDataGrid.ItemsSource = menuItems;
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.Open();

                // Fill Customers DataGrid
                FillDataGrid("Customers", customerDataGrid);

                // Fill Menu DataGrid with multiple tables
                FillMenuDataGrid(menuDataGrid);

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
