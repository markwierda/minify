using minify.Controller;
using minify.DAL.Entities;
using minify.Model;

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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace minify.View
{

    /// <summary>
    /// Interaction logic for OverviewStreamroom.xaml
    /// </summary>
    public partial class OverviewStreamroom : Page
    {
        private readonly Guid _streamroomId;
        private Streamroom _streamroom;
        private StreamroomManager _manager;

        public OverviewStreamroom(Guid streamroomId)
        {
            _streamroomId = streamroomId;
            _manager = new StreamroomManager(streamroomId);
            _manager.StreamroomRefreshed += UpdateLocalStreamroom;
            InitializeComponent();
        }

        private void UpdateLocalStreamroom(object sender, LocalStreamroomUpdatedEventArgs e)
        {
            _streamroom = e.Streamroom;

            //TODO set all changes to screen
        }
    }
}
