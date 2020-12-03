using mini_spotify.Controller;
using mini_spotify.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace mini_spotify.View
{
    /// <summary>
    /// Interaction logic for OverviewSongs.xaml
    /// </summary>
    public partial class OverviewSongs : Window
    {
        private readonly SongController _controller;
        public OverviewSongs()
        {
            
            InitializeComponent();
            _controller = new SongController();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            List<Song> items = _controller.GetAll();
               
            Songs.ItemsSource = items;
            Songs.Visibility = Visibility.Visible;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Guid id = (Guid)btn.CommandParameter;

            int a = 1;

        }
    }
}