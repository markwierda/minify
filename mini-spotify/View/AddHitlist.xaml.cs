using Castle.Core.Internal;
using mini_spotify.Controller;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AddHitlist : Window
    {
        private readonly HitlistController hitlistContoller;

       
        public AddHitlist()
        {
            InitializeComponent();
            hitlistContoller = new HitlistController();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Create_Button_Click(object sender, RoutedEventArgs e)
        {
            // get Title and Description from page
            string title = TitleText.Text;
            string description = DescriptionText.Text;


            if (hitlistContoller.Validation(title, description))
            {

                Close();
            }
            else {
                
                // display error message for title
                TitleError.Visibility = Visibility.Visible;
                // display error message for description
                DescriptionError.Visibility = Visibility.Visible;
             }

           
        }
    }
}
