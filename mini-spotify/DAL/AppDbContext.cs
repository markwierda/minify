using Microsoft.EntityFrameworkCore;
using mini_spotify.Controller;
using mini_spotify.DAL.Entities;
using System;

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

            User[] users = new User[]
            {
                new User() { Id = new Guid("{aa5ab627-3b64-5d22-8cc3-cca5fd57c896}"), Email = "s1140207@student.windesheim.nl", FirstName = "Ronald", LastName="Haan", PassWord=UserController.HashPassword("Test123"), UserName="1140207" },
                new User() { Id = new Guid("{aa5ab653-3b62-5e22-5cc3-cca5fd57c846}"), Email = "s1121300@student.windesheim.nl", FirstName = "Ali", LastName="Alkhalil", PassWord=UserController.HashPassword("Password"), UserName="1121300" },
                new User() { Id = Guid.NewGuid(), Email="Test@user.com", FirstName = "test", LastName = "User", PassWord = UserController.HashPassword("Test123"), UserName = "testuser" }
            };

            builder.Entity<Song>().HasData(songs);
            builder.Entity<User>().HasData(users);



        }
        #endregion
    }
}
