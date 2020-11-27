using mini_spotify.DAL;
using mini_spotify.DAL.Entities;
using mini_spotify.DAL.Repositories;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Windows.Data;
using Microsoft.EntityFrameworkCore;
using mini_spotify.Controller;

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
            AppDbContext context = new AppDbContextFactory().CreateDbContext(null);
            _controller = new SongController(context);
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            List<Song> items = _controller.GetAll();
               
            Songs.ItemsSource = items;
            Songs.Visibility = Visibility.Visible;
        }
    }
}