using Proiect_ABD.Data;
using Proiect_ABD.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Proiect_ABD.View
{
    /// <summary>
    /// Interaction logic for ManageUsersView.xaml
    /// </summary>
    public partial class ManageUsersView : Page
    {
        public ManageUsersView()
        {
            InitializeComponent();
            var usersRepository = new UsersRepository();
            var rolesRepository = new RolesRepository();
            var notificationService = new NotificationService();

            // Use the factory to create the ViewModel with dependencies
            var viewModelFactory = new ViewModelFactory(usersRepository, rolesRepository, notificationService);
            this.DataContext = viewModelFactory.CreateManageUsersViewModel();
        }
    }
}
