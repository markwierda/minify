using mini_spotify.Controller;
using mini_spotify.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Windows;

namespace mini_spotify.View
{
    /// <summary>
    /// Interaction logic for OverviewHitlist.xaml
    /// </summary>
    public partial class OverviewHitlist : Window
    {
        private readonly HitlistCotroller _controller;

        public OverviewHitlist()
        {
            InitializeComponent();
            _controller = new HitlistCotroller();

            List<Song> items = new List<Song>
            {
                new Song() { Name = "John Doe", Genre = "Huh", Duration = new TimeSpan(0, 1, 30), Path = "/path/to/song" },
                new Song() { Name = "Huh", Genre = "Piet", Duration = new TimeSpan(0, 2, 30), Path = "/path/to/song" },
                new Song() { Name = "John", Genre = "Naar huis", Duration = new TimeSpan(0, 6, 00), Path = "/path/to/song" }
            };

            HitlistInfo.Content = _controller.GetHitlistInfo(items);

            if (items.Count > 0)
            {
                HitlistSongs.ItemsSource = items;
                HitlistSongs.Visibility = Visibility.Visible;
            }
        }
    }
}
