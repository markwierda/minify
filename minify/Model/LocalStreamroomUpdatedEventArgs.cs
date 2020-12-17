using minify.DAL.Entities;

using System;
using System.Collections.Generic;
using System.Text;

namespace minify.Model
{
    public class LocalStreamroomUpdatedEventArgs : EventArgs
    {
        public Streamroom Streamroom { get; set; }

        public List<Message> Messages { get; set; }

        public LocalStreamroomUpdatedEventArgs(Streamroom streamroom, List<Message> messages)
        {
            Streamroom = streamroom;
            Messages = messages;
        }
    }
}
