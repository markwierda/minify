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
        public void Find__Random_Id_IsNull()
        {
            Guid randomId = new Guid();
            Hitlist hitlist = _hitlistController.Get(randomId);

            Assert.IsNull(hitlist);
        }

        [Test]
        public void Find_Rerturn_IsNotNull()
        {
            Hitlist hitlist = _hitlistController.Get(testId);

            Assert.IsNotNull(hitlist);
        }
    }
}
