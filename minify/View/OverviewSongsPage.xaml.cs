using mini_spotify.Controller;
using mini_spotify.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace mini_spotify.View
{
    /// <summary>
    /// Interaction logic for OverviewSongsPage.xaml
    /// </summary>
    public partial class OverviewSongsPage : Page
    {
        private readonly SongController _songController;
        private readonly HitlistController _hitlistController;
        private List<Song> ListSongs { get; set; }

        public OverviewSongsPage()
        {
            InitializeComponent();
            _songController = new SongController();
            _hitlistController = new HitlistController();

            List<Song> items = _songController.GetAll();

            Songs.ItemsSource = items.ToArray();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Guid songId = (Guid)btn.CommandParameter;
            ChooseHitlistDialog choose = new ChooseHitlistDialog(songId);
            choose.IdRetreived += IdRetreived;
            choose.Show();
            btn.Visibility = Visibility.Hidden;
       

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IdRetreived(object sender, EventArgs e)
        {
            var re = (IdRetreivedEventArgs)e;

            Hitlist hitlist = _hitlistController.Get(re.HitlistId, true);
            Song song = _songController.Get(re.SongId);

            if(!hitlist.Songs.Any(x => x.SongId == song.Id))
            {
                _hitlistController.AddSongsToHitlist(hitlist, song);
            }

        }
    }
}