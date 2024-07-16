using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CaterHub
{
    /// <summary>
    /// Interaction logic for User.xaml
    /// </summary>
    public partial class User : UserControl
    {
        public User()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-J7PRN5P\SQLEXPRESS;Initial Catalog=CaterHub;Integrated Security=True;Encrypt=False");
        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (firstnameTxt.Text == string.Empty || lastnameTxt.Text == string.Empty || emailTxt.Text == string.Empty || phoneTxt.Text == string.Empty || eventType.Text == string.Empty || guestCountTxt.Text == string.Empty || date.Text == string.Empty || time.Text == string.Empty)
                {
                    MessageBox.Show("Please fill all the details");
                }
                else
                {
                    conn.Open();
                    //Enter the values into DB
                    string query = "INSERT INTO Customers  (firstName, lastName, email, phoneNo, address, notes, eventType, guestCount, eventDate, eventTime) VALUES (@firstName, @lastName, @email, @phoneNo,@address,@notes, @eventType, @guestCount, @eventDate, @eventTime)";
                    SqlCommand command = new SqlCommand(query, conn);
                    // Add parameters and their values
                    command.Parameters.AddWithValue("@firstName", firstnameTxt.Text);
                    command.Parameters.AddWithValue("@lastName", lastnameTxt.Text);
                    command.Parameters.AddWithValue("@email", emailTxt.Text);
                    command.Parameters.AddWithValue("@phoneNo", phoneTxt.Text);
                    command.Parameters.AddWithValue("@address", addressTxt.Text);
                    command.Parameters.AddWithValue("@notes", notesTxt.Text);
                    command.Parameters.AddWithValue("@eventType", eventType.Text);
                    command.Parameters.AddWithValue("@guestCount", int.Parse(guestCountTxt.Text));
                    DateTime eventDate;
                    if (DateTime.TryParse(date.Text, out eventDate))
                    {
                        command.Parameters.AddWithValue("@eventDate", eventDate);
                    }
                    else
                    {
                        MessageBox.Show("Invalid date format. Please enter a valid date.");
                        return;
                    }
                    command.Parameters.AddWithValue("@eventTime", time.Text);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Customer data saved sucessfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }
        }
    }
}
