using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace CaterHub
{
    public partial class Menu : UserControl
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-J7PRN5P\SQLEXPRESS;Initial Catalog=CaterHub;Integrated Security=True;Encrypt=False");

        public Menu()
        {
            InitializeComponent();
        }

        void isValid()
        {
            if (nameTxt.Text == string.Empty)
            {
                nameError.Visibility = Visibility.Visible;
            }
            else if (priceTxt.Text == string.Empty)
            {
                priceError.Visibility = Visibility.Visible;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.Open();

                // Fill Chinese Menu DataGrid
                FillDataGrid("Chinese_Menu", chineseDataGrid);

                // Fill Indian Menu DataGrid
                FillDataGrid("Indian_Menu", indianDataGrid);

                // Fill Snacks Menu DataGrid
                FillDataGrid("Snacks_Menu", snacksDataGrid);

                // Fill Desserts Menu DataGrid
                FillDataGrid("Dessert_Menu", dessertsDataGrid);

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FillDataGrid(string tableName, DataGrid dataGrid)
        {
            SqlCommand cmd = new SqlCommand($"SELECT menuName, price FROM {tableName}", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable(tableName);
            da.Fill(dataTable);

            var menuItems = new List<dynamic>();

            foreach (DataRow row in dataTable.Rows)
            {
                menuItems.Add(new
                {
                    Name = row[0],
                    Price = row[1],
                });
            }

            dataGrid.ItemsSource = menuItems;
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            isValid();
            try
            {
                string tableName = categoryCombo.Text + "_Menu";

                conn.Open();

                string query = $"INSERT INTO {tableName} (menuName, price) VALUES (@menuName, @price)";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@menuName", nameTxt.Text);
                command.Parameters.AddWithValue("@price", priceTxt.Text);
                command.ExecuteNonQuery();

                MessageBox.Show("Item saved successfully!");

                conn.Close();

                // Refresh the DataGrid
                UserControl_Loaded(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (nameTxt.Text == string.Empty || priceTxt.Text == string.Empty)
            {
                MessageBox.Show("Please select an item to update.");
            }
            else
            {
                try
                {
                    // Determine which table to update based on the selected category
                    string tableName = categoryCombo.Text + "_Menu";

                    // Get the selected item's original name to identify the correct row to update
                    string originalName = nameTxt.Tag?.ToString();

                    if (originalName == null)
                    {
                        MessageBox.Show("Please select an item to update.");
                        return;
                    }

                    conn.Open();

                    // Update the values in the database
                    string query = $"UPDATE {tableName} SET menuName = @menuName, price = @price WHERE menuName = @originalName";
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@menuName", nameTxt.Text);
                    command.Parameters.AddWithValue("@price", priceTxt.Text);
                    command.Parameters.AddWithValue("@originalName", originalName);
                    command.ExecuteNonQuery();

                    MessageBox.Show("Item updated successfully!");

                    conn.Close();

                    // Refresh the DataGrid
                    UserControl_Loaded(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void chineseDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (chineseDataGrid.SelectedItem != null)
            {
                dynamic selectedItem = chineseDataGrid.SelectedItem;
                nameTxt.Text = selectedItem.Name;
                priceTxt.Text = selectedItem.Price.ToString();
                nameTxt.Tag = selectedItem.Name; // Store the original name in the Tag property

            }
        }

        private void indianDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (indianDataGrid.SelectedItem != null)
            {
                dynamic selectedItem = indianDataGrid.SelectedItem;
                nameTxt.Text = selectedItem.Name;
                priceTxt.Text = selectedItem.Price.ToString();
                nameTxt.Tag = selectedItem.Name; // Store the original name in the Tag property
                categoryCombo.Text = "Indian";

            }
        }

        private void snacksDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (snacksDataGrid.SelectedItem != null)
            {
                dynamic selectedItem = snacksDataGrid.SelectedItem;
                nameTxt.Text = selectedItem.Name;
                priceTxt.Text = selectedItem.Price.ToString();
                nameTxt.Tag = selectedItem.Name; // Store the original name in the Tag property
                categoryCombo.Text = "Snacks";
            }
        }

        private void dessertsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dessertsDataGrid.SelectedItem != null)
            {
                dynamic selectedItem = dessertsDataGrid.SelectedItem;
                nameTxt.Text = selectedItem.Name;
                priceTxt.Text = selectedItem.Price.ToString();
                nameTxt.Tag = selectedItem.Name; // Store the original name in the Tag property
                categoryCombo.Text = "Desserts";

            }
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (nameTxt.Text == string.Empty)
            {
                MessageBox.Show("Please select an item to delete.");
                return;
            }

            try
            {
                // Determine which table to delete from based on the selected category
                string tableName = categoryCombo.Text + "_Menu";

                // Get the selected item's original name to identify the correct row to delete
                string originalName = nameTxt.Tag?.ToString();

                if (originalName == null)
                {
                    MessageBox.Show("Please select an item to delete.");
                    return;
                }

                conn.Open();

                // Delete the row from the database
                string query = $"DELETE FROM {tableName} WHERE menuName = @originalName";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@originalName", originalName);
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Item deleted successfully!");
                    nameTxt.Text = "";
                    priceTxt.Text = "";
                }
                else
                {
                    MessageBox.Show("Item not found in the database.");
                }

                conn.Close();

                // Refresh the DataGrid
                UserControl_Loaded(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        }
}
