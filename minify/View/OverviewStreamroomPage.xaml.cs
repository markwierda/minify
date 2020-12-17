using minify.Controller;
using minify.DAL.Entities;
using minify.Manager;
using minify.Model;

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

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
        private List<Song> _songs;
        private StreamroomManager _manager;

        public event StreamroomRefreshedEventHandler MessagesRefreshed;

        //event for invoking messages to overview

        public OverviewStreamroom(Guid streamroomId)
        {
            _streamroomId = streamroomId;
            _manager = new StreamroomManager(streamroomId);
            _manager.StreamroomRefreshed += UpdateLocalStreamroom;
            InitializeComponent();

            HitlistController hitlistcontroller = new HitlistController();
            _streamroom = new StreamroomController().Get(streamroomId, true);

            if (_streamroom.Hitlist != null)
            {
                StreamroomTitle.Content = _streamroom.Hitlist.Title;
                if (!string.IsNullOrEmpty(_streamroom.Hitlist.Description))
                {
                    StreamroomDescription.Content = _streamroom.Hitlist.Description;
                    StreamroomDescription.Visibility = Visibility.Visible;
                }
                StreamroomInfo.Content = hitlistcontroller.GetHitlistInfo(_streamroom.Hitlist);

                if (_streamroom.Hitlist.Songs != null && _streamroom.Hitlist.Songs.Count > 0)
                {
                    _songs = hitlistcontroller.GetSongs(_streamroom.Hitlist.Songs);
                    StreamroomSongs.ItemsSource = _songs;
                    StreamroomSongs.Visibility = Visibility.Visible;
                }
            }
        }

        public void Refresh(Song song)
        {
            StreamroomSongs.ItemsSource = _songs;

            foreach (var item in StreamroomSongs.Items)
            {
                if (((Song)item).Equals(song))
                    StreamroomSongs.SelectedItem = item;
            }

            StreamroomSongs.Visibility = Visibility.Visible;
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

        private void StreamroomSongs_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
    }
}