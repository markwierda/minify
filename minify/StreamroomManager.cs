using minify.Controller;
using minify.DAL.Entities;
using minify.Model;

using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace minify
{
    public delegate void StreamroomRefreshedEventHandler(object sender, LocalStreamroomUpdatedEventArgs e);
    public class StreamroomManager
    {
        private const int INTERVAL = 1000;
        private readonly Timer _timer;
        private readonly Guid _streamroomId;
        private Streamroom _streamroom;

        public StreamroomRefreshedEventHandler StreamroomRefreshed;

        public StreamroomManager(Guid streamroomId)
        {
            _streamroomId = streamroomId;
            _streamroom = Get();

            _timer = new Timer(INTERVAL);
            _timer.Enabled = true;
            _timer.Elapsed += OnTimedEvent;
        }

        public void OnTimedEvent(object obj, ElapsedEventArgs e)
        {
            _streamroom = Get();
            StreamroomRefreshed.Invoke(this, new LocalStreamroomUpdatedEventArgs { Streamroom = _streamroom });
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        public void Pause()
        {
            if (_streamroom.Id == null)
                throw new ArgumentNullException("id");


            _streamroom.IsPaused = true;
            Update();
        }


        public void Play()
        {
            if (_streamroom.Id == null)
                throw new ArgumentNullException("id");


            _streamroom.IsPaused = false;
            Update();
        }

        public bool IsPaused()
        {
            return _streamroom.IsPaused;
        }

        private void Update()
        {
            new StreamroomController().Update(_streamroom);
        }

        private Streamroom Get()
        {
            return new StreamroomController().Get(_streamroomId);
        }
    }
}
