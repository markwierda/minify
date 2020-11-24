using mini_spotify.DAL.Entities;
using System.Collections.Generic;
using System.Windows;

namespace mini_spotify.View
{
    /// <summary>
    /// Interaction logic for Overview.xaml
    /// </summary>
    public partial class Overview : Window
    {
        public Overview()
        {
            InitializeComponent();
        }

        private void Initialize(object sender, RoutedEventArgs e)
        {
            GetAllHitList();
        }

        public void GetAllHitList()
        {
            List<Song> items = new List<Song>
            {
                new Song() { Name = "John Doe", Genre = "Huh", Duration = 1, Path = "/path/to/song" },
                new Song() { Name = "Huh", Genre = "Piet", Duration = 4, Path = "/path/to/song" },
                new Song() { Name = "Huh", Genre = "Piet", Duration = 4, Path = "/path/to/song" },
                new Song() { Name = "Huh", Genre = "Piet", Duration = 4, Path = "/path/to/song" },
                new Song() { Name = "Huh", Genre = "Piet", Duration = 4, Path = "/path/to/song" },
                new Song() { Name = "Huh", Genre = "Piet", Duration = 4, Path = "/path/to/song" },
                new Song() { Name = "Huh", Genre = "Piet", Duration = 4, Path = "/path/to/song" },
                new Song() { Name = "Huh", Genre = "Piet", Duration = 4, Path = "/path/to/song" },
                new Song() { Name = "Huh", Genre = "Piet", Duration = 4, Path = "/path/to/song" },
                new Song() { Name = "Huh", Genre = "Piet", Duration = 4, Path = "/path/to/song" },
                new Song() { Name = "Huh", Genre = "Piet", Duration = 4, Path = "/path/to/song" },
                new Song() { Name = "Huh", Genre = "Piet", Duration = 4, Path = "/path/to/song" },
                new Song() { Name = "Huh", Genre = "Piet", Duration = 4, Path = "/path/to/song" },
                new Song() { Name = "Huh", Genre = "Piet", Duration = 4, Path = "/path/to/song" },
                new Song() { Name = "Huh", Genre = "Piet", Duration = 4, Path = "/path/to/song" },
                new Song() { Name = "Huh", Genre = "Piet", Duration = 4, Path = "/path/to/song" },
                new Song() { Name = "Huh", Genre = "Piet", Duration = 4, Path = "/path/to/song" },
                new Song() { Name = "Huh", Genre = "Piet", Duration = 4, Path = "/path/to/song" },
                new Song() { Name = "Huh", Genre = "Piet", Duration = 4, Path = "/path/to/song" },
                new Song() { Name = "Huh", Genre = "Piet", Duration = 4, Path = "/path/to/song" },
                new Song() { Name = "Huh", Genre = "Piet", Duration = 4, Path = "/path/to/song" },
                new Song() { Name = "Huh", Genre = "Piet", Duration = 4, Path = "/path/to/song" },
                new Song() { Name = "Huh", Genre = "Piet", Duration = 4, Path = "/path/to/song" },
                new Song() { Name = "Huh", Genre = "Piet", Duration = 4, Path = "/path/to/song" },
                new Song() { Name = "Huh", Genre = "Piet", Duration = 4, Path = "/path/to/song" },
                new Song() { Name = "John", Genre = "Naar huis", Duration = 56, Path = "/path/to/song" }
            };

            HitlistMenu.ItemsSource = items;
        }
    }
}