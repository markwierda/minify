using mini_spotify.Controller;
using mini_spotify.DAL.Entities;
using mini_spotify.Model;
using System;
using System.Collections.Generic;
using System.Windows;

namespace mini_spotify.View
{
    /// <summary>
    /// Interaction logic for Overview.xaml
    /// </summary>
    public partial class Overview : Window
    {

        private HitlistController _hitlistController;
        public Overview()
        {
            InitializeComponent();
            _hitlistController =new HitlistController();
        }

        private void Initialize(object sender, RoutedEventArgs e)
        {
            GetAllHitList();
        }

        public void GetAllHitList()
        {
            //TODO: Add a check if given list is empty
            /*List<Song> items = new List<Song>
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
            };*/

            List<Hitlist> hitlists = _hitlistController.GetHitlistsByUserId(AppData.Id);

            HitlistMenu.ItemsSource = hitlists;

        }

        private void btn_Add_Hitlist(object sender, RoutedEventArgs e)
        {

        }
    }
}