using minify.Controller;
using System.Windows;

namespace minify.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private readonly LoginController _controller;

        public Login()
        {
            InitializeComponent();
            _controller = new LoginController();
        }

        private void Create_Account_Button_click(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            register.Show();
            Close();
        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            // get username and password from page
            string username = Username.Text;
            string password = Password.Password;

            // try to login with the values
            if (_controller.TryLogin(username, password))
            {
                // TODO: Display overview
                Overview overview = new Overview();
                overview.Show();
                Close();
            }
            else
            {
                // display error message
                Messages.Visibility = Visibility.Visible;
                LoginErrorMessage.Visibility = Visibility.Visible;
            }
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void OnRegister()
        {
            Messages.Visibility = Visibility.Visible;
            RegisteredMessage.Visibility = Visibility.Visible;
        }
    }
}