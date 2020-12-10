using mini_spotify.DAL.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace minify.DAL.Entities
{
    public class Streamroom : BaseEntity
    {
        [Required]
        public Guid HitlistId { get; set; }

        [Required]
        public Guid CurrentSongId { get; set; }

        [Required]
        public TimeSpan CurrentSongPosition { get; set; }

        [Required]
        public bool IsPaused { get; set; }

        public Song Song { get; set; }

        public Hitlist Hitlist { get; set; }
    }
}
