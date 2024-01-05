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
            return FetchDefaultWordFromAnotherSource();
        }

        private string FetchDefaultWordFromAnotherSource()
        {
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

            string randomWord = fiveLetterWords[randomIndex];
            return randomWord;
        }
    }
}
