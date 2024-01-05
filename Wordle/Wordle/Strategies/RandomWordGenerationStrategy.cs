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
            return await FetchRandomWordFromApi();
        }

        private async Task<string> FetchRandomWordFromApi()
        {
            return await WordApiService.Instance.GetInitialWordAsync();
        }
    }
}
