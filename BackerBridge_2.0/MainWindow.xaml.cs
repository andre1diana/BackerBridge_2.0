using BackerBridge_2._0;
using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Donator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        private const double aspectRatio = 1.497;
        private int cnt = 0;
        private int[] allowValidation;
        private bool showPassword = true;
        private bool showConfirmPassword = true;

        public MainWindow()
        {
            InitializeComponent();
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
            if (this.tbPassword.Password == string.Empty)
                this.tbPassword.Password = string.Empty;

            tbPasswordVisible.Text = tbPassword.Password;
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
        }


        private void tbConfirmPassword_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void tbConfirmPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.tbPassword.Password != this.tbConfirmPassword.Password)
                this.tbConfirmPassword.Password = string.Empty;
        }

        private void btLogIn_Click(object sender, RoutedEventArgs e)
        {
            // TODO verifica email si parola in baza de date si valideaza autentificarea
            LogInWindow objLogInWindow = new LogInWindow();
            this.Close();
            objLogInWindow.Show();
        }

        private void btSignUp_Click(object sender, RoutedEventArgs e)
        {
            //TODO adauga in baza de date utilizatorul
            BackerBridgeDataContext data = new BackerBridgeDataContext();        
            if (this.tbEmail.Text == "Your Email")
            {
                this.LbMessage.Content = "Please introduce a valid email";
                return;
            }

            var emails = from user1 in data.Users
                         select new { user1.Email };

            foreach (var email in emails)
            {
                if (email.Email == this.tbEmail.Text)
                {
                    this.LbMessage.Content = "A user with this email address already exists!";
                    return;
                }
            }
            if (this.tbFirstName.Text != null && this.tbLastName.Text != null)
            {
                if (this.tbPassword == this.tbConfirmPassword)
                {

                }
            }
            //UserClass user = new UserClass();
            //user.lastName = tbLastName.Text;
            //user.firstName = tbFirstName.Text;
            //user.email = tbEmail.Text;
            //user.password = tbPassword.Password;
            //user.birthday = dpBirthDate.SelectedDate;

            Console.WriteLine($"{tbPassword.Password.GetHashCode()}");

            MainPage objMainPage = new MainPage();
            this.Close();
            objMainPage.Show();
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
                //tbPassword.Password = tbPasswordVisible.Text;
            }
            else
            {
                tbPassword.Visibility = Visibility.Collapsed;
                tbPasswordVisible.Visibility = Visibility.Visible;
                //tbPasswordVisible.Text = tbPassword.Password;
            }
        }

        private void tbPasswordVisible_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void tbPasswordVisible_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.tbPasswordVisible.Text == string.Empty)
                this.tbPasswordVisible.Text = string.Empty;

            tbPassword.Password = tbPasswordVisible.Text;
        }

        private void tbConfirmPassword_GotFocus_1(object sender, RoutedEventArgs e)
        {

        }

        private void tbConfirmPasswordVisible_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void tbConfirmPasswordVisible_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void btShowConfirmPassword_Click(object sender, RoutedEventArgs e)
        {
            showConfirmPassword = !showConfirmPassword;
            if (showPassword)
            {
                tbConfirmPassword.Visibility = Visibility.Visible;
                tbConfirmPasswordVisible.Visibility = Visibility.Collapsed;
                //tbConfirmPassword.Password = tbPasswordVisible.Text;
            }
            else
            {
                tbConfirmPassword.Visibility = Visibility.Collapsed;
                tbConfirmPasswordVisible.Visibility = Visibility.Visible;
                //tbPasswordVisible.Text = tbPassword.Password;
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
