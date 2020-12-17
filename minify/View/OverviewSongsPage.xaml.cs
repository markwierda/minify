using minify.View;
using minify.Controller;
using minify.DAL.Entities;
using minify.Managers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows;
using System.Diagnostics;

namespace minify.View
{
    /// <summary>
    /// Interaction logic for OverviewSongsPage.xaml
    /// </summary>
    public partial class OverviewSongsPage : Page
    {
        private readonly List<Song> _songs;

        public OverviewSongsPage()
        {
            InitializeComponent();
            SongController songController = new SongController();
            _songs = songController.GetAll();
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
        }

        public void Refresh()
        {
            SongController controller = new SongController();
            List<Song> items = controller.GetAll();
            Songs.ItemsSource = items.ToArray();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = (Button)sender;           
                Guid songId = (Guid)btn.CommandParameter;
                ChooseHitlistDialog choose = new ChooseHitlistDialog(songId, this);
                choose.IdRetreived += IdRetreived;
                choose.Show();
                btn.Visibility = Visibility.Hidden;
            }
            catch(Exception)
            {
                //Handle Exception
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IdRetreived(object sender, EventArgs e)
        {
            try
            {
                var re = (IdRetreivedEventArgs)e;

                HitlistController hitlistController = new HitlistController();
                SongController songController = new SongController();

                Hitlist hitlist = hitlistController.Get(re.HitlistId, true);
                Song song = songController.Get(re.SongId);

                if (!hitlist.Songs.Any(x => x.SongId == song.Id))
                {
                    hitlistController.AddSongsToHitlist(hitlist, song);
                }
            }
            catch(Exception ex)
            {
                // Handle exception
                Debug.Write(ex);
            }
        }

        public OverviewSongsPage(List<Song> songs)
        {
            InitializeComponent();
            Songs.ItemsSource = songs;
        }
    }
}
