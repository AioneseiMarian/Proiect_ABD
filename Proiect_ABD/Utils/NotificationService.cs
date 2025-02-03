using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_ABD.Utils
{
    public class NotificationService : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private bool _hasNewEquipment;
        public bool HasNewEquipment
        {
            get => _hasNewEquipment;
            set { _hasNewEquipment = value; OnPropertyChanged(nameof(HasNewEquipment)); }
        }

        private bool _hasNewMaintenance;
        public bool HasNewMaintenance
        {
            get => _hasNewMaintenance;
            set { _hasNewMaintenance = value; OnPropertyChanged(nameof(HasNewMaintenance)); }
        }

        private bool _hasNewUsers;
        public bool HasNewUsers
        {
            get => _hasNewUsers;
            set { _hasNewUsers = value; OnPropertyChanged(nameof(HasNewUsers)); }
        }
    }

}
