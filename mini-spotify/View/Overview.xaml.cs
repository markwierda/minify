using mini_spotify.Controller;
using mini_spotify.DAL.Entities;
using mini_spotify.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows;

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

        //mediaplayer handlers

        public void OnMouseDownPlay(object sender, RoutedEventArgs e)
        {
            //change play button into pause button
            btn_Play.Visibility = Visibility.Collapsed;
            btn_Pause.Visibility = Visibility.Visible;
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
    }
}
