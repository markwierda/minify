using Castle.Core.Internal;
using minify.Controller;
using minify.DAL.Entities;
using minify.Managers;
using minify.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace minify.View
{
    public delegate void RefreshHitlistOverview(object sender, EventArgs e);

    /// <summary>
    /// Interaction logic for OverviewHitlistPage.xaml
    /// </summary>
    public partial class OverviewHitlistPage : Page
    {
        private readonly Hitlist _hitlist;
        private List<Song> _songs;

        public event RefreshHitlistOverview RefreshHitlistOverview;
        public OverviewHitlistPage(Guid id)
        {
            InitializeComponent();

            // create instance of controller and get the hitlist by id
            HitlistController hitlistController = new HitlistController();
            _hitlist = hitlistController.Get(id, true);

            // check if hitlist is not null
            if (_hitlist != null)
            {
                // set the title, description and the info in the overview
                HitlistTitle.Content = _hitlist.Title;
                if (!_hitlist.Description.IsNullOrEmpty())
                {
                    HitlistDescription.Content = _hitlist.Description;
                    HitlistDescription.Visibility = Visibility.Visible;
                }
                HitlistInfo.Content = hitlistController.GetHitlistInfo(_hitlist);

                // if there are songs, display the listview
                if (_hitlist.Songs != null && _hitlist.Songs.Count > 0)
                {
                    _songs = hitlistController.GetSongs(_hitlist.Songs);
                    HitlistSongs.ItemsSource = _songs;
                    HitlistSongs.Visibility = Visibility.Visible;
                }

                if(_hitlist.UserId == AppData.UserId)
                {
                    DeleteHitlist.Visibility = Visibility.Visible;
                }
            }
        }

        public void Refresh(Song song)
        {
            HitlistController hitlistController = new HitlistController();
            _songs = hitlistController.GetSongs(_hitlist.Songs);
            HitlistSongs.ItemsSource = _songs;
            
            foreach (var item in HitlistSongs.Items)
            {
                if (((Song)item).Equals(song))
                    HitlistSongs.SelectedItem = item;


            }

            HitlistSongs.Visibility = Visibility.Visible;
        }

        private void HitlistSongs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                // Get song
                Song selectedSong = (Song)e.AddedItems[0];

                // Initialize songs
                MediaplayerController.Initialize(_songs);

                // Open song
                MediaplayerController.Open(selectedSong);

                // Play song
                MediaplayerController.Play();
            }
        }

        private void btn_delete_click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure?",
                                             "Confirmation",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                HitlistController hitlistController = new HitlistController();
                hitlistController.Delete(_hitlist);
                MessageBox.Show("Hitlist Deleted", "Success");
                RefreshHitlistOverview.Invoke(this, new EventArgs());
            }
        }
    }
}