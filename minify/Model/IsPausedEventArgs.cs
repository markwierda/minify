using System;
using System.Collections.Generic;
using System.Text;

namespace minify.Model
{
    public class IsPausedEventArgs : EventArgs
    {
        public bool IsPaused { get; set; }
    }
}
