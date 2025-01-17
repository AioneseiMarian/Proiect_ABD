using GalaSoft.MvvmLight.Command;
using Proiect_ABD.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace Proiect_ABD.View_Model
{
    public class MaintenanceViewModel
    {
        private Users user;
        public Users User
        {
            get => user;
            set
            {
                user = value;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private ObservableCollection<MaintenanceRecord> maintenanceList;
        public ObservableCollection<MaintenanceRecord> MaintenanceList
        {
            get => maintenanceList;
            set
            {
                maintenanceList = value;
                OnPropertyChanged("MaintenanceList");
            }
        }
        public ICommand SendToMainPage { get; }


        public MaintenanceViewModel() : this(null) { }
        public MaintenanceViewModel(Users user)
        {
            this.user = user;
            MaintenanceList = (new MaintenanceRecord()).GetAllMaintenanceRecords();
            SendToMainPage = new RelayCommand<MaintenanceRecord>(SendToMainPageExecute);
        }



        void RemoveMaintenanceRecord(MaintenanceRecord maintenanceRecord)
        {
            (new MaintenanceRecord()).RemoveFromMaintenanceRecord(maintenanceRecord);
            MaintenanceList.Remove(maintenanceRecord);
        }

        void SendToMainPageExecute(MaintenanceRecord maintenanceRecord)
        {
            Equipments eq = (new Equipments()).GetEquipmentById(maintenanceRecord.EquipmentId);
            eq.Status = "disponibil";

            var context = new Proiect_ABDDataContext();
            var dbEquipment = context.Equipments.FirstOrDefault(e => e._id == eq.Id);
            if (dbEquipment != null)
            {
                if (dbEquipment._name != eq.Name || dbEquipment._status != eq.Status)
                {
                    dbEquipment._name = eq.Name;
                    dbEquipment._status = eq.Status;
                    dbEquipment._last_update = DateTime.Now;
                }
                context.SubmitChanges();
            }
            RemoveMaintenanceRecord(maintenanceRecord);
        }
    }
}
