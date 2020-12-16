using minify.Controller;
using minify.DAL.Entities;

using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
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
        private const int INTERVAL = 1000;
        private Timer timer;
        private StreamroomController _controller;
        private Streamroom streamroom;
        public OverviewStreamroom(Guid streamroomId)
        {
            InitializeComponent();
            _controller = new StreamroomController();
            streamroom = _controller.Get(streamroomId, true);
            timer = new Timer(INTERVAL)
            {
                Enabled = true
            };
            timer.Elapsed += OnTimedEvent;
        }

        public void OnTimedEvent(object obj, ElapsedEventArgs e)
        {

        }
    }
}
