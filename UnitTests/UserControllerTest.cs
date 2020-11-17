using mini_spotify.Controller;
using NUnit.Framework;

namespace UnitTests
{
    public class UserControllerTest
    {
        private readonly string testPassword = "Test!123";
        private readonly string testPasswordHashed = "p+eUD89OFO/VOk+Ca1Qq+0w1pyp8A6maF/u/gQrH+Icp3GQp";

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void HashPassword_Return_NotNull()
        {
           string hash = UserController.HashPassword(testPassword);

            Assert.NotNull(hash);
        }

        [Test]
        public void ValidatePassword_Return_True()
        {
            bool hash = UserController.ValidatePassword(testPassword, testPasswordHashed);

            Assert.IsTrue(hash);
        }

        [Test]
        public void ValidatePassword_Return_False()
        {
            bool hash = UserController.ValidatePassword("AnderWachtwoord:)" , testPasswordHashed);

            Assert.IsFalse(hash);
        }
    }
}
