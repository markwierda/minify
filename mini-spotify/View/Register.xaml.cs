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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            AppDbContext context = new AppDbContextFactory().CreateDbContext(null);
            InitializeComponent();
            RegisterConntroller.Initialize(context);
        }

        private void Register_Button_Click(object sender, RoutedEventArgs e)
        {
            // get values from register form
            string firstName = tBox_First_name.Text;
            string lastName = tBox_Last_Name.Text;
            string username = tBox_Username.Text;
            string email = tBox_Email.Text;
            string password = tBox_Password.Password;
            string confirmPassword = tBox_Confirm_Password.Password;

            // validate values, if true then create a new user
            if (RegisterConntroller.Validate(firstName, lastName, username, email, password, confirmPassword))
            {
                Console.WriteLine("huh");
                // TODO: call create function here
            }
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            Close();
        }
    }
}
