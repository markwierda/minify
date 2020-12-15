using minify.Controller;
using minify.DAL.Entities;
using mini_spotify.Controller;
using mini_spotify.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace minify.View
{
    /// <summary>
    /// Interaction logic for OverviewSongs.xaml
    /// </summary>
    public partial class OverviewSongs : Window
    {
        private readonly SongController _songController;
        private readonly HitlistController _hitlistController;
        private List<Song> ListSongs { get; set; }

        public OverviewSongs()
        {
            InitializeComponent();
            _songController = new SongController();
            _hitlistController = new HitlistController();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            List<Song> items = _songController.GetAll();

            Songs.ItemsSource = items;
            Songs.Visibility = Visibility.Visible;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Guid songId = (Guid)btn.CommandParameter;
            //ChooseHitlistDialog choose = new ChooseHitlistDialog(songId, this);
            


        }
    }
}