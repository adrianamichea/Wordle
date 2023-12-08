using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Wordle.Models;

namespace Wordle.ViewModels
{
    public class GameViewModel : INotifyPropertyChanged
    {

        private string[] _attempts = new string[6];

        public string[] Attempts
        {
            get { return _attempts; }
            set
            {
                if (_attempts != value)
                {
                    _attempts = value;
                    OnPropertyChanged(nameof(Attempts));
                }
            }
        }



        private readonly string api = "https://api.dictionaryapi.dev/api/v2/entries/en/";

        private StringBuilder _userInput = new StringBuilder(5);

        public string UserInput
        {
            get { return _userInput.ToString(); }
            set
            {
                if (_userInput.ToString() != value)
                {
                    _userInput.Clear();
                    _userInput.Append(value);
                    OnPropertyChanged(nameof(UserInput));
                }
            }
        }
        public RelayCommand UpdateUserInputCommand { get; }



        public GameViewModel()
        {
            InitializeGameAsync();
            UpdateUserInputCommand = new RelayCommand(UpdateUserInput);

        }

        private async void UpdateUserInput()
        {
            try
            {
                bool? isValidWord = await ValidateWord(UserInput);

                if (isValidWord.HasValue)
                {
                    if (isValidWord.Value)
                    {
                        UpdateAttemptsArray(UserInput);
                    }
                    else
                    {
                        ShowErrorMessage("Invalid word. Please enter a valid five-letter word.");
                    }
                }
                else
                {
                    ShowErrorMessage("An error occurred during word validation. Please try again.");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error updating user input: {ex.Message}");
            }
        }

        private void UpdateAttemptsArray(string word)
        {

            for (int i = 0; i < _attempts.Length; i++)
            {
                if (string.IsNullOrEmpty(_attempts[i]))
                {
                    _attempts[i] = word;
                    OnPropertyChanged(nameof(Attempts));
                    break;
                }
            }
            UserInput = string.Empty;
        }

        private void ShowErrorMessage(string message)
        {
            Console.WriteLine($"Error: {message}");
        }


        private async Task<bool?> ValidateWord(string word)
        {
            try
            {
                string apiEndpoint = $"{api}{word}";

                using (HttpClient client = new HttpClient())
                {
                    string response = await client.GetStringAsync(apiEndpoint);
                    var definitions = JsonConvert.DeserializeObject<JArray>(response);

                    if (definitions.Count > 0)
                    {
                        // string retrievedWord = definitions[0]["word"].ToString();
                        //string partOfSpeech = definitions[0]["meanings"][0]["partOfSpeech"].ToString();
                        //string definition = definitions[0]["meanings"][0]["definitions"][0]["definition"].ToString();
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

        private async void InitializeGameAsync()
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
                        string initialWord = words[0].ToUpper();
                        // model = new Game(initialWord);
                        // DisplayWord = model.DisplayWord; // Set DisplayWord property
                        DisplayWord = initialWord;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching word from API: {ex.Message}");

                ErrorMessage = "Internet Error, please try again later";


            }
        }

        private string _displayWord;
        public string DisplayWord
        {
            get { return _displayWord; }
            set
            {
                if (_displayWord != value)
                {
                    _displayWord = value;
                    OnPropertyChanged(nameof(DisplayWord));
                }
            }
        }
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                if (errorMessage != value)
                {
                    errorMessage = value;
                    OnPropertyChanged(nameof(ErrorMessage));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
