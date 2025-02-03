using GalaSoft.MvvmLight.Command;
using Proiect_ABD.Model;
using Proiect_ABD.View;
using Proiect_ABD.Data;
using System.Linq;
using System.Windows;
using System.Windows.Input;


namespace Proiect_ABD.View_Model
{
    public class ChangePasswordViewModel : ViewModelBase
    {
        private readonly Users _user;
        private readonly UsersRepository _usersRepository;
        private string _oldPassword;
        private string _newPassword;
        private string _confirmPassword;
        public ICommand ChangePasswordCommand { get; }
        public ICommand CancelCommand { get; }

        public string OldPassword
        {
            get => _oldPassword;
            set
            {
                _oldPassword = value;
                OnPropertyChanged(nameof(OldPassword));
            }
        }
        public string NewPassword
        {
            get => _newPassword;
            set
            {
                _newPassword = value;
                OnPropertyChanged(nameof(NewPassword));
            }
        }
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }
        public ChangePasswordViewModel(Users user, UsersRepository usersRepository)
        {
            _user = user;
            _usersRepository = usersRepository;
            ChangePasswordCommand = new RelayCommand(ExecuteChangePassword, CanExecuteChangePassword);
            CancelCommand = new RelayCommand(ExecuteCancel);
        }
        private bool CanExecuteChangePassword()
        {
            return !string.IsNullOrEmpty(_oldPassword) &&
                   !string.IsNullOrEmpty(_newPassword) &&
                   !string.IsNullOrEmpty(_confirmPassword) &&
                   _newPassword == _confirmPassword;
        }
        
        private void ExecuteChangePassword()
        {
            var dbUser = _usersRepository.GetUserById(_user.Id);
            if (dbUser != null)
            {
                string hashedOldPassword = Users.HashPassword(_oldPassword);

                if (dbUser.Password == hashedOldPassword)
                {
                    dbUser.Password = Users.HashPassword(_newPassword);
                    _usersRepository.UpdateUser(dbUser);

                    MessageBox.Show("Password changed successfully!", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    CloseWindow();
                }
                else
                {
                    MessageBox.Show("Old password is incorrect!", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ExecuteCancel()
        {
            CloseWindow();
        }

        private void CloseWindow()
        {
            if (Application.Current.Windows.OfType<ChangePasswordView>().FirstOrDefault() is Window window)
            {
                window.DialogResult = false;
                window.Close();
            }
        }
    }
}




