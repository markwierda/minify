using mini_spotify.Controller;
using mini_spotify.DAL;
using NUnit.Framework;

namespace UnitTests
{
    public class LoginControllerTest
    {

        private LoginController _loginController;
        
        [SetUp]
        public void SetUp()
        {
            AppDbContext context = new AppDbContextFactory().CreateDbContext(null);
            _loginController = new LoginController(context);
        }


        [Test]
        public void Validation_UserName_IsNullOrEmpty_ReturnFalse()
        {
            // Assemble

            string username = "";
            string password = "notempty";

            // Act
            var LoginResults = _loginController.Validation(username, password);


            // Assert
            Assert.IsFalse(LoginResults);
        }


        [Test]
        public void Validation_PassWord_IsNullOrEmpty_ReturnFalse()
        {
            // Assemble

            string username = "notempty";
            string password = "";

            // Act
            var LoginResults = _loginController.Validation(username, password);


            // Assert
            Assert.IsFalse(LoginResults);
        }
        [Test]
        public void Validation_Credentials_ReturnTrue()
        {
            // Assemble

            string username = "1140207";
            string password = "Test123";

            // Act
            var LoginResults = _loginController.Validation(username, password);


            // Assert
            Assert.IsTrue(LoginResults);
        }

        [Test]
        public void Validation_WrongPasswoord_ReturnFalse()
        {
            // Assemble

            string username = "1140207";
            string password = "tess123";

            // Act
            var LoginResults = _loginController.Validation(username, password);


            // Assert
            Assert.IsFalse(LoginResults);
        }

        [Test]
        public void Validation_Password_ToLower_ReturnFalse()
        {
            // Assemble

            string username = "MijnUniekeUsernam";
            string password = "test123";

            // Act
            var LoginResults = _loginController.Validation(username, password.ToLower());


            // Assert
            Assert.IsFalse(LoginResults);
        }


        [Test]
        public void Validation_Password_ToUpper_ReturnFalse()
        {
            // Assemble

            string username = "MijnUniekeUsernam";
            string password = "test123";

            // Act
            var LoginResults = _loginController.Validation(username, password.ToUpper());


            // Assert
            Assert.IsFalse(LoginResults);
        }

        [Test]
        public void Validation_Password_WithOthersusername_ReturnFalse()
        {
            // Assemble

            string username = "Ronald";
            string password = "Password";

            // Act
            var LoginResults = _loginController.Validation(username, password);


            // Assert
            Assert.IsFalse(LoginResults);
        }
        [Test]
        public void TryLogin_Successfully()
        {
            // Assemble

            string username = "1140207";
            string password = "Test123";

            // Act
            var LoginResults = _loginController.TryLogin(username, password);


            // Assert
            Assert.IsTrue(LoginResults);
        }
        [Test]
        public void TryLogin_UnSuccessfully()
        {
            // Assemble

            string username = "Ronald";
            string password = "Pasord";

            // Act
            var LoginResults = _loginController.TryLogin(username, password);


            // Assert
            Assert.IsFalse(LoginResults);
        }
    }
}
