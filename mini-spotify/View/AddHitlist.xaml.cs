using Castle.Core.Internal;
using mini_spotify.Controller;
using System.Windows;

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


            if (hitlistContoller.Validation_Title(title) == false)
            {
                // display error message for title
                TitleError.Visibility = Visibility.Visible;
            }
            else if (hitlistContoller.Validation_Description(description) == false) 
            { 
                // display error message for description
                DescriptionError.Visibility = Visibility.Visible;
            }
            else
            {
               
            }
        }
    }
}
