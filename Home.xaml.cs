using System;
using System.Collections.Generic;
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
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Customer Button
            if (sender == Button1)
            {
                MainContent.Content = new User();
            }
            else if(sender == orderBtn)
            {
                MainContent.Content = new Order();
            } 
            else if(sender == menuBtn)
            {
                MainContent.Content = new Menu();
            }
            else
            {
                MainContent.Content = new Dashboard();
            }
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
