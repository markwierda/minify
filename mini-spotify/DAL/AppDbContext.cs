using Microsoft.EntityFrameworkCore;

using mini_spotify.DAL.Entities;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace mini_spotify.DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<Song> Songs { get; set; }

        public DbSet<Stream> Streams { get; set; }

        public DbSet<User> Users { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }

        #region Required
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<User>(user =>
            {
                user.HasIndex(ts => ts.UserName).IsUnique();
            });

            Song[] songs = new Song[]
            {
                new Song() { Id = new Guid("{aa5ab627-3b64-4c22-9cc3-cca5fd57c896}"), Name = "Titanic", Duration = 5, Genre = "Classic", Path = "." },
                new Song() { Id = Guid.NewGuid(), Name = "Low(feat. T-Pain", Duration = 4, Genre = "Rap", Path = "." },
            };


            builder.Entity<Song>().HasData(songs);


        }
        #endregion
    }
}
