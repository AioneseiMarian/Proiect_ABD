using GalaSoft.MvvmLight.Command;
using Proiect_ABD.Data;
using Proiect_ABD.Model;
using Proiect_ABD.Utils;
using Proiect_ABD.View;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Proiect_ABD.View_Model
{
    public class ManageUsersViewModel : ViewModelBase
    {
        private readonly UsersRepository _usersRepository;
        private readonly RolesRepository _rolesRepository;
        private readonly NotificationService _notificationService;

        private ObservableCollection<Users> _usersList;
        public ObservableCollection<Users> UsersList
        {
            get => _usersList;
            set
            {
                _usersList = value;
                OnPropertyChanged(nameof(UsersList));
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand ChangePasswordCommand { get; }
        public ICommand DeleteUserCommand { get; }
        public ICommand AddUserCommand { get; }

        private ObservableCollection<Roles> rolesList;
        public ObservableCollection<Roles> RolesList
        {
            get => rolesList;
            set
            {
                rolesList = value;
                OnPropertyChanged(nameof(RolesList));
            }
        }
        public ManageUsersViewModel(UsersRepository usersRepository, RolesRepository rolesRepository, NotificationService notificationService)
        {
            _notificationService = notificationService;
            _usersRepository = usersRepository;
            _rolesRepository = rolesRepository;

            UsersList = new ObservableCollection<Users>(_usersRepository.GetAllUsers());
            RolesList = new ObservableCollection<Roles>(_rolesRepository.GetAllRoles());
            SaveCommand = new RelayCommand(SaveChanges);
            ChangePasswordCommand = new RelayCommand<Users>(ChangePassword);
            DeleteUserCommand = new RelayCommand<Users>(DeleteUser);
            AddUserCommand = new RelayCommand(OpenAddUserWindow);
        }

        private void SaveChanges()
        {
            foreach (var user in UsersList)
            {
                UpdateUserInDatabase(user);
            }

           MessageBox.Show("Changes saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void UpdateUserInDatabase(Users user)
        {
            _usersRepository.UpdateUser(user);
        }
        private void ChangePassword(Users user)
        {
            if (user != null)
            {
                var changePasswordWindow = new ChangePasswordView
                {
                    DataContext = new ChangePasswordViewModel(user, _usersRepository)
                };

                changePasswordWindow.ShowDialog();
            }
        }

        private void DeleteUser(Users user)
        {
            if (user != null)
            {
                UsersList.Remove(user);
                MessageBox.Show($"User {user.Name} deleted.");
                _usersRepository.DeleteUser(user.Id); // Using repository method to delete
            }
        }
        private void OpenAddUserWindow()
        {
            var addUserWindow = new AddUserView
            {
                DataContext = new AddUserViewModel()
            };
            if (addUserWindow.ShowDialog() == true)
            {
                var viewModel = (AddUserViewModel)addUserWindow.DataContext;
                if (viewModel.NewUser != null)
                {
                    UsersList.Add(viewModel.NewUser);
                    _notificationService.HasNewUsers = true;
                }
            }
        }
    }
}
