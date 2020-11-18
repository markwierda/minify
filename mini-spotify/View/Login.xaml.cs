using mini_spotify.Controller;
using mini_spotify.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
                Overview overview = new Overview();
                overview.Show();
                this.Close();
            }
            else
            {
                // TODO: Display error message
              
                MessageBox.Show("Wrong username or password please try again", "Error");
                
            }




        }

       
    }
}
