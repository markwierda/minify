using Castle.Core.Internal;
using mini_spotify.DAL;
using mini_spotify.DAL.Entities;
using mini_spotify.DAL.Repositories;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace mini_spotify.Controller
{
    public static class RegisterConntroller
    {
        private static Repository<User> _userRepository;

        // create a song repository with the context
        public static void Initialize(AppDbContext context)
        {
            _userRepository = new Repository<User>(context);
        }

        public static bool Validate(string firstName, string lastName, string username, string email, string password, string confirmPassword)
        {
            // check if firstName is null or empty
            if (firstName.IsNullOrEmpty())
                return false;

            // check if username is not unique
            if (!IsUniqueUsername(username))
                return false;

            // check if email is not valid
            if (!IsValidEmail(email))
                return false;

            // check if password does not equels confirmPassword
            if (!password.Equals(confirmPassword))
                return false;

            // check if password is not valid
            if (!IsValidPassword(password))
                return false;

            return true;
        }

        // returns if given username is unique
        private static bool IsUniqueUsername(string username)
        {
            return !_userRepository.Any(u => u.UserName.Equals(username));
        }

        // returns if given email is valid (offical function from docs.microsoft.com)
        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        // returns if given password is valid
        private static bool IsValidPassword(string password)
        {
            // check if lenght is between 8 and 32
            var hasLenght = new Regex(@".{8,32}");

            // check if password contains a number
            var hasNumber = new Regex(@"[0-9]+");

            // check if password contains a special char
            var hasSpecialChar = new Regex(@"(?=.*[*.!@$%^&(){}[]:;<>,.?\/~_+-=|\])");

            // return true or false
            return hasLenght.IsMatch(password) && hasNumber.IsMatch(password) && hasSpecialChar.IsMatch(password);
        }
    }
}
