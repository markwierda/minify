using mini_spotify.DAL;
using mini_spotify.DAL.Entities;
using mini_spotify.DAL.Repositories;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace mini_spotify.Controller
{
    public class RegisterController
    {
        private readonly Repository<User> _userRepository;

        // create a user repository with the context
        public RegisterController(AppDbContext context)
        {
            _userRepository = new Repository<User>(context);
        }

        /// <summary>
        /// Adds a user to the database
        /// </summary>
        /// <param name="user"></param>
        public void Add(User user)
        {
            if (user.Id == null)
                throw new ArgumentNullException("id");

            user.PassWord = UserController.HashPassword(user.PassWord);

            _userRepository.Add(user);
            _userRepository.SaveChanges();
        }

        // returns if given username is unique
        public bool IsUniqueUsername(string username)
        {
            return !_userRepository.Any(u => u.UserName.Equals(username));
        }

        // returns if given email is valid (offical function from docs.microsoft.com)
        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                static string DomainMapper(Match match)
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

        // returns if password equals confirmPassword
        public bool PasswordEqualsConfirmPassword(string password, string confirmPassword)
        {
            return password.Equals(confirmPassword);
        }

        // returns if given password is valid
        public bool IsValidPassword(string password)
        {
            // check if minimal lenght is 8
            var hasLenght = new Regex(@".{8,}");

            // check if password contains a number
            var hasNumber = new Regex(@"(?=.*?[0-9])");

            // check if password contains a special char
            var hasSpecialChar = new Regex(@"(?=.*?[#?!@$%^&*-])");

            // return true or false
            return hasLenght.IsMatch(password) && hasNumber.IsMatch(password) && hasSpecialChar.IsMatch(password);
        }
    }
}
