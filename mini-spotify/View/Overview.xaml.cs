using mini_spotify.Controller;
using mini_spotify.DAL.Entities;
using mini_spotify.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace mini_spotify.View
{
    /// <summary>
    /// Interaction logic for Overview.xaml
    /// </summary>
    public partial class Overview : Window
    {
        private readonly HitlistController _controller;

        public Overview()
        {
            InitializeComponent();
            _controller = new HitlistController();
            MediaplayerController.UpdateMediaplayer += UpdateMediaplayer;
        }

        private void Initialize(object sender, RoutedEventArgs e)
        {
            GetAllHitList();
        }

        public void GetAllHitList()
        {
            List<Hitlist> hitlists = _controller.GetHitlistsByUserId(AppData.UserId);
            HitlistMenu.ItemsSource = hitlists;
        }

        public void UpdateHitlistMenu()
        {
            List<Hitlist> hitlists = _controller.GetHitlistsByUserId(AppData.UserId);
            HitlistMenu.ItemsSource = hitlists;
            HitlistMenu.Items.Refresh();
        }

        private void Btn_Add_Hitlist(object sender, RoutedEventArgs e)
        {
            AddHistlistPage addHitlistPage = new AddHistlistPage();
            contentFrame.Content = addHitlistPage;
            //AddHistlistPage.Show();
            //Close();
        }

        private void HitlistMenu_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Hitlist selected = (Hitlist)e.AddedItems[0];
            OverviewHitlistPage overviewHitlistpage = new OverviewHitlistPage(selected.Id);
            contentFrame.Content = overviewHitlistpage;
            //overviewHitlist.Show();
            //Close();
        }

        private void DisplayPlay()
        {
            btn_Pause.Visibility = Visibility.Collapsed;
            btn_Play.Visibility = Visibility.Visible;
        }

        private void DisplayPause()
        {
            btn_Play.Visibility = Visibility.Collapsed;
            btn_Pause.Visibility = Visibility.Visible;
        }

        private void OnMouseDownPlay(object sender, MouseButtonEventArgs e)
        {
            if (MediaplayerController.GetSource() == null)
            {
                Hitlist hitlist = _controller.Get(new Guid("aa4cb653-3c62-5e22-5cc3-cca5fd57c846"), true);
                List<Song> songs = _controller.GetSongs(hitlist.Songs);

                if (hitlist.Songs != null)
                {
                    MediaplayerController.Initialize(songs);
                    MediaplayerController.Play(songs.First());
                    DisplayPause();
                }
            } 
            else
            {
                MediaplayerController.Play();
                DisplayPause();
            }
        }

        private void OnMouseDownPause(object sender, MouseButtonEventArgs e)
        {
            MediaplayerController.Pause();
            DisplayPlay();
        }

        private void OnMouseDownBack(object sender, MouseButtonEventArgs e)
        {
            lbl_Current_Time.Content = "00:00";
            Song_Progressbar.Value = Song_Progressbar.Minimum;

            if (e.ClickCount == 1)
                MediaplayerController.Replay();
            else
            {
                if (MediaplayerController.Previous())
                    DisplayPause();
                else
                    DisplayPlay();
            }
        }
        
        private void OnMouseDownNext(object sender, MouseButtonEventArgs e)
        {
            lbl_Current_Time.Content = lbl_Song_Duration.Content;
            Song_Progressbar.Value = Song_Progressbar.Maximum;
            
            if (MediaplayerController.Next())
                DisplayPause();
            else
                DisplayPlay();
        }

        private void UpdateMediaplayer(object sender, UpdateMediaplayerEventArgs e)
        {
            if (e.SongName == null)
                DisplayPlay();

            lbl_Song_Name.Content = e.SongName;
            lbl_Artist.Content = e.Artist;
            lbl_Current_Time.Content = e.Position.ToString(@"mm\:ss");
            lbl_Song_Duration.Content = e.Duration.ToString(@"mm\:ss");
            Song_Progressbar.Maximum = e.Duration.TotalMilliseconds;
            Song_Progressbar.Value = e.Position.TotalMilliseconds;
        }
    }
}
