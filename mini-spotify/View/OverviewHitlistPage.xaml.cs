using Castle.Core.Internal;
using mini_spotify.Controller;
using mini_spotify.DAL.Entities;
using System;
using System.Windows;
using System.Windows.Controls;

namespace mini_spotify.View
{
    /// <summary>
    /// Interaction logic for OverviewHitlistPage.xaml
    /// </summary>
    public partial class OverviewHitlistPage : Page
    {
        private HitlistController _controller;

        public OverviewHitlistPage(Guid id)
        {
            InitializeComponent();
            // create instance of controller and get the hitlist by id
            _controller = new HitlistController();
            Hitlist _hitlist = _controller.Get(id, true);

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
                HitlistInfo.Content = _controller.GetHitlistInfo(_hitlist);

                // if there are songs, display the listview
                if (_hitlist.Songs != null && _hitlist.Songs.Count > 0)
                {
                    HitlistSongs.ItemsSource = _controller.GetSongs(_hitlist.Songs);
                    HitlistSongs.Visibility = Visibility.Visible;
                }
            }
        }
    }
}