using minify.Controller;
using minify.DAL.Entities;
using minify.Model;
using System.Windows;
using System.Windows.Controls;

namespace minify.View
{
    /// <summary>
    /// Interaction logic for AddHistlistPage.xaml
    /// </summary>
    public partial class AddHistlistPage : Page
    {
        private readonly HitlistController _controller;

        public AddHistlistPage(HitlistController controller)
        {
            InitializeComponent();
            _controller = controller;
        }

        private void Create_Button_Click(object sender, RoutedEventArgs e)
        {
            // get Title and Description from page
            string title = TitleText.Text;
            string description = DescriptionText.Text;

            if (_controller.Validation_Title(title) == false)
            {
                // display error message for title
                TitleError.Visibility = Visibility.Visible;
            }
            else if (_controller.Validation_Description(description) == false)
            {
                // display error message for description
                DescriptionError.Visibility = Visibility.Visible;
            }
            else
            {
                Hitlist hitlist = new Hitlist(title, description, AppData.UserId);
                _controller.Add(hitlist);
                //OverviewHitlist overviewHitlist = new OverviewHitlist(hitlist.Id);
                //overviewHitlist.Show();
            }
        }
    }
}