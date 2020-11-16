using System;
using System.Collections.Generic;
using System.Text;

using mini_spotify.Controller;
using mini_spotify.DAL;
using mini_spotify.DAL.Entities;

using NUnit.Framework;

namespace UnitTests
{
    public class RegisterControllerTest
    {
        private RegisterController _registerController;
        private Guid testId;

        [SetUp]
        public void Setup()
        {
            AppDbContext context = new AppDbContextFactory().CreateDbContext(null);
            _registerController = new RegisterController(context);
            testId = new Guid("{aa5ab627-3b64-4c22-9cc3-cca5fd57c896}");
        }

        [Test]
        public void GetAll_NotNull()
        {
            List<User> users = _registerController.GetAll();

            Assert.NotNull(users);
        }

        [Test]
        public void Find__Random_Id_IsNull()
        {
            Guid randomId = new Guid();
            User user = _registerController.Get(randomId);

            Assert.IsNull(user);
        }

        [Test]
        public void Find_Rerturn_IsNotNull()
        {
            User user = _registerController.Get(testId);

            Assert.IsNotNull(user);
        }
    }
}
