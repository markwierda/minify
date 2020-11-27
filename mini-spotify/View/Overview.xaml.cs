using mini_spotify.Controller;
using mini_spotify.DAL.Entities;
using mini_spotify.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace mini_spotify.View
{
    /// <summary>
    /// Interaction logic for Overview.xaml
    /// </summary>
    public partial class Overview : Window
    {
        private readonly HitlistController _hitlistController;
        private readonly SongController _songController;
        private readonly MediaPlayer _mediaPlayer;

        public Overview()
        {
            InitializeComponent();
            _hitlistController = new HitlistController();
            _songController = new SongController();
            _mediaPlayer = new MediaPlayer();
        }

        private void Initialize(object sender, RoutedEventArgs e)
        {
            GetAllHitList();
        }

        public void GetAllHitList()
        {
            List<Hitlist> hitlists = _hitlistController.GetHitlistsByUserId(AppData.UserId);
            HitlistMenu.ItemsSource = hitlists;
        }

        private void Btn_Add_Hitlist(object sender, RoutedEventArgs e)
        {
            AddHitlist addHitlist = new AddHitlist();
            addHitlist.Show();
            Close();
        }

        private void HitlistMenu_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Hitlist selected = (Hitlist)e.AddedItems[0];
            OverviewHitlist overviewHitlist = new OverviewHitlist(selected.Id);
            overviewHitlist.Show();
            Close();
        }

        //mediaplayer handlers

        //change play button into pause button and start music
        private void OnMouseDownPlay(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Song song = _songController.Get(new Guid("aa5ab627-3b64-4c22-9cc3-cca5fd57c896"));

            if (song != null)
            {
                btn_Play.Visibility = Visibility.Collapsed;
                btn_Pause.Visibility = Visibility.Visible;
                Play(song);
            }
        }

        public void OnMouseDownPause(object sender, RoutedEventArgs e)
        {
            //change pause button into play button
            btn_Pause.Visibility = Visibility.Collapsed;
            btn_Play.Visibility = Visibility.Visible;
        }

        private void OnMouseDownBack(object sender, RoutedEventArgs e)
        {

        }
        
        private void OnMouseDownNext(object sender, RoutedEventArgs e)
        {

        }

        private void Play(Song song)
        {
            _mediaPlayer.Open(new Uri(song.Path, UriKind.RelativeOrAbsolute));
            _mediaPlayer.Volume = 0.1;
            _mediaPlayer.Play();

            DispatcherTimer timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromMilliseconds(1)
            };

            timer.Tick += UpdateMediaplayer;
            timer.Start();
        }

        private void UpdateMediaplayer(object sender, EventArgs e)
        {
            if (_mediaPlayer.NaturalDuration.HasTimeSpan)
            {
                lbl_Current_Time.Content = _mediaPlayer.Position.ToString(@"mm\:ss");
                lbl_Song_Duration.Content = _mediaPlayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss");

                Song_Progressbar.Maximum = _mediaPlayer.NaturalDuration.TimeSpan.TotalMilliseconds;
                Song_Progressbar.Value = _mediaPlayer.Position.TotalMilliseconds;
            }
        }
    }
}
