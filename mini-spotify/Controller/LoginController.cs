using Castle.Core.Internal;
using Microsoft.EntityFrameworkCore;
using mini_spotify.DAL;
using mini_spotify.DAL.Entities;
using mini_spotify.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace mini_spotify.Controller
{
    public class LoginController
    {
        private readonly Repository<User> _userRepository;


        // create a user repository with the context
        public LoginController(AppDbContext context) {

            _userRepository = new Repository<User>(context);


        }

        public bool Validation(string username, string password)
        {
            // check if username is null or empty
            if (username.IsNullOrEmpty())
                return false;
            // check if password is null or empty
            if (password.IsNullOrEmpty())
                return false;
            // check if password is valid
           foreach(User user  in _userRepository.GetAll())
           {
                if(user.UserName == username && user.PassWord == password)
                {
                    return true;
                }
           }

            return false;        
        }



        }

    }
