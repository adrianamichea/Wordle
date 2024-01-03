using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Wordle.Models;

namespace Wordle.ViewModels
{
    public class GameViewModel : INotifyPropertyChanged
    {
        #region private fields
        private GameEntity _gameEntity;
        private readonly string api = "https://api.dictionaryapi.dev/api/v2/entries/en/";
        private StringBuilder _userInput = new StringBuilder(5);
        #endregion

        #region public fields
        public GameEntity GameEntity
        {
            get { return _gameEntity; }
            set
            {
                if (_gameEntity != value)
                {
                    _gameEntity = value;
                    OnPropertyChanged(nameof(GameEntity));
                }
            }
        }
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
        #endregion

        #region methods

        private async void UpdateUserInput()
        {
            try
            {
                bool? isValidWord = await ValidateWord(UserInput);

                if (isValidWord.HasValue)
                {
                    if (isValidWord.Value)
                    {
                        UpdateAttemptsArray(UserInput.ToUpper());
                        UpdateCodesArray(UserInput.ToUpper());
                        CheckGameState();
                        UserInput = string.Empty;
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

        private void CheckGameState()
        {
            if (GameEntity.Attempts.Contains(GameEntity.SecretWord.ToUpper()))
            {
                MessageBox.Show("You won!", "Congratulations", MessageBoxButton.OK, MessageBoxImage.Information);
                
            }
            else if (GameEntity.Attempts.All(a => !string.IsNullOrEmpty(a)))
            {
                MessageBox.Show("You lost!", "Game over", MessageBoxButton.OK, MessageBoxImage.Information);
                
            }
        }
        private void UpdateAttemptsArray(string word)
        {

            for (int i = 0; i < GameEntity.Attempts.Length; i++)
            {
                if (string.IsNullOrEmpty(GameEntity.Attempts[i]))
                {
                    GameEntity.Attempts[i] = word;
                    OnPropertyChanged(nameof(GameEntity.Attempts));
                    break;
                }
            } 
        }

        private void UpdateCodesArray(string word)
        {
           
            for (int i = 0; i < GameEntity.Codes.Length; i++)
            {
                if (string.IsNullOrEmpty(GameEntity.Codes[i]))
                {
                    GameEntity.Codes[i] = GetCode(word);
                    OnPropertyChanged(nameof(GameEntity));
                    break;
                }
            }
            OnPropertyChanged(nameof(GameEntity));
        }

        private string GetCode(string word)
        {
            Console.WriteLine($"Display word: {GameEntity.SecretWord}");
            Console.WriteLine($"Word: {word}");
            StringBuilder code = new StringBuilder(5);
            word = word.ToUpper();
            GameEntity.SecretWord = GameEntity.SecretWord.ToUpper();
            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == GameEntity.SecretWord[i])
                {
                    code.Append("G");
                }
                else if (GameEntity.SecretWord.Contains(word[i]))
                {
                    code.Append("P");
                }
                else
                {
                    code.Append("U");
                }
            }

            Console.WriteLine(code.ToString());
            return code.ToString();
        }

        private void ShowErrorMessage(string message)
        {
            Console.WriteLine($"Error: {message}");
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                        GameEntity = new GameEntity(initialWord);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching word from API: {ex.Message}");

                ErrorMessage = "Internet Error, please try again later";


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

        #endregion
    }
}
