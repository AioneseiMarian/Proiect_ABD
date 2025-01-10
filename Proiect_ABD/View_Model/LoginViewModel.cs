using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Proiect_ABD.View_Model
{
    public class LoginViewModel : ViewModelBase
    {
        private string _username;
        private string _password;
        public event Action<LoginViewModel> RequestClose;
        private bool _isAuthenticated = false;

        public ICommand LoginCommand { get; }
        public ICommand GoToRegisterCommand { get; }

        public string Username
        {
            get => _username;
            set => Set(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => Set(ref _password, value);
        }


        public LoginViewModel()
        {
            LoginCommand = new Command(Login, CanLogin);
            GoToRegisterCommand = new Command(GoToRegister);
        }

        private bool CanLogin()
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
        }

        public bool IsAuthenticated
        {
            get => _isAuthenticated;
            set 
            {
                _isAuthenticated = value;
                OnPropertyChanged(nameof(IsAuthenticated));
            }
        }

        private void GoToRegister()
        {
            RequestClose?.Invoke(this);
            new View.RegisterView().Show();
        }
        private void Login()
        {
            // Exemplu simplu de autentificare
            if (Username == "admin" && Password == "password")
            {
                IsAuthenticated = true;
                RequestClose?.Invoke(this);           // Închide fereastra de login
            }
            else
            {
                MessageBox.Show("Autentificare eșuată.");
            }
        }

        public void UpdatePassword(string password)
        {
            Password = password;
            ((Command)LoginCommand).RaiseCanExecuteChanged();
        }
    }
}
