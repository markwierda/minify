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
        private OverviewSongsPage _overviewSongsPage;
        private OverviewHitlistPage _hitlistPage;
        private OverviewStreamroom _overviewStreamroomPage;
        private AddHistlistPage _addHitlistPage;

        private OverviewStreamroom OverviewStreamroomPage
        {
            get { return _overviewStreamroomPage;  }
            set
            {
                value.MessagesRefreshed += OverviewStreamroom_MessagesRefreshed;
                _overviewStreamroomPage = value;
            }
        }

        private AddHistlistPage AddHitlistPage 
        { 
            get { return _addHitlistPage; }
            set
            {
                value.HitlistAdded += UpdateHitlistMenu;
                _addHitlistPage = value;
            }
        }


        private OverviewHitlistPage OverviewHitlistPage
        {
            get { return _hitlistPage; }
            set
            {
                value.StreamroomCreated += OpenStreamroom;
                value.RefreshHitlistOverview += RefreshHitListMenu;
                _hitlistPage = value;
            }
        }

        public Overview()
        {
            MediaplayerController.UpdateMediaplayer += UpdateMediaplayer;


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
            OverviewHitlistPage = new OverviewHitlistPage(e.Id);

            // set the new item as selected
            foreach (var item in HitlistMenu.Items)
            {
                // cast ListViewItem to Hitlist and check if the id equals the eventargs id
                if (((Hitlist)item).Id.Equals(e.Id))
                {
                    HitlistMenu.SelectedItem = item;
                }
            }

            contentFrame.Content = OverviewHitlistPage;
        }

        private void Btn_Add_Hitlist(object sender, RoutedEventArgs e)
        {
            AddHitlistPage = new AddHistlistPage();
            contentFrame.Content = AddHitlistPage;
        }

        private void HitlistMenu_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Hitlist selected = (Hitlist)e.AddedItems[0];
                OverviewHitlistPage = new OverviewHitlistPage(selected.Id);
                contentFrame.Content = OverviewHitlistPage;
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

            if (OverviewHitlistPage != null)
                OverviewHitlistPage.Refresh(MediaplayerController.GetCurrentSong());

            if (_overviewSongsPage != null)
                _overviewSongsPage.Refresh(MediaplayerController.GetCurrentSong());

            if (_overviewStreamroomPage != null)
                _overviewStreamroomPage.Refresh(MediaplayerController.GetCurrentSong());
        }

        private void OnMouseDownNext(object sender, MouseButtonEventArgs e)
        {
            lbl_Current_Time.Content = lbl_Song_Duration.Content;
            Song_Progressbar.Value = Song_Progressbar.Maximum;

            if (MediaplayerController.Next())
                DisplayPause();
            else
                DisplayPlay();

            if (OverviewHitlistPage != null) 
                OverviewHitlistPage.Refresh(MediaplayerController.GetCurrentSong());

            if (_overviewSongsPage != null)
                _overviewSongsPage.Refresh(MediaplayerController.GetCurrentSong());

            if (_overviewStreamroomPage != null)
                _overviewStreamroomPage.Refresh(MediaplayerController.GetCurrentSong());
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
                if (OverviewHitlistPage != null)
                    OverviewHitlistPage.Refresh(MediaplayerController.GetCurrentSong());

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
                    Label label = new Label
                    {
                        Content = "No songs could be found"
                    };
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

        private void OpenStreamroom(object sender, CreatedStreamRoomEventArgs e)
        {
            OverviewStreamroomPage = new OverviewStreamroom(e.Streamroom.Id);
            contentFrame.Content = OverviewStreamroomPage;
        }

        private void OverviewStreamroom_MessagesRefreshed(object sender, LocalStreamroomUpdatedEventArgs e)
        {
            // e.Messages to your beautiful chat view
        }
    }
}