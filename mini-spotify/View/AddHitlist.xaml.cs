using Castle.Core.Internal;
using mini_spotify.Controller;
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
        private readonly HitlistController hitlistContoller;

        public AddHitlist()
        private readonly HitlistController _controller;

        public Window1()
        {
            InitializeComponent();
            hitlistContoller = new HitlistController();
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


            if (hitlistContoller.Validation_Title(title) == false)
            // errors standard false
            bool errors = false;

            // Check Title
            if (title.IsNullOrEmpty())
            {
                // display error message for title
                TitleError.Visibility = Visibility.Visible;
                errors = true;

            }
            else if (hitlistContoller.Validation_Description(description) == false) 
            { 
                // display error message for description
                DescriptionError.Visibility = Visibility.Visible;
            }
            else
            {
               
                errors = true;
            }

            // add a new hitlist if there are no errors
            if (!errors)
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
