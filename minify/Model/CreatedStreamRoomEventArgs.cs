using minify.DAL.Entities;

using System;

namespace minify.Model
{
    public class CreatedStreamRoomEventArgs : EventArgs
    {
        public Streamroom Streamroom { get; set; }
    }
}