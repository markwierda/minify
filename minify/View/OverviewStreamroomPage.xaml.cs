using minify.Controller;
using minify.DAL.Entities;
using minify.Manager;
using minify.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace minify.View
{
    /// <summary>
    /// Interaction logic for OverviewStreamroom.xaml
    /// </summary>
    public partial class OverviewStreamroomPage : Page
    {
        private readonly Guid _streamroomId;
        private Streamroom _streamroom;
        private List<Song> _songs;
        public StreamroomManager Manager { get; set; }

        public event StreamroomRefreshedEventHandler MessagesRefreshed;

        //event for invoking messages to overview

        public OverviewStreamroomPage(Guid streamroomId)
        {
            // initialize
            _streamroomId = streamroomId;
            Manager = new StreamroomManager(streamroomId);
            Manager.StreamroomRefreshed += UpdateLocalStreamroom;
            InitializeComponent();


            HitlistController hitlistcontroller = new HitlistController();
            _streamroom = new StreamroomController().Get(streamroomId, true);

            // check if hitlist available
            if (_streamroom.Hitlist != null)
            {
                // set streamroom title, using hitlist title
                StreamroomTitle.Content = _streamroom.Hitlist.Title;

                // check if hitlist description is not null or empty
                if (!string.IsNullOrEmpty(_streamroom.Hitlist.Description))
                {
                    // set streamroom description
                    StreamroomDescription.Content = _streamroom.Hitlist.Description;
                    StreamroomDescription.Visibility = Visibility.Visible;
                }

                // sets the hitlists information in the streamroom
                StreamroomInfo.Content = hitlistcontroller.GetHitlistInfo(_streamroom.Hitlist);

                // check if songs available
                if (_streamroom.Hitlist.Songs != null && _streamroom.Hitlist.Songs.Count > 0)
                {
                    // get songs
                    _songs = hitlistcontroller.GetSongs(_streamroom.Hitlist.Songs);

                    // check if the streamroom have a current song
                    if (_streamroom.CurrentSongId != null)
                    {
                        MediaplayerController.Initialize(_songs);
                        MediaplayerController.Open(_streamroom.Song, _streamroom.CurrentSongPosition);
                        //MediaplayerController.UpdatePosition(_streamroom.CurrentSongPosition);
                    }
                    // use the first song
                    //else
                    //{
                    //    _currentSong = _songs.FirstOrDefault();
                    //    MediaplayerController.Open(_currentSong);
                    //}

                    // set the itemsource of the songs listview
                    StreamroomSongs.ItemsSource = _songs;
                    StreamroomSongs.Visibility = Visibility.Visible;

                    // highlight the current song by refreshing the listview
                    Refresh(_streamroom.Song);

                    // send join messege
                    new MessageController().CreateMessage(new Message
                    {
                        StreamroomId = streamroomId,
                        Text = $"{AppData.UserName} neemt nu deel aan de stream!",
                        UserId = AppData.UserId
                    });
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

            if(MediaplayerController.GetCurrentSong()?.Id != _streamroom.Song.Id)
            {
                MediaplayerController.Close();
            }
            //MediaplayerController.Open(_streamroom.Song, _streamroom.CurrentSongPosition);

            //TODO: set all changes to screen
            //MediaplayerController.UpdatePosition(_streamroom.CurrentSongPosition);
            
            //if (_streamroom.IsPaused)
            //{
            //    MediaplayerController.Pause();
            //}
            //else
            //{
            //    MediaplayerController.Play();
            //}

            //invoken naar overview
            MessagesRefreshed?.Invoke(this, e);
        }

        public override void EndInit()
        {
            base.EndInit();
            // start with reloading the data.
            Manager.Start();
        }

        private void StreamroomSongs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MediaplayerController.Close();

            if (e.AddedItems.Count > 0)
            {
                // Get song
                Song selectedSong = (Song)e.AddedItems[0];

                // Initialize songs
                MediaplayerController.Initialize(_songs);

                // Open song
                MediaplayerController.Open(selectedSong, TimeSpan.Zero);

                // Play song
                MediaplayerController.Play();
            }
        }

        public void Close()
        {
            Manager?.Close();

            new MessageController().CreateMessage(new Message
            {
                StreamroomId = _streamroomId,
                Text = $"{AppData.UserName} heeft de stream verlaten!",
                UserId = AppData.UserId
            });
        }

        public Guid GetStreamroomId()
        {
            return _streamroomId;
        }
    }
}