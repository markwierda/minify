using mini_spotify.Controller;
using mini_spotify.DAL;
using System.Windows;

namespace mini_spotify.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private readonly LoginController loginController;

        public Login()
        {
            InitializeComponent();
            AppDbContext context = new AppDbContextFactory().CreateDbContext(null);
            loginController = new LoginController(context);
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
            if (loginController.TryLogin(username, password))
            {
                // TODO: Display overview

                // For testing, can be deleted later:
                OverviewHitlist overviewHitlist = new OverviewHitlist(new System.Guid("9b0cc3c2-8df5-45bf-a0c4-05a8476443d0"));
                overviewHitlist.Show();
                Close();
                // end
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
