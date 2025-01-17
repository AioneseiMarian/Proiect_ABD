using GalaSoft.MvvmLight.Command;
using Proiect_ABD.Model;
using Proiect_ABD.View;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Proiect_ABD.View_Model
{
    public class ManageUsersViewModel : ViewModelBase
    {
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
        public ManageUsersViewModel()
        {
            UsersList = (new Users()).GetAllUsers();
            RolesList = new Roles().GetAllRoles();
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
            var context = new Proiect_ABDDataContext();
            var dbUser = context.Users.FirstOrDefault(u => u._id == user.Id);
            if (dbUser != null)
            {
                dbUser._name = user.Name;
                dbUser._role_id = user.Role.Id;
                context.SubmitChanges();
            }
        }

        public ICommand ChangePasswordCommand { get; }
        public ICommand DeleteUserCommand { get; }
        public ICommand AddUserCommand { get; }


        private void ChangePassword(Users user)
        {
            if (user != null)
            {
                var changePasswordWindow = new ChangePasswordView
                {
                    DataContext = new ChangePasswordViewModel(user)
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
                (new Users()).DeleteUser(user);
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
                }
            }
        }




    }
}
