using mini_spotify.DAL.Entities;
using System.Collections.Generic;
using System.Windows;

namespace mini_spotify.View
{
    /// <summary>
    /// Interaction logic for OverviewHitlist.xaml
    /// </summary>
    public partial class OverviewHitlist : Window
    {
        public OverviewHitlist()
        {
            InitializeComponent();

            HitlistNoSongsMessage.Visibility = Visibility.Hidden;

            List<Song> items = new List<Song>
            {
                new Song() { Name = "John Doe", Genre = "Huh", Duration = 1, Path = "/path/to/song" },
                new Song() { Name = "Huh", Genre = "Piet", Duration = 4, Path = "/path/to/song" },
                new Song() { Name = "John", Genre = "Naar huis", Duration = 56, Path = "/path/to/song" }
            };

            HitlistSongs.ItemsSource = items;
            HitlistSongs.Visibility = Visibility.Visible;
        }
    }
}
