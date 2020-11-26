using mini_spotify.Controller;
using mini_spotify.DAL;
using mini_spotify.DAL.Entities;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Text;

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
        public void Find_Rerturn_IsNotNull()
        {
            Hitlist hitlist = _hitlistController.Get(testId);

            Assert.IsNotNull(hitlist);
        }

        [Test]
        public void GetHitlistInfo_WithSongsReturn_AreEqual()
        {
            // arrange
            Hitlist hitlist = _hitlistController.Get(new Guid("9b0cc3c2-8df5-45bf-a0c4-05a8476443d0"), true);
            string expected = "Created by 1121300 at 24-11-2020 - 1 song, 4 min 30 sec";

            // act
            string result = _hitlistController.GetHitlistInfo(hitlist);

            // assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetHitlistInfo_WithoutSongsReturn_AreEqual()
        {
            // arrange
            Hitlist hitlist = _hitlistController.Get(new Guid("a6539bd6-c720-4999-bc1b-58f12809ef17"), true);
            string expected = "Created by testuser at 24-11-2020 - This hitlist doesn't contain any songs yet";

            // act
            string result = _hitlistController.GetHitlistInfo(hitlist);

            // assert
            Assert.AreEqual(expected, result);
        }
    }
}
