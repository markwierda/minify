using System;

namespace minify.Model
{
    public class IsPausedEventArgs : EventArgs
    {
        public bool IsPaused { get; set; }
    }
}