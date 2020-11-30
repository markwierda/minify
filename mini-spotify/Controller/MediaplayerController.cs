using mini_spotify.DAL.Entities;
using mini_spotify.Model;
using System;
using System.Windows.Media;
using System.Windows.Threading;

namespace mini_spotify.Controller
{
    public delegate void UpdateMediaplayerEventHandler(object sender, UpdateMediaplayerEventArgs e);
    public delegate void NextSongEventHandler(object sender, EventArgs e);

    public static class MediaplayerController
    {
        private static readonly MediaPlayer _mediaPlayer = new MediaPlayer();
        private static Song _song;

        public static event UpdateMediaplayerEventHandler UpdateMediaplayer;
        public static event NextSongEventHandler NextSong;

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
                _song = song;

                _mediaPlayer.Open(new Uri(song.Path, UriKind.RelativeOrAbsolute));
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
        /// Replays a song in the mediaplayer
        /// </summary>
        public static void Replay()
        {
            _mediaPlayer.Stop();
            _mediaPlayer.Play();
        }

        public static void Previous()
        {
            // TODO
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
                        _song.Name,
                        _song.Artist,
                        _mediaPlayer.Position, 
                        _mediaPlayer.NaturalDuration.TimeSpan
                    )
                );
        }

        private static void MediaEnded(object sender, EventArgs e)
        {
            _mediaPlayer.Close();
            NextSong?.Invoke(null, new EventArgs());
        }
    }
}
