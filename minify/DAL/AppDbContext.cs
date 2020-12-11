using Microsoft.EntityFrameworkCore;
using minify.Controller;
using minify.DAL.Entities;
using System;

namespace minify.DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<Song> Songs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Hitlist> Hitlists { get; set; }
        public DbSet<HitlistSong> HitlistSongs { get; set; }
        public DbSet<Streamroom> Streamrooms { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<SongVote> SongVotes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        #region Required

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.UseCollation("SQL_Latin1_General_CP1_CS_AS");

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
                    .HasIndex(hs => new { hs.SongId, hs.HitlistId })
                    .IsUnique();

                // create a one to many relation from hitlistSongs.Song to Song
                hitlistsongs
                    .HasOne(hs => hs.Song)
                    .WithMany(s => s.Hitlists)
                    .HasForeignKey(hs => hs.SongId)
                    // When the relation is deleted, do not delete the song.
                    .OnDelete(DeleteBehavior.Cascade);

                // create a one to many relation from hitlistSongs.Hitlist to Hitlist
                hitlistsongs
                    .HasOne(hs => hs.Hitlist)
                    .WithMany(hl => hl.Songs)
                    .HasForeignKey(hs => hs.HitlistId)
                    // When the relation is deleted, do not delete the hitlist.
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Streamroom>(streamroom => {
                streamroom
                    .HasKey(s => s.Id);

                // create a one to one relation from Streamroom.HitlistId to Hitlist
                streamroom
                    .HasOne(r => r.Hitlist)
                    .WithOne()
                    .HasForeignKey<Streamroom>(h => h.HitlistId);

                // create a one to one relation from Streamroom.SongId to Song
                streamroom
                    .HasOne(r => r.Song)
                    .WithOne()
                    .HasForeignKey<Streamroom>(s => s.CurrentSongId);
            });

            builder.Entity<Message>(message => {
                message
                    .HasKey(s => s.Id);

                // create a one to one relation from Message.UserId to User
                message
                    .HasOne(m => m.User)
                    .WithOne()
                    .HasForeignKey<Message>(u => u.UserId);
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
                new Hitlist() { Id = new Guid("{aa4cb653-3c62-5e22-5cc3-cca5fd57c846}"), Title = "Unieke playlist1", UserId = users[0].Id, Description = "Description"},
                new Hitlist() { Id = new Guid("{aa3cb653-3c62-5e22-5cc3-cca5fd57c846}"), Title = "Unieke playlist2", UserId = users[1].Id,  Description = "Description" },
                new Hitlist() { Id = new Guid("{aa4cb653-3c62-5522-5cc3-cca5fd57c846}"), Title = "HUH", UserId = users[2].Id, Description = "HUH"},
            };

            HitlistSong[] hitlistSongs = new HitlistSong[]
            {
                new HitlistSong { Id = Guid.NewGuid(), SongId = songs[0].Id, HitlistId = hitlists[0].Id },
                new HitlistSong { Id = Guid.NewGuid(), SongId = songs[1].Id, HitlistId = hitlists[0].Id },
                new HitlistSong { Id = Guid.NewGuid(), SongId = songs[0].Id, HitlistId = hitlists[1].Id },
            };

            Streamroom[] streamrooms = new Streamroom[]
            {
                new Streamroom { Id = new Guid("{197a232b-4bb7-4961-9153-81349df9d785}"), HitlistId = hitlists[0].Id, CurrentSongId = songs[0].Id, CurrentSongPosition = new TimeSpan(0, 0, 0), IsPaused = false },
            };

            Message[] messages = new Message[]
            {
                new Message { Id = Guid.NewGuid(), StreamroomId = streamrooms[0].Id, UserId = users[0].Id, Text = "Huh naar huis?" },
            };

            SongVote[] songVotes = new SongVote[]
            {
                new SongVote { Id = Guid.NewGuid(), StreamroomId = streamrooms[0].Id, SongId = songs[0].Id, Votes = 1 },
            };

            builder.Entity<Song>().HasData(songs);
            builder.Entity<User>().HasData(users);
            builder.Entity<Hitlist>().HasData(hitlists);
            builder.Entity<HitlistSong>().HasData(hitlistSongs);
            builder.Entity<Streamroom>().HasData(streamrooms);
            builder.Entity<Message>().HasData(messages);
            builder.Entity<SongVote>().HasData(songVotes);

            #endregion Seed
        }

        #endregion Required
    }
}