using System;

namespace mini_spotify.DAL.Entities
{
    public class Song : BaseEntity
    {
        public string Name { get; set; }

        public string Genre { get; set; }

        public TimeSpan Duration { get; set; }

        public string Path { get; set; }
    }
}
