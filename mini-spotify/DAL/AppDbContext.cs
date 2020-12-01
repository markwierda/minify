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
        public DbSet<Hitlist> Hitlists { get; set; }
        public DbSet<HitlistSong> HitlistSongs { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        #region Required
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(user =>
            {
                user.HasIndex(ts => ts.UserName).IsUnique();
            });

            builder.Entity<HitlistSong>(hitlistsongs =>
            {
                hitlistsongs
                    .HasKey(hs => hs.Id);
                // create a unique constraint with the songid and hitlistid
                hitlistsongs
                    .HasIndex(hs => new { hs.SongId, hs.HitlistId})
                    .IsUnique();

                // create a one to many relation from hitlistSongs.Song to Song
                hitlistsongs
                    .HasOne(hs => hs.Song)
                    .WithMany(s => s.Hitlists)
                    .HasForeignKey(hs => hs.SongId)
                    // When the relation is deleted, do not delete the song.
                    .OnDelete(DeleteBehavior.Restrict);

                // create a one to many relation from hitlistSongs.Hitlist to Hitlist
                hitlistsongs
                    .HasOne(hs => hs.Hitlist)
                    .WithMany(hl => hl.Songs)
                    .HasForeignKey(hs => hs.HitlistId)
                    // When the relation is deleted, do not delete the hitlist.
                    .OnDelete(DeleteBehavior.Restrict);
            });

            #region Seed
            Song[] songs = new Song[]
            {
                new Song() { Id = new Guid("{aa5ab627-3b64-4c22-9cc3-cca5fd57c896}"), Artist = "G-Eazy & Halsey", Name = "Him & I", Duration = new TimeSpan(0, 0, 4, 40), Genre = "Rap", Path = "Music/G-Eazy & Halsey - Him & I.mp3" },
                new Song() { Id = new Guid("{aa5ab677-3b64-4c22-9cc3-cca5fd57c896}"), Artist = "James Arthur", Name = "Say You Wont Let Go", Duration = new TimeSpan(0, 0, 3, 30), Genre = "Pop", Path = "Music/James Arthur - Say You Wont Let Go.mp3" }
            };

            User[] users = new User[]
            {
                new User() { Id = new Guid("{aa5ab627-3b64-5d22-8cc3-cca5fd57c896}"), Email = "s1140207@student.windesheim.nl", FirstName = "Ronald", LastName="Haan", PassWord=UserController.HashPassword("Test123"), UserName="1140207" },
                new User() { Id = new Guid("{aa5ab653-3b62-5e22-5cc3-cca5fd57c846}"), Email = "s1121300@student.windesheim.nl", FirstName = "Ali", LastName="Alkhalil", PassWord=UserController.HashPassword("Password"), UserName="1121300" },
                new User() { Id = Guid.NewGuid(), Email="Test@user.com", FirstName = "test", LastName = "User", PassWord = UserController.HashPassword("Test123"), UserName = "testuser" }
            };

            Hitlist[] hitlists = new Hitlist[]
            {
                new Hitlist() { Id = new Guid("{aa4cb653-3c62-5e22-5cc3-cca5fd57c846}"), Title = "Unieke playlist", UserId = users[0].Id, Description = "Description"},
                new Hitlist() { Id = new Guid("{aa3cb653-3c62-5e22-5cc3-cca5fd57c846}"), Title = "Unieke playlist", UserId = users[1].Id,  Description = "Description" },
                new Hitlist() { Id = new Guid("{aa4cb653-3c62-5522-5cc3-cca5fd57c846}"), Title = "HUH", UserId = users[2].Id, Description = "HUH"},
            };

            HitlistSong[] hitlistSongs = new HitlistSong[]
            {
                new HitlistSong { Id = Guid.NewGuid(), SongId = songs[0].Id, HitlistId = hitlists[0].Id },
                new HitlistSong { Id = Guid.NewGuid(), SongId = songs[1].Id, HitlistId = hitlists[0].Id },
                new HitlistSong { Id = Guid.NewGuid(), SongId = songs[0].Id, HitlistId = hitlists[1].Id },
            };

            builder.Entity<Song>().HasData(songs);
            builder.Entity<User>().HasData(users);
            builder.Entity<Hitlist>().HasData(hitlists);
            builder.Entity<HitlistSong>().HasData(hitlistSongs);
            #endregion Seed
        }
        #endregion
    }
}
