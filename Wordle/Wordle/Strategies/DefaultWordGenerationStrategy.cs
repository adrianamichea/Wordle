using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wordle.Interfaces;

namespace Wordle.Strategies
{
    public class DefaultWordGenerationStrategy: IWordGenerationStrategy
    {
        public async Task<string> GetInitialWordAsync()
        {
            // Default logic to get a random word
            // Example: Fetch from a different source
            return FetchDefaultWordFromAnotherSource();
        }

        private string FetchDefaultWordFromAnotherSource()
        {
            // Implementation to fetch a default word from another source
            string[] fiveLetterWords = {
    "apple",
    "zebra",
    "chair",
    "queen",
    "laser",
    "table",
    "truck",
    "smile",
    "music",
    "grape"
    
};
            Random random = new Random();
            int randomIndex = random.Next(0, fiveLetterWords.Length);

            // Get the random word
            string randomWord = fiveLetterWords[randomIndex];
            return randomWord;
        }
    }
}
