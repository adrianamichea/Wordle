using System.Windows.Input;
using Wordle.View;

namespace Wordle.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {

        public ICommand PlayNewGameCommand { get; }


        public MenuViewModel()
        {
            PlayNewGameCommand = new RelayCommand(PlayNewGame);
        }

        private void PlayNewGame()
        {
            GameWindow newGameWindow = new GameWindow();
            newGameWindow.Show();
        }
    }
}
