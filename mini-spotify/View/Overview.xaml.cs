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
        private HitlistController _hitlistController;

        public Overview()
        {
            InitializeComponent();
            _hitlistController =new HitlistController();
        }

        private void Initialize(object sender, RoutedEventArgs e)
        {
            GetAllHitList();
        }

        public void GetAllHitList()
        {
            List<Hitlist> hitlists = _hitlistController.GetHitlistsByUserId(AppData.Id);
            HitlistMenu.ItemsSource = hitlists;
        }

        private void btn_Add_Hitlist(object sender, RoutedEventArgs e)
        {
            // TODO: open add hitlist page
        }

        private void HitlistMenu_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Hitlist selected = (Hitlist)e.AddedItems[0];
            OverviewHitlist overviewHitlist = new OverviewHitlist(selected.Id);
            overviewHitlist.Show();
            Close();
        }
    }
}
