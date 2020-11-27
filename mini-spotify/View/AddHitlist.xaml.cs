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
