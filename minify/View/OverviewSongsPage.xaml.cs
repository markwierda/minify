using minify.Controller;
using minify.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace minify.View
{
    /// <summary>
    /// Interaction logic for OverviewSongsPage.xaml
    /// </summary>
    public partial class OverviewSongsPage : Page
    {
        private readonly SongController _controller;
        private readonly List<Song> _songs;

        public OverviewSongsPage()
        {
            InitializeComponent();
            _controller = new SongController();
            _songs = _controller.GetAll();
            Songs.ItemsSource = _songs;
        }

        public void Refresh(Song song)
        {
            Songs.ItemsSource = _songs;

            foreach (var item in Songs.Items)
            {
                if (((Song)item).Equals(song))
                    Songs.SelectedItem = item;


            }
        }
            _songController = new SongController();
            _hitlistController = new HitlistController();

            List<Song> items = _songController.GetAll();

            Songs.ItemsSource = items.ToArray();

        private void Songs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                // Get song
                Song selectedSong = (Song)e.AddedItems[0];

                // Initialize songs
                MediaplayerController.Initialize(_songs);

                // Open song
                MediaplayerController.Open(selectedSong);
        }

        public void Refresh()
        {
            List<Song> items = _songController.GetAll();
            Songs.ItemsSource = items.ToArray();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Guid songId = (Guid)btn.CommandParameter;
            ChooseHitlistDialog choose = new ChooseHitlistDialog(songId, this);
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

            Songs.ItemsSource = items;
        }
    }
}
