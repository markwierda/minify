using mini_spotify.Controller;
using mini_spotify.DAL;
using mini_spotify.DAL.Entities;
using mini_spotify.Model;
using System.Windows;

namespace mini_spotify.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AddHitlist : Window
    {
        private readonly HitlistController _controller;

        public AddHitlist()
        {
            InitializeComponent();
            _controller = new HitlistController();
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
                OverviewHitlist overviewHitlist = new OverviewHitlist(hitlist.Id);
                overviewHitlist.Show();
                Close();
            }
        }
    }
}
