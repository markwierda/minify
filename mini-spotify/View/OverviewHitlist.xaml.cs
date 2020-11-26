using mini_spotify.Controller;
using mini_spotify.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Windows;

namespace mini_spotify.View
{
    /// <summary>
    /// Interaction logic for OverviewHitlist.xaml
    /// </summary>
    public partial class OverviewHitlist : Window
    {
        private readonly HitlistController _controller;

        public OverviewHitlist(Guid id)
        {
            InitializeComponent();

            _controller = new HitlistController();
            Hitlist _hitlist = _controller.Get(id, true);

            Title.Content = _hitlist.Title;
            Description.Content = _hitlist.Description;
            HitlistInfo.Content = _controller.GetHitlistInfo(_hitlist);

            if (_hitlist.Songs.Count > 0)
            {
                HitlistSongs.ItemsSource = _controller.GetSongs(_hitlist.Songs);
                HitlistSongs.Visibility = Visibility.Visible;
            }
        }
    }
}
