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
        private readonly ViewModelFactory _viewModelFactory;
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
        private readonly NotificationService _notificationService;
        public DashboardViewModel(NotificationService notificationService)
        {
            _notificationService = notificationService;
            NavigateCommand = new RelayCommand<string>(ExecuteNavigate);
            LogoutCommand = new RelayCommand(ExecuteLogout);

            // Default values for demonstration
            IsEquipmentModified = notificationService.HasNewEquipment;
            IsMaintenanceModified = notificationService.HasNewMaintenance;
            IsUserModified = notificationService.HasNewUsers;
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
        public bool IsEquipmentModified
        {
            get => _notificationService.HasNewEquipment;
            set
            {
                if (_notificationService.HasNewEquipment != value)
                {
                    _notificationService.HasNewEquipment = value;
                    OnPropertyChanged(nameof(IsEquipmentModified));
                }
            }
        }

        public bool IsMaintenanceModified
        {
            get => _notificationService.HasNewMaintenance;
            set
            {
                if (_notificationService.HasNewMaintenance != value)
                {
                    _notificationService.HasNewMaintenance = value;
                    OnPropertyChanged(nameof(IsMaintenanceModified));
                }
            }
        }

        public bool IsUserModified
        {
            get => _notificationService.HasNewUsers;
            set
            {
                if (_notificationService.HasNewUsers != value)
                {
                    _notificationService.HasNewUsers = value;
                    OnPropertyChanged(nameof(IsUserModified));
                }
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

        public DashboardViewModel() : this(null, null, null) { }

        public DashboardViewModel(Users user, NotificationService notificationService, ViewModelFactory viewModelFactory)
        {
            _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService)); _currentUser = user;
            _viewModelFactory = viewModelFactory ?? throw new ArgumentNullException(nameof(viewModelFactory));
            OnPropertyChanged(nameof(UserRole));

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

            CurrentView = new EquipmentViewModel(CurrentUser, _notificationService);
        }



        private void ExecuteNavigate(string viewName)
        {
            switch (viewName)
            {
                case "EquipmentView":
                    IsEquipmentModified = false;
                    CurrentView = new EquipmentViewModel(CurrentUser, _notificationService);
                    break;
                case "ManageUsersView":
                    IsUserModified = false;
                    CurrentView = _viewModelFactory.CreateManageUsersViewModel();
                    break;
                case "MaintenanceView":
                    IsMaintenanceModified = false;
                    CurrentView = new MaintenanceViewModel(CurrentUser, _notificationService);
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
