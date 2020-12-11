using minify.Controller;
using minify.DAL.Entities;
using System.Collections.Generic;
using System.Windows;

namespace minify.View
{
    /// <summary>
    /// Interaction logic for OverviewSongs.xaml
    /// </summary>
    public partial class OverviewSongs : Window
    {
        private readonly SongController _controller;
        private List<Song> ListSongs { get; set; }

        public OverviewSongs()
        {
            InitializeComponent();
            _controller = new SongController();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            List<Song> items = _controller.GetAll();

            Songs.ItemsSource = items;
            Songs.Visibility = Visibility.Visible;
        }

        private void Songs_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Song[] songs = (Song[])e.AddedItems;

            if (ListSongs.Count > 4)
            {
                int a = 1;
            }
        }
    }
}