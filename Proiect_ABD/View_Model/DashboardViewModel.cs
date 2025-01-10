using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Proiect_ABD.View_Model
{
    public class DashboardViewModel : ViewModelBase
    {
        private object _currentView;

        public event Action RequestLogout;

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

            CurrentView = new EquipmentViewModel();
        }

        private void ExecuteNavigate(string viewName)
        {
            switch (viewName)
            {
                case "EquipmentView":
                    CurrentView = new EquipmentViewModel();
                    break;
                case "ReportsView":
                    CurrentView = new ReportsViewModel();
                    break;
                case "MaintenanceView":
                    CurrentView = new MaintenanceViewModel();
                    break;
            }
        }

        private void OpenNewWindow(Window window)
        {
            // Open the new window
            window.Show();

            // Close the current dashboard window
            Application.Current.Windows[0]?.Close();
        }

        private void ExecuteLogout()
        {
            RequestLogout?.Invoke();
        }
    }
}
