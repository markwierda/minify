using minify.Controller;
using minify.DAL.Entities;
using minify.Model;
using System.Windows;
using System.Windows.Controls;

namespace minify.View
{
    public delegate void HitlistAddedEventHandler(object sender, UpdateHitlistMenuEventArgs e);

    /// <summary>
    /// Interaction logic for AddHistlistPage.xaml
    /// </summary>
    public partial class AddHistlistPage : Page
    {
        public event HitlistAddedEventHandler HitlistAdded;

        public AddHistlistPage()
        {
            InitializeComponent();
        }

        private void Create_Button_Click(object sender, RoutedEventArgs e)
        {
            HitlistController controller = new HitlistController();
            // get Title and Description from page
            string title = TitleText.Text;
            string description = DescriptionText.Text;

            if (controller.Validation_Title(title) == false)
            {
                // display error message for title
                TitleError.Visibility = Visibility.Visible;
            }
            else if (controller.Validation_Description(description) == false)
            {
                // display error message for description
                DescriptionError.Visibility = Visibility.Visible;
            }
            else
            {
                Hitlist hitlist = new Hitlist(title, description, AppData.UserId);
                hitlist = controller.Add(hitlist);
                HitlistAdded.Invoke(this, new UpdateHitlistMenuEventArgs(hitlist.Id));
                //OverviewHitlist overviewHitlist = new OverviewHitlist(hitlist.Id);
                //overviewHitlist.Show();

                TitleText.Text = string.Empty;
                DescriptionText.Text = string.Empty;
            }
        }
    }
}