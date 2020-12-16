using minify.DAL.Entities;

using System;
using System.Collections.Generic;
using System.Text;

namespace minify.Model
{
    public class CreatedStreamRoomEventArgs : EventArgs
    {
        public Streamroom Streamroom { get; set; }
    }
}
