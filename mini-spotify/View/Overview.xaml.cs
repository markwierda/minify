using mini_spotify.Controller;
using mini_spotify.DAL.Entities;
using mini_spotify.Model;
using System.Collections.Generic;
using System.Windows;

namespace mini_spotify.View
{
    /// <summary>
    /// Interaction logic for Overview.xaml
    /// </summary>
    public partial class Overview : Window
    {
        private readonly HitlistController _controller;

        public Overview()
        {
            InitializeComponent();
            _controller = new HitlistController();
        }

        private void Initialize(object sender, RoutedEventArgs e)
        {
            GetAllHitList();
        }

        public void GetAllHitList()
        {
            List<Hitlist> hitlists = _controller.GetHitlistsByUserId(AppData.UserId);
            HitlistMenu.ItemsSource = hitlists;
        }

        private void Btn_Add_Hitlist(object sender, RoutedEventArgs e)
        {
            AddHitlist addHitlist = new AddHitlist();
            addHitlist.Show();
            Close();
        }

        private void HitlistMenu_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Hitlist selected = (Hitlist)e.AddedItems[0];
            OverviewHitlist overviewHitlist = new OverviewHitlist(selected.Id);
            overviewHitlist.Show();
            Close();
        }

        private void btn_home(object sender, RoutedEventArgs e)
        {
            Overview overview = new Overview();
            overview.Show();
            Close();
        }

        private void btn_songs(object sender, RoutedEventArgs e)
        {
            OverviewSongs overviewSongs = new OverviewSongs();
            overviewSongs.Show();
            Close();
        }
    }
}
