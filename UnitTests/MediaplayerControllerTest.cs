using mini_spotify.Controller;
using mini_spotify.DAL.Entities;
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
        private List<Song> _songs;

        [SetUp]
        public void SetUp()
        {
            _hitlistController = new HitlistController();
            _songController = new SongController();
        }

        [Test]
        public void Initialize_Songs_NotNull()
        {
            Hitlist hitlist = _hitlistController.Get(new Guid("aa4cb653-3c62-5e22-5cc3-cca5fd57c846"), true);
            _songs = _hitlistController.GetSongs(hitlist.Songs);
            MediaplayerController.Initialize(_songs);
            Assert.NotNull(MediaplayerController.Songs);
        }

        [Test]
        public void Play_ValidSong_ShouldOpen()
        {
            MediaplayerController.Close();
            Song song = _songController.Get(new Guid("aa5ab627-3b64-4c22-9cc3-cca5fd57c896"));
            MediaplayerController.Play(song);
            Assert.IsTrue(MediaplayerController.GetSource() != null);
        }

        [Test]
        public void Play_InvalidSong_ShouldNotOpen()
        {
            MediaplayerController.Close();
            Song song = _songController.Get(new Guid("12345678-1234-1234-1234-123456789012"));
            MediaplayerController.Play(song);
            Assert.IsTrue(MediaplayerController.GetSource() == null);
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
            MediaplayerController.Initialize(_songs);
            MediaplayerController.Play(_songs.First());
            MediaplayerController.Next();
            Assert.IsTrue(MediaplayerController.Previous());
        }

        [Test]
        public void Previous_SongUnavailable_IsFalse()
        {
            MediaplayerController.Initialize(_songs);
            MediaplayerController.Play(_songs.First());
            Assert.IsFalse(MediaplayerController.Previous());
        }
    }
}
