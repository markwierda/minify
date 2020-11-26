using mini_spotify.Controller;
using mini_spotify.DAL;
using mini_spotify.DAL.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace UnitTests
{
    public class SongControlerTest
    {
        private SongController _songController;
        private Guid testId;
        
        [SetUp]
        public void Setup()
        {
            AppDbContext context = new AppDbContextFactory().CreateDbContext(null);
            _songController = new SongController(context);
            testId = new Guid("{aa5ab627-3b64-4c22-9cc3-cca5fd57c896}");
        }

        [Test]
        public void GetAll_NotNull()
        {
            List<Song> songs = _songController.GetAll();

            Assert.NotNull(songs);
        }

        [Test]
        public void Find__Random_Id_IsNull()
        {
            Guid randomId = new Guid();
            Song song = _songController.Get(randomId);

            Assert.IsNull(song);
        }

        [Test]
        public void Get_By_Id_Rerturn_IsNotNull()
        {
            Song song = _songController.Get(testId);

            Assert.IsNotNull(song);
        }
    }
}
