using System;
using System.Text;

namespace mini_spotify.DAL.Entities
{
    public class Song : BaseEntity
    {
        public string Name { get; set; }

        public string Genre { get; set; }

        public int Duration { get; set; }

        public string Path { get; set; }
    }
}
