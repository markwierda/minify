using minify.Controller;
using minify.DAL.Entities;
using minify.Model;
using minify.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace minify.View

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
            HitlistController hitlistController = new HitlistController();
            var hitlists = hitlistController.GetHitlistsByUserId(AppData.UserId, true);
            Hitlists = new List<Hitlist>(hitlists);

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
