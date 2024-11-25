using BackerBridge_2._0;
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

namespace Donator
{
    /// <summary>
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        private const double aspectRatio = 1.497;
        private int cnt = 0;

        public LogInWindow()
        {
            InitializeComponent();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double newWidth = e.NewSize.Width;
            double newHeight = newWidth / aspectRatio;
            this.Height = newHeight;
        }

        private void tbEmail_GotFocus(object sender, RoutedEventArgs e)
        {
            if (this.tbEmail.Text == "Your Email")
                this.tbEmail.Text = string.Empty;
        }

        private void tbEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbEmail.Text == string.Empty)
                this.tbEmail.Text = "Your Email";
        }

        private void tbPassword_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void tbPassword_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void btSignUp_Click(object sender, RoutedEventArgs e)
        {
            MainWindow objMainWindow = new MainWindow();
            this.Close();
            objMainWindow.Show();
        }

        private void btLogIn_Click(object sender, RoutedEventArgs e)
        {
            MainPage objMainPage = new MainPage();
            this.Close();
            objMainPage.Show();
        }
    }
}
