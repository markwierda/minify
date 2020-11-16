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
            // get values from register form
            string username = tBox_Username.Text;
            string email = tBox_Email.Text;
            string firstName = tBox_First_name.Text;
            string lastName = tBox_Last_Name.Text;
            string password = tBox_Password.Password;
            string confirmPassword = tBox_Confirm_Password.Password;

            // check if username is not unique
            if (!_controller.IsUniqueUsername(username))
            {
                // TODO: set error message
                return;
            }

            // check if email is not valid
            if (!_controller.IsValidEmail(email))
            {
                // TODO: set error message
                return;
            }

            // check if firstName is null or empty
            if (firstName.IsNullOrEmpty())
            {
                // TODO: set error message
                return;
            }

            // check if password does not equels confirmPassword
            if (!_controller.PasswordEqualsConfirmPassword(password, confirmPassword))
            {
                // TODO: set error message
                return;
            }

            // check if password is not valid
            if (!_controller.IsValidPassword(password))
            {
                // TODO: set error message
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
