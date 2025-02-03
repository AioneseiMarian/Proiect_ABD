using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Proiect_ABD.Data;
using Proiect_ABD.Model;

namespace Proiect_ABD.View_Model
{
    public class RolesViewModel : ViewModelBase
    {
        private readonly RolesRepository _rolesRepository;
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

        public ICommand AddRoleCommand { get; }
        public ICommand RemoveRoleCommand { get; }

        public RolesViewModel()
        {
            _rolesRepository = new RolesRepository();
            LoadRoles();

            //AddRoleCommand = new RelayCommand<Roles>(AddRole);
            //RemoveRoleCommand = new RelayCommand<Roles>(RemoveRole);
        }

        private void LoadRoles()
        {
            RolesList = new ObservableCollection<Roles>(_rolesRepository.GetAllRoles());
        }

        //private void AddRole(Roles role)
        //{
        //    if (role != null)
        //    {
        //        _rolesRepository.AddRole(role);
        //        LoadRoles();
        //    }
        //}

        //private void RemoveRole(Roles role)
        //{
        //    if (role != null)
        //    {
        //        _rolesRepository.RemoveRole(role);
        //        LoadRoles();
        //    }
        //}

    }
}
