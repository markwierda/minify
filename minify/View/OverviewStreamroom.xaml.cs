using minify.Controller;
using minify.DAL.Entities;
using minify.Manager;
using minify.Model;

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
    public partial class OverviewStreamroom : Page
    {
        private readonly Guid _streamroomId;
        private Streamroom _streamroom;
        private List<Message> _messages;
        private StreamroomManager _manager;

        public event StreamroomRefreshedEventHandler MessagesRefreshed;

        //event for invoking messages to overview

        public OverviewStreamroom(Guid streamroomId)
        {
            _streamroomId = streamroomId;
            _manager = new StreamroomManager(streamroomId);
            _manager.StreamroomRefreshed += UpdateLocalStreamroom;
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

        private void UpdateLocalStreamroom(object sender, LocalStreamroomUpdatedEventArgs e)
        {
            // Get data from the updates per second from the manager.
            _streamroom = e.Streamroom;
            _messages = e.Messages;

            //TODO: set all changes to screen

            //invoken naar overview
            MessagesRefreshed?.Invoke(this, e);
        }

        public override void EndInit()
        {
            base.EndInit();
            // start with reloading the data.
            _manager.Start();
        }
    }
}
