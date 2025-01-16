using GalaSoft.MvvmLight.Command;
using Proiect_ABD.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Proiect_ABD.Utils;

namespace Proiect_ABD.View_Model
{
    public class AddUserViewModel : ViewModelBase
    {
        public Users NewUser { get; set; }
        public ObservableCollection<Roles> RolesList { get; set; }
        public RolesViewModel Roles { get; set; }

        public ICommand AddUserCommand { get; }
        public ICommand CancelCommand { get; }

        public Action CloseWindowAction { get; set; }

        public AddUserViewModel()
        {
            Roles = new RolesViewModel();
            RolesList = Roles.RolesList;
            NewUser = new Users();

            AddUserCommand = new RelayCommand(AddUser);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void AddUser()
        {
            if (string.IsNullOrEmpty(NewUser.Name) || string.IsNullOrEmpty(NewUser.Email) || string.IsNullOrEmpty(NewUser.Password) || NewUser.Role == null)
            {
                MessageBox.Show("All fields must be filled!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            (new Users()).AddUser(NewUser);

            // Pass the new user back to the main view (handled in ManageUsersViewModel)
            CloseWindowAction?.Invoke();
        }

        private void Cancel()
        {
            CloseWindowAction?.Invoke();
        }
    }
}
