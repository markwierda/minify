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

                // Play song
                MediaplayerController.Play();
            }
        }

        public OverviewSongsPage(List<Song> songs)
        {
            InitializeComponent();
            Songs.ItemsSource = songs;
        }
    }
}
