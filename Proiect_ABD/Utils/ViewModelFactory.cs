using Proiect_ABD.Data;
using Proiect_ABD.Model;
using Proiect_ABD.View_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_ABD.Utils
{
    public class ViewModelFactory
    {
        private readonly UsersRepository _usersRepository;
        private readonly RolesRepository _rolesRepository;
        private readonly NotificationService _notificationService;

        public ViewModelFactory(UsersRepository usersRepository, RolesRepository rolesRepository, NotificationService notificationService)
        {
            _usersRepository = usersRepository;
            _rolesRepository = rolesRepository;
            _notificationService = notificationService;
        }

        public ManageUsersViewModel CreateManageUsersViewModel()
        {
            return new ManageUsersViewModel(_usersRepository, _rolesRepository, _notificationService);
        }
        public DashboardViewModel CreateDashboardViewModel(Users currentUser)
        {
            return new DashboardViewModel(currentUser, _notificationService, this);
        }
    }
}
