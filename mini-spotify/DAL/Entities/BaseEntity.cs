using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mini_spotify.DAL.Entities
{
    public abstract class BaseEntity
    {
        /// <summary>
        /// The unique identifier (Primary Key)
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
    }
}
