using GalaSoft.MvvmLight.Command;
using Proiect_ABD.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Proiect_ABD.View_Model
{
    public class DashboardViewModel : ViewModelBase , INotifyPropertyChanged
    {
        private object _currentView;
        //public event Action RequestLogout;
        public bool IsAdmin { get; set; }
        public bool CanViewReports { get; set; }

        public ICommand NavigateCommand { get; }
        public ICommand LogoutCommand { get; }

        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public DashboardViewModel()
        {
            // Inițial, afișează EquipmentView
            NavigateCommand = new RelayCommand<string>(ExecuteNavigate);
            LogoutCommand = new RelayCommand(ExecuteLogout);
            var userRole = GetUserRole(); // Replace with your actual role-fetching logic
            IsAdmin = userRole == "Admin";
            CanViewReports = userRole == "Admin" || userRole == "Manager";

            CurrentView = new EquipmentViewModel();
        }

        public string GetUserRole()     //TO BE MODIFIED
        {
            return "Admin";
        }

        private void ExecuteNavigate(string viewName)
        {
            switch (viewName)
            {
                case "EquipmentView":
                    CurrentView = new EquipmentViewModel();
                    break;
                case "ManageUsersView":
                    CurrentView = new ManageUsersViewModel();
                    break;
                case "ReportsView":
                    CurrentView = new ReportsViewModel();
                    break;
                case "MaintenanceView":
                    CurrentView = new MaintenanceViewModel();
                    break;
            }
        }

        //private void OpenNewWindow(Window window)
        //{
        //    // Open the new window
        //    window.Show();

        //    // Close the current dashboard window
        //    Application.Current.Windows[0]?.Close();
        //}

        private void ExecuteLogout()
        {
            NavigationClass.NavigateTo("LoginView");
        }
    }
}
