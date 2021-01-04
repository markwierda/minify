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
        private HitlistController _controller;
        private Hitlist _hitlist;

        [SetUp]
        public void SetUp()
        {
            _controller = new HitlistController();
            _hitlist = _controller.Get(new Guid("aa4cb653-3c62-5e22-5cc3-cca5fd57c846"), true);
        }

        [Test]
        public void Initialize_Songs_NotNull()
        {
            List<Song> songs = _controller.GetSongs(_hitlist.Songs);
            MediaplayerController.Initialize(songs);
            Assert.NotNull(MediaplayerController.Songs);
        }

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
            List<Song> songs = _controller.GetSongs(_hitlist.Songs);
            MediaplayerController.Initialize(songs);
            MediaplayerController.Open(songs.First(), TimeSpan.Zero);
            MediaplayerController.Play();
            MediaplayerController.Next();
            Assert.IsTrue(MediaplayerController.Previous());
        }

        [Test]
        public void Previous_SongUnavailable_IsFalse()
        {
            List<Song> songs = _controller.GetSongs(_hitlist.Songs);
            MediaplayerController.Initialize(songs);
            MediaplayerController.Open(songs.First(), TimeSpan.Zero);
            MediaplayerController.Play();
            Assert.IsFalse(MediaplayerController.Previous());
        }

        [Test]
        public void GetCurrentSong_SongAvailable_IsNotNull()
        {
            List<Song> songs = _controller.GetSongs(_hitlist.Songs);
            MediaplayerController.Initialize(songs);
            MediaplayerController.Open(songs.First(), TimeSpan.Zero);
            MediaplayerController.Play(); Assert.IsNotNull(MediaplayerController.GetCurrentSong());
        }

        [Test]
        public void GetCurrentSong_SongUnavailable_IsNull()
        {
            MediaplayerController.Open(null, TimeSpan.Zero);
            Assert.IsNull(MediaplayerController.GetCurrentSong());
        }
    }
}