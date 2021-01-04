using minify.Controller;
using minify.DAL.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    [TestFixture]
    public class MediaplayerControllerTest
    {
        private HitlistController _hitlistController;
        private SongController _songController;
        private Hitlist _hitlist;

        [SetUp]
        public void SetUp()
        {
            _hitlistController = new HitlistController();
            _songController = new SongController();
            _hitlist = _hitlistController.Get(new Guid("aa4cb653-3c62-5e22-5cc3-cca5fd57c846"), true);
        }

        [Test]
        public void Initialize_Songs_NotNull()
        {
            List<Song> songs = _hitlistController.GetSongs(_hitlist.Songs);
            MediaplayerController.Initialize(songs);
            Assert.NotNull(MediaplayerController.Songs);
        }

        //[Test]
        //public void Play_ValidSong_ShouldOpen()
        //{
        //    MediaplayerController.Close();
        //    Song song = _songController.Get(new Guid("aa5ab627-3b64-4c22-9cc3-cca5fd57c896"));
        //    MediaplayerController.Open(song);
        //    MediaplayerController.Play();
        //    Assert.IsTrue(MediaplayerController.GetSource() != null);
        //}

        //[Test]
        //public void Play_InvalidSong_ShouldNotOpen()
        //{
        //    MediaplayerController.Close();
        //    Song song = _songController.Get(new Guid("12345678-1234-1234-1234-123456789012"));
        //    MediaplayerController.Open(song);
        //    MediaplayerController.Play();
        //    Assert.IsTrue(MediaplayerController.GetSource() == null);
        //}

        [Test]
        public void Next_SongsEmpty_IsFalse()
        {
            MediaplayerController.Initialize(null);
            Assert.IsFalse(MediaplayerController.Next());
        }

        [Test]
        public void Next_SongAvailable_IsTrue()
        {
            Assert.IsTrue(MediaplayerController.Next());
        }

        [Test]
        public void Next_SongUnavailable_IsFalse()
        {
            MediaplayerController.Next();
            Assert.IsFalse(MediaplayerController.Next());
        }

        [Test]
        public void Previous_SongsEmpty_IsFalse()
        {
            MediaplayerController.Initialize(null);
            Assert.IsFalse(MediaplayerController.Previous());
        }

        [Test]
        public void Previous_SongAvailable_IsTrue()
        {
            List<Song> songs = _hitlistController.GetSongs(_hitlist.Songs);
            MediaplayerController.Initialize(songs);
            MediaplayerController.Open(songs.First());
            MediaplayerController.Play();
            MediaplayerController.Next();
            Assert.IsTrue(MediaplayerController.Previous());
        }

        [Test]
        public void Previous_SongUnavailable_IsFalse()
        {
            List<Song> songs = _hitlistController.GetSongs(_hitlist.Songs);
            MediaplayerController.Initialize(songs);
            MediaplayerController.Open(songs.First());
            MediaplayerController.Play();
            Assert.IsFalse(MediaplayerController.Previous());
        }

        [Test]
        public void GetCurrentSong_SongAvailable_IsNotNull()
        {
            List<Song> songs = _hitlistController.GetSongs(_hitlist.Songs);
            MediaplayerController.Initialize(songs);
            MediaplayerController.Open(songs.First());
            MediaplayerController.Play(); Assert.IsNotNull(MediaplayerController.GetCurrentSong());
        }

        [Test]
        public void GetCurrentSong_SongUnavailable_IsNull()
        {
            MediaplayerController.Open(null);
            Assert.IsNull(MediaplayerController.GetCurrentSong());
        }
    }
}