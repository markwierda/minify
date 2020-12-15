using mini_spotify.Controller;
using mini_spotify.DAL.Entities;
using mini_spotify.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace mini_spotify.View

{
    public class IdRetreivedEventArgs : EventArgs
    {
        public Guid HitlistId { get; set; }
        public Guid SongId { get; set; }
    }

    public delegate void IdRetreived(object sender, EventArgs e);

    /// <summary>
    /// Interaction logic for ChooseHitlistDialog.xaml
    /// </summary>
    public partial class ChooseHitlistDialog : Window
    {
        private HitlistController _hitlistController;
        private Guid songId; 
        public List<Hitlist> Hitlists { get; set; }
        public Hitlist hitlist;
        public IdRetreived IdRetreived { get; set; }
        public OverviewSongsPage _page;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="songId"></param>
        public ChooseHitlistDialog(Guid songId, OverviewSongsPage overviewSongsPage)
        {
            _page = overviewSongsPage;

            this.songId = songId;
            InitializeComponent();
            DataContext = this;
            _hitlistController = new HitlistController();
            var hitlists = _hitlistController.GetHitlistsByUserId(AppData.UserId, true);
            Hitlists = new List<Hitlist>(hitlists);
            //TODO: filter de hitlists die deze songid al hebben eruit.

            var removeHitlists = new List<Hitlist>();
            foreach (Hitlist hitlist in hitlists)
            {

                if (hitlist == null)
                {
                    continue;
                }

                if (hitlist.Songs.Any(x => x.SongId == songId))
                {
                    Hitlists.Remove(hitlist);

                }
               
            }
        }
        private void AddSongToHitlist(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbxHitlist.SelectedValue != null)
                {
                    Guid hitlistId = (Guid)cbxHitlist.SelectedValue;

                    IdRetreived.Invoke(this, new IdRetreivedEventArgs
                    {
                        HitlistId = hitlistId,
                        SongId = songId
                    });
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Geen hitlijst gekozen");
                }
            }
           
            catch(Exception)
            {
                MessageBox.Show("Onbekende fout opgetreden.");
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _page.Refresh();
        }
    }
}
