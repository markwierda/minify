using Castle.Core.Internal;
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
    public partial class Window1 : Window
    {
        private readonly HitlistController _controller;

        public Window1()
        {
            InitializeComponent();
            AppDbContext context = new AppDbContextFactory().CreateDbContext(null);
            _controller = new HitlistController(context);
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

            // errors standard false
            bool errors = false;

            // Check Title
            if (title.IsNullOrEmpty())
            {
                // display error message
                TitleError.Visibility = Visibility.Visible;
                errors = true;

            }
            // Check descriptoin
            if (description.Length > 140)
            {
                // display error message
                DescriptionError.Visibility = Visibility.Visible;
                errors = true;
            }

            // add a new hitlist if there are no errors
            if (!errors)
            {
                Hitlist hitlist = new Hitlist(title, description, AppData.Id);
                _controller.Add(hitlist);
                OverviewHitlist overviewHitlist = new OverviewHitlist(hitlist.Id);
                overviewHitlist.Show();
                Close();
            }
        }
    }
}
