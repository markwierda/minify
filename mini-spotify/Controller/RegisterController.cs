using mini_spotify.DAL;
using mini_spotify.DAL.Entities;
using mini_spotify.DAL.Repositories;

using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;

namespace mini_spotify.Controller
{
    public class RegisterController
    {
        private readonly Repository<User> _registerRepository;

        public RegisterController(AppDbContext context)
        {
            // create a song repository with the context.
            _registerRepository = new Repository<User>(context);
        }
        /// <summary>
        /// Adds a user to the database
        /// </summary>
        /// <param name="user"></param>
        public void Add(User user)
        {
            if (user.Id == null)
            {
                throw new ArgumentNullException("id");
            }

            //TO DO: check on values of required properties. 
            // For more information check the Acceptation Criteria.

            user.PassWord = UserController.HashPassword(user.PassWord);

            _registerRepository.Add(user);
            _registerRepository.SaveChanges();
        }

        public User Get(Guid randomId)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
