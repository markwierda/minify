using minify.Controller;
using minify.DAL.Entities;
using minify.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace minify.View
{
    /// <summary>
    /// Interaction logic for Overview.xaml
    /// </summary>
    public partial class Overview : Window
    {
        private readonly HitlistController _hitlistController;
        private readonly LoginController _loginController;
        private TimeSpan _positionCache;

        private OverviewHitlistPage _overviewHitlistPage;
        private OverviewSongsPage _overviewSongsPage;

        public Overview()
        {
            _hitlistController = new HitlistController();
            _loginController = new LoginController();
            MediaplayerController.UpdateMediaplayer += UpdateMediaplayer;
            _hitlistController.HitlistAdded += UpdateHitlistMenu;
            InitializeComponent();
        }

        public void GetAllHitList()
        {
            List<Hitlist> hitlists = _hitlistController.GetHitlistsByUserId(AppData.UserId);
            HitlistMenu.ItemsSource = hitlists;
        }

        public void UpdateHitlistMenu(object sender, UpdateHitlistMenuEventArgs e)
        {
            List<Hitlist> hitlists = _hitlistController.GetHitlistsByUserId(AppData.UserId);
            HitlistMenu.ItemsSource = hitlists;
            HitlistMenu.Items.Refresh();

            //display current hitlist
            _overviewHitlistPage = new OverviewHitlistPage(e.Id);

            // set the new item as selected
            foreach (var item in HitlistMenu.Items)
            {
                // cast ListViewItem to Hitlist and check if the id equals the eventargs id
                if (((Hitlist)item).Id.Equals(e.Id))
                {
                    HitlistMenu.SelectedItem = item;
                }
            }

            contentFrame.Content = _overviewHitlistPage;
        }

        private void Btn_Add_Hitlist(object sender, RoutedEventArgs e)
        {
            AddHistlistPage addHitlistPage = new AddHistlistPage(_hitlistController);
            contentFrame.Content = addHitlistPage;
        }

        private void HitlistMenu_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Hitlist selected = (Hitlist)e.AddedItems[0];
                _overviewHitlistPage = new OverviewHitlistPage(selected.Id);
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
            _overviewSongsPage = new OverviewSongsPage();
            contentFrame.Content = _overviewSongsPage;
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            GetAllHitList();
            label_Username.Content = AppData.UserName;
        }

        private void Btn_Logout(object sender, RoutedEventArgs e)
        {
            _loginController.Logout();
            Login login = new Login();
            login.Show();
            Close();
        }
    }
}