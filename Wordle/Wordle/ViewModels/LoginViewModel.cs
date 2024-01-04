using System.Windows.Input;
using System.Windows;
using Wordle.Services;
using Wordle.View;
namespace Wordle.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {

        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }

        private AuthentificationService authService =AuthentificationService.Instance;

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login);
            RegisterCommand = new RelayCommand(Register);
        }

        public ICommand RegisterCommand { get; }

        public LoginViewModel(string username, string password)
        {
            Username = username;
            Password = password;
            
        }

        private void Login()
        {

            bool isAuthenticated = authService.Authenticate(Username, Password, out string errorMessage);

            if (isAuthenticated)
            {
                    MenuWindow menuWindow= new MenuWindow();
                    menuWindow.Show();
            }
            else
            {
                MessageBox.Show(errorMessage, "Authentication Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Register()
        {
            if (Username == null || Password == null)
            {
                MessageBox.Show("Please enter a username and password.", "Registration Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                bool isRegistered = authService.Register(Username, Password, out string errorMessage);
                if (isRegistered)
                {
                    MessageBox.Show("Registration successful.", "Registration", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show(errorMessage, "Registration Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            
        }


    }
}
