using minify.Controller;
using minify.DAL.Entities;
using minify.Model;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;

namespace minify.Manager
{
    public delegate void StreamroomRefreshedEventHandler(object sender, LocalStreamroomUpdatedEventArgs e);

    public delegate void StreamroomIsPausedToggledEventHandler(object sender, IsPausedEventArgs e);

    public class StreamroomManager
    {
        private const int INTERVAL = 1000;
        private readonly Timer _timer;
        private readonly Guid _streamroomId;
        private Streamroom _streamroom;
        private List<Message> _messages;

        public StreamroomRefreshedEventHandler StreamroomRefreshed;
        public StreamroomIsPausedToggledEventHandler IsPausedToggled;

        public StreamroomManager(Guid streamroomId)
        {
            _messages = new List<Message>();
            _streamroomId = streamroomId;
            LoadData();

            _timer = new Timer(INTERVAL);
            _timer.Enabled = true;
            _timer.Elapsed += OnTimedEvent;
        }

        public void OnTimedEvent(object obj, ElapsedEventArgs e)
        {
            if (_streamroom.Hitlist.UserId == AppData.UserId)
            {
                UpdateData();
            }
            LoadData();
            StreamroomRefreshed?.Invoke(this, new LocalStreamroomUpdatedEventArgs(_streamroom, _messages));
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

        private void UpdateData()
        {
            if (_streamroom.Hitlist.UserId == AppData.UserId && MediaplayerController.CheckAccess())
            {
                _streamroom.CurrentSongPosition = MediaplayerController.GetPosition();
                _streamroom.CurrentSongId = MediaplayerController.GetCurrentSong().Id;
                Update();
            }
        }

        private void LoadData()
        {
            _streamroom = new StreamroomController().Get(_streamroomId, true);
            _messages = new MessageController().GetMessages(_streamroom);

            Debug.WriteLine($"Position song: {_streamroom.CurrentSongPosition}");
            Debug.WriteLine($"amount of messages: {_messages.Count}");
        }
    }
}