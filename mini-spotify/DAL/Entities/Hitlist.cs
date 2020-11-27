using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace mini_spotify.DAL.Entities
{
    public class Hitlist : BaseEntity
    {
        [Required]
        public string Title { get; set; }

        [MaxLength(140)]
        public string Description { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public User User { get; set; }

        public ICollection<HitlistSong> Songs { get; set; }
    }
}
