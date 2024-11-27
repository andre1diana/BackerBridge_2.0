using BackerBridge_2._0;
using System.Windows.Controls;
using System.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Runtime.Remoting.Messaging;

namespace Donator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const double aspectRatio = 1.497;
        private int cnt = 0;
        private int[] allowValidation;
        private bool showPassword = true;
        private bool showConfirmPassword = true;
        private int id = 3;

        public MainWindow()
        {
            InitializeComponent();
            allowValidation = new int[6];
        }

        private void tbLastName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (this.tbLastName.Text == "Your Last Name")
                this.tbLastName.Text = string.Empty;
        }

        private void tbLastName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.tbLastName.Text == string.Empty)
                this.tbLastName.Text = "Your Last Name";
            else
                allowValidation[0] = 1;
        }

        private void tbFirstName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (this.tbFirstName.Text == "Your First Name")
                this.tbFirstName.Text = string.Empty;
        }

        private void tbFirstName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.tbFirstName.Text == string.Empty)
                this.tbFirstName.Text = "Your First Name";
            else
                allowValidation[1] = 1;
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
            else
                allowValidation[2] = 1;
        }

        private void tbPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            allowValidation[3] = 0;
        }

        private void tbPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            tbPasswordVisible.Text = tbPassword.Password;
            if (this.tbPassword.Password == string.Empty)
                this.tbPassword.Password = string.Empty;
            else
                allowValidation[3] = 1;
        }

        private void tbBirthDate_GotFocus(object sender, RoutedEventArgs e)
        {
            if (this.tbBirthDate.Text == "Your Birth Date")
                this.tbBirthDate.Text = string.Empty;
        }

        private void tbBirthDate_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.tbBirthDate.Text == string.Empty)
                this.tbBirthDate.Text = "Your Birth Date";
            else
                allowValidation[5] = 1;
        }


        private void tbConfirmPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            allowValidation[4] = 0;
        }

        private void tbConfirmPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            tbConfirmPasswordVisible.Text = tbConfirmPassword.Password;
            if (this.tbPassword.Password == this.tbConfirmPassword.Password)
                allowValidation[4] = 1;
        }

        private void btLogIn_Click(object sender, RoutedEventArgs e)
        {
            LogInWindow objLogInWindow = new LogInWindow();
            this.Close();
            objLogInWindow.Show();
        }

        private void btSignUp_Click(object sender, RoutedEventArgs e)
        {
            LbMessage.Content = string.Empty;
            BackerBridgeDataContext data = new BackerBridgeDataContext();

            int validationSum = 0;
            Console.WriteLine("=======ValidationSum=========");
            for (int i = 0; i < 6; i++)
            {
                validationSum += allowValidation[i];
                Console.WriteLine($"index {i} : {allowValidation[i]}");
            }
            Console.WriteLine($"Total sum : {validationSum}/6");

            var emails = from U in data.Users
                         select U.Email;
            foreach (var email in emails)
            {
                if (email == this.tbEmail.Text)
                {
                    LbMessage.Content = "User with this email already exists!";
                    return;
                }
            }

            if (validationSum == 6)
            {
                try
                {
                    byte[] salt = GenerateSalt();
                    //byte[] hashedPassword = HashPasswordWithSalt(tbPassword.Password, salt);

                    User newUser = new User
                    {
                        UserID = id++,
                        FirstName = tbFirstName.Text,
                        LastName = tbLastName.Text,
                        Email = tbEmail.Text,
                        //UserAddress = tbAddress.Text,
                        BirthDate = DateTime.Parse(tbBirthDate.Text),
                        UserType = "donor",
                        UserPassword = new System.Data.Linq.Binary(BitConverter.GetBytes(tbPassword.GetHashCode())),
                        Salt = salt
                    };

                    data.Users.InsertOnSubmit(newUser);
                    data.SubmitChanges();

                    MessageBox.Show("User registered successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    MainPage objMainPage = new MainPage();
                    this.Close();
                    objMainPage.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                LbMessage.Content = "Please ensure all validations are met!";
            }
        }

        private byte[] GenerateSalt()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[20];
                rng.GetBytes(salt);
                return salt;
            }
        }

        private byte[] HashPasswordWithSalt(string password, byte[] salt)
        {
                //using (var sha256 = SHA256.Create())
                //{
                //    byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                //    byte[] passwordWithSalt = new byte[passwordBytes.Length + salt.Length];

                //    Buffer.BlockCopy(passwordBytes, 0, passwordWithSalt, 0, passwordBytes.Length);
                //    Buffer.BlockCopy(salt, 0, passwordWithSalt, passwordBytes.Length, salt.Length);

                //    return sha256.ComputeHash(passwordWithSalt);
                //}
                //byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                //return passwordBytes.GetHashCode();
                return null;
        }


        private void dpBirthDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Console.WriteLine($"DEBUG: birth date = {this.dpBirthDate.Text}");
            tbBirthDate.Text = this.dpBirthDate.Text;
            allowValidation[5] = 1;
        }

        private void btShowPassword_Click(object sender, RoutedEventArgs e)
        {
            showPassword = !showPassword;
            if (showPassword)
            {
                tbPassword.Visibility = Visibility.Visible;
                tbPasswordVisible.Visibility = Visibility.Collapsed;
            }
            else
            {
                tbPassword.Visibility = Visibility.Collapsed;
                tbPasswordVisible.Visibility = Visibility.Visible;
            }
        }

        private void tbPasswordVisible_GotFocus(object sender, RoutedEventArgs e)
        {
            allowValidation[3] = 0;
        }

        private void tbPasswordVisible_LostFocus(object sender, RoutedEventArgs e)
        {
            tbPassword.Password = tbPasswordVisible.Text;
            if (this.tbPasswordVisible.Text == tbPassword.Password)
                allowValidation[3] = 1;
        }

        private void tbConfirmPasswordVisible_GotFocus(object sender, RoutedEventArgs e)
        {
            allowValidation[4] = 0;
        }

        private void tbConfirmPasswordVisible_LostFocus(object sender, RoutedEventArgs e)
        {
            tbConfirmPassword.Password = tbConfirmPasswordVisible.Text;
            if (this.tbConfirmPasswordVisible.Text == tbConfirmPassword.Password)
                allowValidation[4] = 1;
        }

        private void btShowConfirmPassword_Click(object sender, RoutedEventArgs e)
        {
            showConfirmPassword = !showConfirmPassword;
            if (showConfirmPassword)
            {
                tbConfirmPassword.Visibility = Visibility.Visible;
                tbConfirmPasswordVisible.Visibility = Visibility.Collapsed;
            }
            else
            {
                tbConfirmPassword.Visibility = Visibility.Collapsed;
                tbConfirmPasswordVisible.Visibility = Visibility.Visible;
            }
        }

        private void tbPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            cnt++;
            Console.WriteLine($"DEBUG: password(hidden) changed event {cnt}");
        }

        private void tbPasswordVisible_TextChanged(object sender, TextChangedEventArgs e)
        {
            cnt++;
            Console.WriteLine($"DEBUG: password(visible) changed event {cnt}");
        }

        private void tbConfirmPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            cnt++;
            Console.WriteLine($"DEBUG: Vpassword(hidden) changed event {cnt}");
        }

        private void tbConfirmPasswordVisible_TextChanged(object sender, TextChangedEventArgs e)
        {
            cnt++;
            Console.WriteLine($"DEBUG: Vpassword(visible) changed event {cnt}");
        }
    }
}