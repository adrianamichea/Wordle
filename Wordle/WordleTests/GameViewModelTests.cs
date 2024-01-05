using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using System.Windows;
using Wordle.Models;
using Wordle.Services;
using Wordle.ViewModels;

namespace WordleTests
{
    [TestClass]
    public class GameViewModelTests
    {
        private GameViewModel gameViewModel;

        [TestInitialize]
        public void Setup()
        {
            // Initialize the GameViewModel instance before each test
            gameViewModel = new GameViewModel();

        }


        [TestMethod]
        public void UpdateAttemptsArray_WordAddedToAttempts()
        {
            // Arrange
            var word = "ABCDE"; 
            gameViewModel.GameEntity = new GameEntity();
            gameViewModel.UpdateAttemptsArray(word);
            

            CollectionAssert.Contains(gameViewModel.GameEntity.Attempts, word);
        }

        [TestMethod]
        public void UpdateAttemptsArray_CorrectLength_WordAddedToAttempts()
        {
            // Arrange
            var word = "APPLE"; // 5 letters
            gameViewModel.GameEntity = new GameEntity();

            // Act
            gameViewModel.UpdateAttemptsArray(word);

            // Assert
            CollectionAssert.Contains(gameViewModel.GameEntity.Attempts, word);
        }

    }
}
