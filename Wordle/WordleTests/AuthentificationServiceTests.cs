using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Wordle.Services;

namespace WordleTests
{
    [TestClass]
    public class AuthentificationServiceTests
    {
            [TestMethod]
            [DataRow("user1", "parola1", true, null)] 
            [DataRow("invalidUsername", "invalidPassword", false, "Authentication failed. Invalid username or password.")] 
            public void Authenticate_ValidAndInvalidCredentials_ReturnsExpectedResult(
                string username, string password, bool expectedResult, string expectedErrorMessage)
            {

                // Act
                bool result = AuthentificationService.Instance.Authenticate(username, password, out string errorMessage);

                // Assert
                Assert.AreEqual(expectedResult, result);
                Assert.AreEqual(expectedErrorMessage, errorMessage);
            }

        [TestMethod]
        [DataRow("newUsername", "newPassword", true, null)] 
        public void Register_ValidAndInvalidUsernames_ReturnsExpectedResult(
           string username, string password, bool expectedResult, string expectedErrorMessage)
        {
            // Act
            bool result = AuthentificationService.Instance.Register(username, password, out string errorMessage);

            // Assert
            Assert.AreEqual(expectedResult, result);
            Assert.AreEqual(expectedErrorMessage, errorMessage);
        }

        [TestMethod]
        [DataRow("user1", true)] // Existing username
        [DataRow("nonExistingUser", false)] // Non-existing username
        public void GetUserId_Username_ReturnsExpectedResult(string username, bool expectedResult)
        {
            

            // Act
            int userId = AuthentificationService.Instance.getUserId(username);

            // Assert
            if (expectedResult)
            {
                Assert.AreEqual(1, userId); 
            }
            else
            {
                Assert.AreEqual(0, userId);
            }
        }
    }
}

