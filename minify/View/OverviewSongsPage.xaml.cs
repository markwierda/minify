using mini_spotify.Controller;
using mini_spotify.DAL.Entities;
using System.Collections.Generic;
using System.Windows.Controls;

namespace mini_spotify.View
{
    /// <summary>
    /// Interaction logic for OverviewSongsPage.xaml
    /// </summary>
    public partial class OverviewSongsPage : Page
    {
        private readonly SongController _controller;

        public OverviewSongsPage()
        {
            InitializeComponent();

            InitializeComponent();
            _controller = new SongController();
            List<Song> items = _controller.GetAll();

            Songs.ItemsSource = items;
        }
    }
}