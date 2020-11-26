using mini_spotify.Controller;
using mini_spotify.DAL;
using mini_spotify.DAL.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace UnitTests
{
    public class HitlistControllerTest
    {
        private HitlistController _hitlistController;
        private Guid testId;

        [SetUp]
        public void Setup()
        {
            AppDbContext context = new AppDbContextFactory().CreateDbContext(null);
            _hitlistController = new HitlistController(context);
            testId = new Guid("{aa4cb653-3c62-5e22-5cc3-cca5fd57c846}");
        }

        [Test]
        public void GetAll_NotNull()
        {
            List<Hitlist> hitlists = _hitlistController.GetAll();

            Assert.NotNull(hitlists);
        }

        [Test]
        public void GetAll_Count_Greater_Than_Or_Equal_To_Zero()
        {
            List<Hitlist> hitlists = _hitlistController.GetAll();

            Assert.GreaterOrEqual(hitlists.Count, 0);
        }

        [Test]
        public void Find__Random_Id_IsNull()
        {
            Guid randomId = Guid.NewGuid();
            Hitlist hitlist = _hitlistController.Get(randomId);

            Assert.IsNull(hitlist);
        }

        [Test]
        public void Find_Return_IsNotNull()
        {
            Hitlist hitlist = _hitlistController.Get(testId);

            Assert.IsNotNull(hitlist);
        }

        [Test]
        public void GetHitlistInfo_WithSongsReturn_AreEqual()
        {
            // arrange
            Hitlist hitlist = _hitlistController.Get(new Guid("aa4cb653-3c62-5e22-5cc3-cca5fd57c846"), true);
            string expected = $"Created by 1140207 at {hitlist.CreatedAt:dd/MM/yyyy} - 2 songs, 9 min";;

            // act
            string result = _hitlistController.GetHitlistInfo(hitlist);

            // assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetHitlistInfo_WithoutSongsReturn_AreEqual()
        {
            // arrange
            Hitlist hitlist = _hitlistController.Get(new Guid("aa4cb653-3c62-5522-5cc3-cca5fd57c846"), true);
            string expected = $"Created by testuser at {hitlist.CreatedAt:dd/MM/yyyy} - This hitlist doesn't contain any songs yet";

            // act
            string result = _hitlistController.GetHitlistInfo(hitlist);

            // assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Find_WithRelation_False_User_IsNull()
        {
            Hitlist hitlist = _hitlistController.Get(testId);

            Assert.IsNull(hitlist.User);
        }

        [Test]
        public void Find_WithRelation_False_Songs_IsNull()
        {
            Hitlist hitlist = _hitlistController.Get(testId);

            Assert.IsNull(hitlist.Songs);
        }

        [Test]
        public void Find_WithRelation_True_User_IsNotNull()
        {
            Hitlist hitlist = _hitlistController.Get(testId, true);

            Assert.IsNotNull(hitlist.User);
        }

        [Test]
        public void Find_WithRelation_True_Songs_IsNotNull()
        {
            Hitlist hitlist = _hitlistController.Get(testId, true);

            Assert.IsNotNull(hitlist.Songs);
        }
    }
}
