using mini_spotify.Controller;
using mini_spotify.DAL;
using mini_spotify.DAL.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
    public class UserControllerTest
    {
        private readonly string testPassword = "Test!123";
        private string testPasswordHashed;

        [SetUp]
        public void Setup()
        {
            testPasswordHashed = UserController.HashPassword(testPassword);
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
