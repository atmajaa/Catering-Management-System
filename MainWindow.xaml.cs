using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
namespace CaterHub
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-J7PRN5P\\SQLEXPRESS;Initial Catalog=CaterHub;Integrated Security=True;Encrypt=False");
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }
        void  isValid()
        {
            if (businessName_txt.Text == string.Empty)
            {
                businessError.Visibility = Visibility.Visible;
            }
            else if(email_txt.Text == string.Empty)
            {
                emailError.Visibility = Visibility.Visible;
            }
            else
            {
                passwordError.Visibility = Visibility.Visible;
            }
        }

        //Sign Up Button Click
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(businessName_txt.Text == string.Empty || email_txt.Text == string.Empty || password_txt.Text == string.Empty)
            {
                isValid();
            }
            else
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO User_Details  VALUES(@businessName, @email, @password)", conn);
                cmd.CommandType =System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@businessName", businessName_txt.Text);
                cmd.Parameters.AddWithValue("@email", email_txt.Text);
                cmd.Parameters.AddWithValue("@password", password_txt.Text);
                conn.Open();
                //Execute Non Query is used to execute query that does not produce any results like UPDATE, INSERT
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Sucessfully Signed Up!");
                MainWindow mainWindow = new MainWindow();
                Home home = new Home();
                Application.Current.Windows[0].Close();
                home.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                home.ShowDialog();
            }

        }
        //Already have a/c - sign in button
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SignIn signIn = new SignIn();
            Application.Current.Windows[0].Close();
            signIn.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            signIn.ShowDialog();
        }

        private void signInBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            signInBtn.Background = Brushes.LightGray;
        }

        private void signInBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            signInBtn.Background = Brushes.White;
        }
    }
}