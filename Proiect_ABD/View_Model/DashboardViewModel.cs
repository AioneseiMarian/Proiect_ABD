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

using Proiect_ABD.Model;

namespace Proiect_ABD.View_Model
{
    public class DashboardViewModel : ViewModelBase , INotifyPropertyChanged
    {
        public Roles UserRole { get; set; }
        private bool canViewMaintenance;

        public bool CanViewMaintenance
        {
            get { return canViewMaintenance; }
            set 
            {
                if (canViewMaintenance != value)
                {
                    canViewMaintenance = value;
                    OnPropertyChanged(nameof(CanViewMaintenance));
                }
            }
        }



        private bool _isAdmin;
        public bool IsAdmin
        {
            get => _isAdmin;
            set
            {
                if (_isAdmin != value)
                {
                    _isAdmin = value;
                    OnPropertyChanged(nameof(IsAdmin));
                }
            }
        }

        private object _currentView;
        private Users _currentUser;

        public Users CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
                OnPropertyChanged(nameof(UserRole));
            }
        }

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

        public DashboardViewModel() : this(null) { }

        public DashboardViewModel(Users user)
        {
            // Inițial, afișează EquipmentView
            _currentUser = user;
            
            switch (user.Role.Name)
            {
                case "Administrator":
                    IsAdmin = true;
                    CanViewMaintenance = true;
                    break;
                case "Manager":
                    IsAdmin = false;
                    CanViewMaintenance = true;
                    break;
                case "Mentenanta":
                    IsAdmin = false;
                    CanViewMaintenance = true;
                    break;
                case "Tehnician":
                    IsAdmin = false;
                    CanViewMaintenance = false;
                    break;

            }
            NavigateCommand = new RelayCommand<string>(ExecuteNavigate);
            LogoutCommand = new RelayCommand(ExecuteLogout);

            CurrentView = new EquipmentViewModel(CurrentUser);
        }



        private void ExecuteNavigate(string viewName)
        {
            switch (viewName)
            {
                case "EquipmentView":
                    CurrentView = new EquipmentViewModel(CurrentUser);
                    break;
                case "ManageUsersView":
                    CurrentView = new ManageUsersViewModel();
                    break;
                case "MaintenanceView":
                    CurrentView = new MaintenanceViewModel(CurrentUser);
                    break;
                default:
                    MessageBox.Show("Invalid view name");
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
