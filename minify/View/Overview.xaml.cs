using minify.Controller;
using minify.DAL;
using minify.DAL.Entities;
using minify.Managers;
using minify.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace minify.View
{
    /// <summary>
    /// Interaction logic for Overview.xaml
    /// </summary>
    public partial class Overview : Window
    {
        private TimeSpan _positionCache;

        private OverviewHitlistPage _overviewHitlistPage;
        private AddHistlistPage _addHitlistPage;
        private OverviewSongsPage _overviewSongsPage;

        public Overview()
        {
            MediaplayerController.UpdateMediaplayer += UpdateMediaplayer;

            _addHitlistPage = new AddHistlistPage();
            _addHitlistPage.HitlistAdded += UpdateHitlistMenu;

            InitializeComponent();
        }

        public void RefreshHitListMenu(object sender, EventArgs e)
        {
            InitializeHitListMenu();
            HitlistMenu.Items.Refresh();

            OverviewSongsPage overviewSongs = new OverviewSongsPage();
            contentFrame.Content = overviewSongs;
        }

        public void InitializeHitListMenu()
        {
            List<Hitlist> hitlists = new HitlistController().GetHitlistsByUserId(AppData.UserId);
            HitlistMenu.ItemsSource = hitlists;
        }

        public void UpdateHitlistMenu(object sender, UpdateHitlistMenuEventArgs e)
        {
            InitializeHitListMenu();
            HitlistMenu.Items.Refresh();

            //display current hitlist
            OverviewHitlistPage overview = new OverviewHitlistPage(e.Id);
            contentFrame.Content = overview;
        }

        private void Btn_Add_Hitlist(object sender, RoutedEventArgs e)
        {
            AddHistlistPage addHitlistPage = _addHitlistPage;
            contentFrame.Content = addHitlistPage;
        }

        private void HitlistMenu_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Hitlist selected = (Hitlist)e.AddedItems[0];
                _overviewHitlistPage = CreateOverviewHitlistPage(selected.Id);
                contentFrame.Content = _overviewHitlistPage;
            }
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
            MediaplayerController.Play();
            DisplayPause();
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

            if (_overviewHitlistPage != null)
                _overviewHitlistPage.Refresh(MediaplayerController.GetCurrentSong());

            if (_overviewSongsPage != null)
                _overviewSongsPage.Refresh(MediaplayerController.GetCurrentSong());
        }

        private void OnMouseDownNext(object sender, MouseButtonEventArgs e)
        {
            lbl_Current_Time.Content = lbl_Song_Duration.Content;
            Song_Progressbar.Value = Song_Progressbar.Maximum;

            if (MediaplayerController.Next())
                DisplayPause();
            else
                DisplayPlay();

            if (_overviewHitlistPage != null) 
                _overviewHitlistPage.Refresh(MediaplayerController.GetCurrentSong());

            if (_overviewSongsPage != null)
                _overviewSongsPage.Refresh(MediaplayerController.GetCurrentSong());
        }

        private void UpdateMediaplayer(object sender, UpdateMediaplayerEventArgs e)
        {
            if (e.Position > _positionCache)
                DisplayPause();
            else
                DisplayPlay();

            _positionCache = e.Position;

            lbl_Song_Name.Content = e.SongName;
            lbl_Artist.Content = e.Artist;
            lbl_Current_Time.Content = e.Position.ToString(@"mm\:ss");
            lbl_Song_Duration.Content = e.Duration.ToString(@"mm\:ss");
            Song_Progressbar.Maximum = e.Duration.TotalMilliseconds;
            Song_Progressbar.Value = e.Position.TotalMilliseconds;

            if (e.SongName == null)
            {
                if (_overviewHitlistPage != null)
                    _overviewHitlistPage.Refresh(MediaplayerController.GetCurrentSong());

                if (_overviewSongsPage != null)
                    _overviewSongsPage.Refresh(MediaplayerController.GetCurrentSong());
            }
        }

        private void Btn_home(object sender, RoutedEventArgs e)
        {
            Overview overview = new Overview();
            overview.Show();
            Close();
        }

        private void Btn_songs(object sender, RoutedEventArgs e)
        {
            InitializeHitListMenu();
            _overviewSongsPage = new OverviewSongsPage();
            contentFrame.Content = _overviewSongsPage;
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            InitializeHitListMenu();
            label_Username.Content = AppData.UserName;
        }

        private void Btn_Logout(object sender, RoutedEventArgs e)
        {
            new LoginController().Logout();
            MediaplayerController.Close();
            Login login = new Login();
            login.Show();
            Close();
        }

        private void SearchBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if(Search.Text != "Search..." && Search.Text != "")
            {
                var songs = new SongController().Search(Search.Text);
                if(songs != null && songs.Count > 0)
                {
                    OverviewSongsPage overviewSongs = new OverviewSongsPage(songs);
                    contentFrame.Content = overviewSongs;
                }
                else
                {
                    Label label = new Label();
                    label.Content = "No songs could be found";
                    contentFrame.Content = label;
                }
            }
        }

        private void Search_MouseEnter(object sender, MouseEventArgs e)
        {
            if(Search.Text == "Search...")
            {
                Search.Text = "";
            }
        }

        private OverviewHitlistPage CreateOverviewHitlistPage(Guid id)
        {
            _overviewHitlistPage = new OverviewHitlistPage(id);
            _overviewHitlistPage.RefreshHitlistOverview += RefreshHitListMenu;

            return _overviewHitlistPage;
        }
    }
}