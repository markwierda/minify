using Castle.Core.Internal;
using minify.Controller;
using minify.DAL.Entities;
using minify.Managers;
using minify.Model;
using System;
using System.Windows;
using System.Windows.Controls;

namespace minify.View
{
    /// <summary>
    /// Interaction logic for OverviewHitlistPage.xaml
    /// </summary>
    public partial class OverviewHitlistPage : Page
    {
        private readonly HitlistController _hitlistcontroller;
        private readonly Hitlist _hitlist;
        
        public OverviewHitlistPage(Guid id)
        {
            InitializeComponent();

            // create instance of controller and get the hitlist by id
            _hitlistcontroller = ControllerManager.Get<HitlistController>();
            _hitlist = _hitlistcontroller.Get(id, true);

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
                HitlistInfo.Content = _hitlistcontroller.GetHitlistInfo(_hitlist);

                // if there are songs, display the listview
                if (_hitlist.Songs != null && _hitlist.Songs.Count > 0)
                {
                    HitlistSongs.ItemsSource = _hitlistcontroller.GetSongs(_hitlist.Songs);
                    HitlistSongs.Visibility = Visibility.Visible;
                }
                if (AppData.UserId == _hitlist.UserId)
                {
                    DeleteHitlist.Visibility = Visibility.Visible;
                }
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
                _hitlistcontroller.Delete(_hitlist);
                MessageBox.Show("Hitlist Deleted", "Success");
                _hitlistcontroller.RefreshHitlistOverview();
            }
        }
    }
}