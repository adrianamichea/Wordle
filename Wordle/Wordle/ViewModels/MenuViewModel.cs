using System;
using System.Windows;
using System.Windows.Input;
using Wordle.Interfaces;
using Wordle.Models;
using Wordle.Services;
using Wordle.View;

namespace Wordle.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {

        public ICommand PlayNewGameCommand { get; }

        public ICommand ResumeLastGameCommand { get; }


        public MenuViewModel()
        {
            PlayNewGameCommand = new RelayCommand(PlayNewGame);
            ResumeLastGameCommand = new RelayCommand(ResumeLastGame);
        }

        private void PlayNewGame()
        {
            Console.WriteLine("Authenticated user id din playnewgame: " + AuthentificationService.Instance.GetAuthenticatedUserId());

            GameWindow newGameWindow = new GameWindow();
            newGameWindow.Show();
        }

        private void ResumeLastGame()
        {
            string message = "";
            GameService gameService= new GameService();
            int userId = (int)AuthentificationService.Instance.GetAuthenticatedUserId();
            GameEntity lastGameEntity = gameService.getLastGamePlayedByUser(userId,out message);
            var gameEntityFactory = new GameEntityFactory();


            if (lastGameEntity != null)
            {
                GameViewModel gameViewModel = new GameViewModel(lastGameEntity, gameEntityFactory);

                GameWindow newGameWindow = new GameWindow
                {
                    DataContext = gameViewModel
                };
                newGameWindow.Show();
            }
            else
            {
                MessageBox.Show("No last game available.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
