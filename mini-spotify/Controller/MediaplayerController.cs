using mini_spotify.DAL.Entities;
using mini_spotify.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows.Threading;

namespace mini_spotify.Controller
{
    public delegate void UpdateMediaplayerEventHandler(object sender, UpdateMediaplayerEventArgs e);

    public static class MediaplayerController
    {
        private static readonly MediaPlayer _mediaPlayer = new MediaPlayer();
        private static Song _currentSong;

        public static List<Song> Songs { get; private set; }
        public static event UpdateMediaplayerEventHandler UpdateMediaplayer;

        /// <summary>
        /// Initialize the Songs variable
        /// </summary>
        /// <param name="songs"></param>
        public static void Initialize(List<Song> songs)
        {
            Songs = songs;
        }

        /// <summary>
        /// Starts playing a song in the mediaplayer
        /// </summary>
        /// <param name="song"></param>
        public static void Play(Song song = null)
        {
            if (song == null)
            {
                _mediaPlayer.Play();
            }
            else
            {
                _currentSong = song;

                _mediaPlayer.Open(new Uri(_currentSong.Path, UriKind.RelativeOrAbsolute));
                _mediaPlayer.MediaEnded += MediaEnded;
                _mediaPlayer.Play();

                DispatcherTimer timer = new DispatcherTimer()
                {
                    Interval = TimeSpan.FromMilliseconds(1)
                };

                timer.Tick += Update;
                timer.Start();
            }
        }

        /// <summary>
        /// Pauses a song in the mediaplayer
        /// </summary>
        public static void Pause()
        {
            _mediaPlayer.Pause();
        }

        /// <summary>
        /// Starts playing the next song in the mediaplayer
        /// </summary>
        /// <returns>Returns true if there is a next song and false if there is no next song</returns>
        public static bool Next()
        {
            if (Songs.Last() == _currentSong)
            {
                Close();
                return false;
            }

            int index = Songs.FindIndex(x => x == _currentSong);
            _currentSong = Songs[index + 1];
            Play(_currentSong);
            return true;
        }

        /// <summary>
        /// Starts playing the previous song in the mediaplayer
        /// </summary>
        /// <returns>Returns true if there is a previous song and false if there is no previous song</returns>
        public static bool Previous()
        {
            if (Songs.First() == _currentSong)
            {
                Close();
                return false;
            }

            int index = Songs.FindIndex(x => x == _currentSong);
            _currentSong = Songs[index - 1];
            Play(_currentSong);
            return true;
        }

        /// <summary>
        /// Replays a song in the mediaplayer
        /// </summary>
        public static void Replay()
        {
            _mediaPlayer.Stop();
            _mediaPlayer.Play();
        }

        /// <summary>
        /// Closes the underlying media
        /// </summary>
        public static void Close()
        {
            _mediaPlayer.Close();
        }

        /// <summary>
        /// Returns the mediaplayer's current source
        /// </summary>
        /// <returns>Mediaplayer's source</returns>
        public static Uri GetSource()
        {
            return _mediaPlayer.Source;
        }

        /// <summary>
        /// Invoke the event handler for updating the mediaplayer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Update(object sender, EventArgs e)
        {
            if (_mediaPlayer.NaturalDuration.HasTimeSpan)
                UpdateMediaplayer?.Invoke(null, 
                    new UpdateMediaplayerEventArgs(
                        _currentSong.Name,
                        _currentSong.Artist,
                        _mediaPlayer.Position, 
                        _mediaPlayer.NaturalDuration.TimeSpan
                    )
                );
        }

        /// <summary>
        /// Event handler to start the next song if the song ended
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MediaEnded(object sender, EventArgs e)
        {
            _mediaPlayer.Close();

            if (Songs.Last() != _currentSong)
                Next();
            else
                _currentSong = Songs.First();

            UpdateMediaplayer?.Invoke(null,
                new UpdateMediaplayerEventArgs()
            );
        }
    }
}
