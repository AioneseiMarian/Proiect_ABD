using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Proiect_ABD;
using Proiect_ABD.Model;

namespace Proiect_ABD.View_Model
{
    public class RolesViewModel : ViewModelBase
    {
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

        public RolesViewModel()
        {
            rolesList = (new Roles()).GetAllRoles();
        }

    }
}
