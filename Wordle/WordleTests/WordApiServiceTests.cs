using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using Wordle.Services;


namespace WordleTests
{
    [TestClass]
    public class WordApiServiceTests
    {
        [TestMethod]
        public async Task ValidateWord_ValidWord_ReturnsTrue()
        {
            // Arrange
            bool? isValidWord = await WordApiService.Instance.ValidateWord("valid");


            // Assert
            Assert.IsTrue(isValidWord.HasValue && isValidWord.Value);
        }

        [TestMethod]
        public async Task ValidateWord_ValidWord_ReturnsFalse()
        {
            // Arrange
            bool? isValidWord = await WordApiService.Instance.ValidateWord("adsads");


            // Assert
            Assert.IsFalse(isValidWord.HasValue && isValidWord.Value);
        }

        [TestMethod]
        public async Task ValidateWord_GetInitialWord()
        {
            string word = await WordApiService.Instance.GetInitialWordAsync();

            Assert.IsTrue(word.Length == 5);
        }
    }
}
