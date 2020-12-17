using Castle.Core.Internal;
using minify.Controller;
using minify.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace minify.View
{
    /// <summary>
    /// Interaction logic for OverviewStreamroom.xaml
    /// </summary>
    public partial class OverviewStreamroom : Window
    {
        private readonly StreamroomController _streamroomcontroller;
        private readonly Streamroom _streamroom;
        private readonly HitlistController _hitlistcontroller;
        
        public OverviewStreamroom() : this(new Guid("{197a232b-4bb7-4961-9153-81349df9d785}")) {}
        public OverviewStreamroom(Guid id)
        {
            InitializeComponent();

            _streamroomcontroller = new StreamroomController();
            _hitlistcontroller = new HitlistController();
            _streamroom = _streamroomcontroller.Get(id, true);

            if (_streamroom.Hitlist != null)
            {
                StreamroomTitle.Content = _streamroom.Hitlist.Title;
                if (!_streamroom.Hitlist.Description.IsNullOrEmpty())
                {
                    HitlistDescription.Content = _streamroom.Hitlist.Description;
                    HitlistDescription.Visibility = Visibility.Visible;
                }
                HitlistInfo.Content = _hitlistcontroller.GetHitlistInfo(_streamroom.Hitlist);

                if (_streamroom.Hitlist.Songs != null && _streamroom.Hitlist.Songs.Count > 0)
                {
                    var songs = _hitlistcontroller.GetSongs(_streamroom.Hitlist.Songs);
                    HitlistSongs.ItemsSource = songs;
                    HitlistSongs.Visibility = Visibility.Visible;
                }
            }
                /*// create instance of controller and get the hitlist by id
                _hitlistcontroller = new HitlistController();
                _hitlistSongController = new HitlistSongController();
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
                }*/
        }
    }
}
