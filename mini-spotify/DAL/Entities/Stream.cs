using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace mini_spotify.DAL.Entities
{
    public class Stream : BaseEntity
    {
        public Song Song { get; set; }
    }
}
