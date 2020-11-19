using System.Windows;

namespace mini_spotify.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Create_Account_Button_click(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            register.Show();
            Close();
        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {

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
