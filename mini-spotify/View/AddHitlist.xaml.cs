using Castle.Core.Internal;
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
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
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

            // Check Title
            if (title.IsNullOrEmpty())
            {
                // display error message
                TitleError.Visibility = Visibility.Visible;

            }
            // Check descriptoin
            if (description.Length > 140)
            {
                // display error message
                DescriptionError.Visibility = Visibility.Visible;

            }

        }
    }
}
