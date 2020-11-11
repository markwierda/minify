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

            builder.Entity<Song>();

            builder.Entity<User>(user =>
            {
                user.HasIndex(ts => ts.UserName).IsUnique();
            });

            //builder.Entity<Stream>()
            //        .HasOne(s => s.Song);
        }
        #endregion
    }
}
