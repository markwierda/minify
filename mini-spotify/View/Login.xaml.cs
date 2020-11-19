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
            //Username && Wachtwoord ophalen van het formulier
            string username = Username.Text;
            string password = Password.Password;
            //Die gegevens valideren && TryLogin aanroepen
            if (loginController.TryLogin(username, password))
            {
                // TODO: Display overview
            }
            else
            {
                // TODO: Display error message
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
