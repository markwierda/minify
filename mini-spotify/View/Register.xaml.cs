using Castle.Core.Internal;
using mini_spotify.Controller;
using mini_spotify.DAL;
using mini_spotify.DAL.Entities;
using System.Windows;

namespace mini_spotify.View
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        private RegisterController _controller;

        public Register()
        {
            InitializeComponent();
            AppDbContext context = new AppDbContextFactory().CreateDbContext(null);
            _controller = new RegisterController(context);
        }

        private void Register_Button_Click(object sender, RoutedEventArgs e)
        {
            //set error messages on hidden
            UsernameErrorMessage.Visibility = Visibility.Hidden;
            EmailErrorMessage.Visibility = Visibility.Hidden;
            FirstNameErrorMessage.Visibility = Visibility.Hidden;
            PasswordEqualsErrorMessage.Visibility = Visibility.Hidden;
            PasswordErrorMessage.Visibility = Visibility.Hidden;

            // get values from register form
            string username = tBox_Username.Text;
            string email = tBox_Email.Text;
            string firstName = tBox_First_name.Text;
            string lastName = tBox_Last_Name.Text;
            string password = tBox_Password.Password;
            string confirmPassword = tBox_Confirm_Password.Password;

            // check if firstName is null or empty
            if (firstName.IsNullOrEmpty())
            {
                FirstNameErrorMessage.Visibility = Visibility.Visible;
                return;
            }

            // check if username is not unique
            if (!_controller.IsUniqueUsername(username))
            {
                UsernameErrorMessage.Visibility = Visibility.Visible;
                return;
            }

            // check if email is not valid
            if (!_controller.IsValidEmail(email))
            {
                EmailErrorMessage.Visibility = Visibility.Visible;
                return;
            }

            // check if password does not equals confirmPassword
            if (!_controller.PasswordEqualsConfirmPassword(password, confirmPassword))
            {
                PasswordEqualsErrorMessage.Visibility = Visibility.Visible;
                return;
            }

            // check if password is not valid
            if (!_controller.IsValidPassword(password))
            {
                PasswordErrorMessage.Visibility = Visibility.Visible;
                return;
            }

            // add new user
            _controller.Add(
                new User(username, email, firstName, lastName, password)
            );
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            Close();
        }
    }
}
