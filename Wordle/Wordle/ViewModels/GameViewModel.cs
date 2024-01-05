using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows;
using Wordle.Interfaces;
using Wordle.Models;
using Wordle.Services;

namespace Wordle.ViewModels
{
    public class GameViewModel : INotifyPropertyChanged
    {
        #region private fields
        private GameEntity _gameEntity;
        private StringBuilder _userInput = new StringBuilder(5);
        private readonly IGameEntityFactory gameEntityFactory;
        private readonly IWordGenerationStrategy wordGenerationStrategy;

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

        public RelayCommand SaveGameCommand { get; }


        public GameViewModel(IGameEntityFactory gameEntityFactory, IWordGenerationStrategy wordGenerationStrategy)
        {
            this.gameEntityFactory = gameEntityFactory ?? throw new ArgumentNullException(nameof(gameEntityFactory));
            this.wordGenerationStrategy = wordGenerationStrategy ?? throw new ArgumentNullException(nameof(wordGenerationStrategy));
            InitializeGameAsync();
            UpdateUserInputCommand = new RelayCommand(UpdateUserInput);
            SaveGameCommand = new RelayCommand(SaveGame);

        }

        public GameViewModel(GameEntity gameEntity, IGameEntityFactory gameEntityFactory)
        {
            this.gameEntityFactory = gameEntityFactory ?? throw new ArgumentNullException(nameof(gameEntityFactory));
            ResumeGame(gameEntity);
            UpdateUserInputCommand = new RelayCommand(UpdateUserInput);
            SaveGameCommand = new RelayCommand(SaveGame);
        }
        public GameViewModel()
        {
        }
        #endregion

        #region methods

        public void SaveGame()
        {
            GameService gameService = new GameService();
            gameService.updateLastGamePlayedByUser(GameEntity,out errorMessage);

        }

        public async void UpdateUserInput()
        {
            Console.WriteLine("User Input", UserInput.ToString());
            try
            {
                bool? isValidWord = await WordApiService.Instance.ValidateWord(UserInput);

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

        public void CheckGameState()
        {
            if (GameEntity.Attempts.Contains(GameEntity.SecretWord.ToUpper()))
            {
                MessageBox.Show("You won!", "Congratulations", MessageBoxButton.OK, MessageBoxImage.Information);
                
            }
            else if (GameEntity.Attempts.All(a => !string.IsNullOrEmpty(a)))
            {
                MessageBox.Show("You lost!", "Game over, the secret word was " + GameEntity.SecretWord, MessageBoxButton.OK, MessageBoxImage.Information);
                
            }
        }
        public void UpdateAttemptsArray(string word)
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

        public void UpdateCodesArray(string word)
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

        public string GetCode(string word)
        {
            StringBuilder code = new StringBuilder(5);
            word = word.ToUpper();
            GameEntity.SecretWord = GameEntity.SecretWord.ToUpper();
            for (int i = 0; i < word.Length; i++)
            {
                Console.WriteLine(code.ToString());

                if (word[i] == GameEntity.SecretWord[i])
                {
                    code.Append("G");
                }
                else if (GameEntity.SecretWord.Contains(word[i]))
                {
                    int countGreenPositions = 0;
                    for (int j = i; j < word.Length; j++)
                    {
                        if (word[j] == GameEntity.SecretWord[j] && word[j] == word[i])
                        {
                            countGreenPositions++;
                        }
                    }

                    int countAlreadyFoundPartialPositions = 0;
                    for (int j = 0; j < i; j++)
                    {
                        if (word[j] == word[i] && code[j] == 'P')
                        {
                            countAlreadyFoundPartialPositions++;
                        }
                    }
                    if (countGreenPositions +countAlreadyFoundPartialPositions< GameEntity.SecretWord.Count(c => c == word[i]))
                        code.Append("P");
                    else
                        code.Append('U');
                }
                else
                {
                    code.Append("U");
                }
            }

            return code.ToString();
        }




        public void ShowErrorMessage(string message)
        {
            Console.WriteLine($"Error: {message}");
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void ResumeGame(GameEntity gameEntity)
        {
            int userID = gameEntity.UserID;
            string secretWord = gameEntity.SecretWord;
            string[] attempts = gameEntity.Attempts;
            string[] codes = gameEntity.Codes;
            if (gameEntityFactory != null)
            {
                GameEntity = (GameEntity)gameEntityFactory.ResumeGameEntity(userID, secretWord, attempts, codes);
            }
            else
            {
                Console.WriteLine("Error: gameEntityFactory is not initialized.");
            }
        }
       
        public async void InitializeGameAsync()
        {
            try
            {
                string initialWord = await wordGenerationStrategy.GetInitialWordAsync();

                if (initialWord != null)
                {
                    GameEntity = (GameEntity)gameEntityFactory.CreateGameEntity(initialWord);

                }
                else
                {
                    ErrorMessage = "Internet Error or unable to fetch the word. Please try again later.";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing game: {ex.Message}");
                ErrorMessage = "An error occurred while initializing the game.";
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
