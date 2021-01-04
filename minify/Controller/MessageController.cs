using Microsoft.EntityFrameworkCore;

using minify.DAL;
using minify.DAL.Entities;
using minify.DAL.Repositories;
using minify.Model;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace minify.Controller
{
    public class MessageController
    {
        private readonly Repository<Message> _messageRepository;

        /// <summary>
        /// Initialize an instance of <see cref="MessageController"/> class.
        /// </summary>
        public MessageController() : this(new AppDbContextFactory().CreateDbContext()) { }

        /// <summary>
        /// Initialize an instance of <see cref="MessageController"/> class with an <see cref="AppDbContext"/> instance.
        /// </summary>
        /// <param name="context">The <see cref="AppDbContext"/> instance this controller will work with</param>
        public MessageController(AppDbContext context)
        {
            _messageRepository = new Repository<Message>(context);
        }

        /// <summary>
        /// Gets all the messages
        /// </summary>
        /// <param name="streamroom"></param>
        /// <returns></returns>
        public List<Message> GetMessages(Streamroom streamroom)
        {
            var query = _messageRepository
                            .GetAll()
                            .Include(m => m.User)
                            .OrderBy(m => m.CreatedAt);
                               

            return query
                    .Where(message => message.StreamroomId == streamroom.Id)
                    .ToList();
        }

        /// <summary>
        /// Gets one specific message by the Global Unique identifier(GUID)
        /// </summary>
        /// <param name="messageId">the id of the message</param>
        /// <returns>The message found if the message belongs to the signed in user, otherwise null</returns>
        public Message GetMessage(Guid messageId)
        {
            Message message = _messageRepository.Find(messageId);

            if (message == null)
            {
                return null;
            }

            return BelongsEntityToUser(message.UserId) ? message : null;
        }

        /// <summary>
        /// Creates a message.
        /// </summary>
        /// <param name="message">The message to be created</param>
        /// <returns>True, when the message is created successfully, False otherwise</returns>
        public bool CreateMessage(Message message)
        {
            if (!BelongsEntityToUser(message.UserId))
            {
                return false;
            }

            if (string.IsNullOrEmpty(message.Text) || message.Text.Length > 140)
            {
                return false;
            }

            if (message.StreamroomId == new Guid())
            {
                return false;
            }

            try
            {
                _messageRepository.Add(message);
                return SaveChangesSuccesfull();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        /// <summary>
        /// Deletes a message.
        /// </summary>
        /// <param name="message">The message to be deleted</param>
        /// <returns>True, when the message is deleted successfully, False otherwise</returns>
        public bool DeleteMessage(Message message)
        {
            if (!BelongsEntityToUser(message.UserId))
            {
                return false;
            }

            try
            {
                _messageRepository.Remove(message);
                return SaveChangesSuccesfull();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        /// <summary>
        /// Checks if the Message belongs to the user that is signed in.
        /// </summary>
        /// <param name="userId">The userid of the message</param>
        /// <returns>True, if the message belongs to the signed in user, false otherwise</returns>
        private bool BelongsEntityToUser(Guid userId)
        {
            return userId != new Guid() && userId == AppData.UserId;
        }

        /// <summary>
        /// Checks if the changes are successfully saved.
        /// </summary>
        /// <returns>True, when the changes are successfully saved, false otherwise </returns>
        private bool SaveChangesSuccesfull()
        {
            return _messageRepository.SaveChanges() > 0;
        }
    }
}