using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Proiect_ABD.View;
using Proiect_ABD.Utils;
using Proiect_ABD.Model;
using Proiect_ABD.Data;

namespace Proiect_ABD.View_Model
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _password;
        private UsersRepository _usersRepository;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand GoToRegisterCommand { get; }

        public LoginViewModel(UsersRepository usersRepository)
        {
            _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            LoginCommand = new RelayCommand(ExecuteLogin);
            GoToRegisterCommand = new RelayCommand(ExecuteGoToRegister);
        }
        private void ExecuteLogin()
        {
            Users user = _usersRepository.GetUserByEmailPassword(Username, Password);
            if (user != null)
            {

                // Navigare la DashboardView
                //DashboardView dashboardView = new DashboardView();
                //Application.Current.MainWindow.Close();
                //Application.Current.MainWindow = dashboardView;
                //Application.Current.MainWindow.Show();

                NavigationClass.CurrentUser = user;

                NavigationClass.NavigateTo("DashboardView");
            }
            else
            {
                MessageBox.Show("Username sau parola incorecte!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExecuteGoToRegister()
        {
            // Navigare la RegisterView
            NavigationClass.NavigateTo("RegisterView");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
