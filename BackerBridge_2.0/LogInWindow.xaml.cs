using BackerBridge_2._0;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
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
            string email = tbEmail.Text.Trim();
            string password = tbPassword.Password;

            using (var context = new BackerBridgeDataContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Email == email);

                if (user == null)
                {
                    Console.WriteLine("Email not ok");
                    LbMessage.Content = "Invalid email or password";
                    return;
                }
                byte[] userPassword = user.UserPassword.ToArray();  
                byte[] computedHash = HashPassword(password);

                Console.WriteLine(userPassword.ToString());
                Console.WriteLine(computedHash.ToString());

                if (!computedHash.SequenceEqual(userPassword))
                {
                    Console.WriteLine("Password not ok");

                    LbMessage.Content = "Invalid email or password";
                    return;
                }

                MessageBox.Show("Login successful!", "Success",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                new MainPage().Show();
                this.Close();
            }
        }


        private byte[] HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                return bytes;
            }
        }

    }
}
