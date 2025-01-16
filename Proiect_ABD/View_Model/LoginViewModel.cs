using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Proiect_ABD.View;
using Proiect_ABD.Utils;
using Proiect_ABD.Model;

namespace Proiect_ABD.View_Model
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _password;

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

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(ExecuteLogin);
            GoToRegisterCommand = new RelayCommand(ExecuteGoToRegister);

   
        }

        private void ExecuteLogin()
        {
            Users user = new Users().GetUserByEmailPassword(Username, Password);
            if ((new Users ()).GetUserByEmailPassword(Username, Password) != null)
            {
                // Navigare la DashboardView
                //DashboardView dashboardView = new DashboardView();
                //Application.Current.MainWindow.Close();
                //Application.Current.MainWindow = dashboardView;
                //Application.Current.MainWindow.Show();

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
            RegisterView registerView = new RegisterView();
            Application.Current.MainWindow.Content = registerView;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
