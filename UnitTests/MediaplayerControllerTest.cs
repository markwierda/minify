using mini_spotify.Controller;
using mini_spotify.DAL.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
    [TestFixture]
    public class MediaplayerControllerTest
    {
        private SongController _controller;

        [SetUp]
        public void SetUp()
        {
            _controller = new SongController();
        }

        [Test]
        public void Play_ValidSong_ShouldOpen()
        {
            Song song = _controller.Get(new Guid("aa5ab627-3b64-4c22-9cc3-cca5fd57c896"));
            MediaplayerController.Play(song);
            Assert.IsTrue(MediaplayerController.GetSource() != null);
        }

        [Test]
        public void Play_InvalidSong_ShouldNotOpen()
        {
            Song song = _controller.Get(new Guid("12345678-1234-1234-1234-123456789012"));
            MediaplayerController.Play(song);
            Assert.IsTrue(MediaplayerController.GetSource() == null);
        }
    }
}
