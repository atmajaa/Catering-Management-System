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

namespace CaterHub
{
    /// <summary>
    /// Interaction logic for SignIn.xaml
    /// </summary>
    public partial class SignIn : Window
    {
        public SignIn()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-J7PRN5P\\SQLEXPRESS;Initial Catalog=CaterHub;Integrated Security=True;Encrypt=False");
        void isValid()
        {
            
             if (email_txt.Text == string.Empty)
            {
                emailError.Visibility = Visibility.Visible;
            }
            else
            {
                passwordError.Visibility = Visibility.Visible;
            }
        }
        private void signIn_btn_Click(object sender, RoutedEventArgs e)
        {
            if (email_txt.Text == string.Empty || password_txt.Text == string.Empty)
            {
                isValid();
            }
            else
            {
                Home home = new Home();
                SqlCommand cmd = new SqlCommand("SELECT * FROM  User_Details  WHERE email = @email AND password = @password ", conn);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@email", email_txt.Text);
                cmd.Parameters.AddWithValue("@password", password_txt.Text);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows) // Check if any rows are returned
                    {
                        MessageBox.Show("Successfully Signed In!");
                        home.Show(); // For example
                        home.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    }
                    else
                    {
                        MessageBox.Show("Invalid email or password.");
                    }
                }
                conn.Close();
                
            }
        }
    }
}
