using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Wordle.Services
{
    public class WordApiService
    {
        private static readonly Lazy<WordApiService> lazyInstance =
            new Lazy<WordApiService>(() => new WordApiService());

        public static WordApiService Instance => lazyInstance.Value;

        private readonly string apiEndpoint = "https://api.dictionaryapi.dev/api/v2/entries/en/";

        private WordApiService()
        {
           
        }

        public async Task<bool?> ValidateWord(string word)
        {
            try
            {
                string apiEndpointForWord = $"{apiEndpoint}{word}";

                using (HttpClient client = new HttpClient())
                {
                    string response = await client.GetStringAsync(apiEndpointForWord);
                    var definitions = JsonConvert.DeserializeObject<JArray>(response);

                    if (definitions.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"No definitions found for the word '{word}'.");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error validating word: {ex.Message}");
                return null;
            }
        }

        public async Task<string> GetInitialWordAsync()
        {
            try
            {
                string apiEndpoint = "https://random-word-api.herokuapp.com/word?length=5";

                using (HttpClient client = new HttpClient())
                {
                    string response = await client.GetStringAsync(apiEndpoint);
                    var words = JsonConvert.DeserializeObject<List<string>>(response);

                    if (words != null && words.Count > 0)
                    {
                        return words[0].ToUpper();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching word from API: {ex.Message}");
            }

            return null;
        }

    }
}
