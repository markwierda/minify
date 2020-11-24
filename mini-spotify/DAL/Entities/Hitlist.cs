using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace mini_spotify.DAL.Entities
{
    public class Hitlist : BaseEntity
    {
        [Required]
        public string Title { get; set; }

        [MaxLength(140)]
        public string Description { get; set; }

        [Required]
        public User User { get; set; }

        public ICollection<HitlistSong> Songs { get; set; }
    }
}
