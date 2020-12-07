using mini_spotify.Model;
using NUnit.Framework;
using System;

namespace UnitTests
{
    [TestFixture]
    public class AppDataTest
    {
        [Test]
        public void Initialize_LoggedIn_IsFalse()
        {
            AppData.Initialize();
            Assert.IsFalse(AppData.LoggedIn);
        }

        [Test]
        public void Initialize_UserId_AreEqual()
        {
            AppData.Initialize();
            Assert.AreEqual(AppData.UserId, Guid.Empty);
        }

        [Test]
        public void Initialize_UserName_IsNull()
        {
            AppData.Initialize();
            Assert.IsNull(AppData.UserName);
        }
    }
}