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
        private readonly Repository<Song> _songRepository;
        private readonly SongController _controller;


        public OverviewSongs()
        {
            
            InitializeComponent();
            AppDbContext context = new AppDbContextFactory().CreateDbContext(null);
            _songRepository = new Repository<Song>(context);
            _controller = new SongController(context);
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            //HitlistNoSongsMessage.Visibility = Visibility.Hidden;

            List<Song> items = _controller.GetAll();
                /*new List<Song>
            {
                new Song() { Name = "99 luftballons", Genre = "Alternative", Artist = "Nena", Duration = 1, Path = "/path/to/song" },
                new Song() { Name = "Frikandel Speciaal", Genre = "Dance", Artist = "Stefan en Sean", Duration = 4, Path = "/path/to/song" },
                new Song() { Name = "Let It Go", Genre = "Soundtrack", Artist = "Demi Lovato", Duration = 4, Path = "/path/to/song"}
            };*/

            Songs.ItemsSource = items;
            Songs.Visibility = Visibility.Visible;
        }


        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private List<Song> GetAllSongs()
        {
            return _songRepository.GetAll().ToList();
        }
    }
}