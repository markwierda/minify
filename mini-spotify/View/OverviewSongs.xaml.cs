using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Interaction logic for OverviewSongs.xaml
    /// </summary>
    public partial class OverviewSongs : Window
    {
        public OverviewSongs()
        {
            InitializeComponent();
            this.Title = "Hello world";
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            Title = e.GetPosition(this).ToString();

        }
    }
}
