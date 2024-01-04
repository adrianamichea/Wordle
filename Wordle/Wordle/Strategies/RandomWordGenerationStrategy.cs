using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wordle.Interfaces;
using Wordle.Services;

namespace Wordle.Strategies
{
    public class RandomWordGenerationStrategy: IWordGenerationStrategy
    {
        public async Task<string> GetInitialWordAsync()
        {
            // Logic to get a random word from a specific source
            // Example: Fetch from an API
            return await FetchRandomWordFromApi();
        }

        private async Task<string> FetchRandomWordFromApi()
        {
            // Implementation to fetch a random word from an API
            return await WordApiService.Instance.GetInitialWordAsync();
        }
    }
}
