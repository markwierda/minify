using minify.Controller;
using minify.DAL.Entities;
using minify.Model;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
    public class MessageControllerTest
    {
        private MessageController messageController;
        private Guid streamRoomIdTest, userIdTest, messageIdTest;

        [SetUp]
        public void SetUp()
        {
            messageController = new MessageController();
            streamRoomIdTest = new Guid("{197a232b-4bb7-4961-9153-81349df9d785}");
            userIdTest = new Guid("{aa5ab627-3b64-5d22-8cc3-cca5fd57c896}");
            messageIdTest = Guid.NewGuid();
            AppData.Initialize();
            AppData.UserId = userIdTest;
        }

        [Test]
        public void CreateMessage_Successfull_Return_True()
        {
            Message message = new Message()
            {
                Id = Guid.NewGuid(),
                Text = "Test",
                StreamroomId = streamRoomIdTest,
                UserId = userIdTest,
            };

            Assert.IsTrue(messageController.CreateMessage(message));
        }

        [Test]
        public void CreateMessage_StreamRoomId_Is_Null_Return_False()
        {
            Message message = new Message()
            {
                Text = "Test",
                UserId = userIdTest,
            };

            Assert.IsFalse(messageController.CreateMessage(message));
        }

        [Test]
        public void CreateMessage_UserId_Is_Null_Return_False()
        {
            Message message = new Message()
            {
                Text = "Test",
                StreamroomId = streamRoomIdTest,
            };

            Assert.IsFalse(messageController.CreateMessage(message));
        }

        [Test]
        public void CreateMessage_Text_Is_Null_Or_Empty_Return_False()
        {
            Message message = new Message()
            {
                UserId = userIdTest,
                StreamroomId = streamRoomIdTest,
            };

            Assert.IsFalse(messageController.CreateMessage(message));
        }
    }
}
