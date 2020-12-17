using System;
using System.ComponentModel.DataAnnotations;

namespace minify.DAL.Entities
{
    public class SongVote : BaseEntity
    {
        [Required]
        public Guid StreamroomId { get; set; }

        [Required]
        public Guid SongId { get; set; }

        [Required]
        public int Votes { get; set; }
    }
}