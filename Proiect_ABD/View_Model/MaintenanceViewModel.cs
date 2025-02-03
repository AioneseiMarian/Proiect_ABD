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
using Proiect_ABD.Data;
using Proiect_ABD.Utils;


namespace Proiect_ABD.View_Model
{
    public class MaintenanceViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Users user;
        public Users User
        {
            get => user;
            set
            {
                user = value;
            }
        }

        private ObservableCollection<MaintenanceRecord> maintenanceList;
        public ObservableCollection<MaintenanceRecord> MaintenanceList
        {
            get => maintenanceList;
            set
            {
                maintenanceList = value;
                OnPropertyChanged(nameof(MaintenanceList));
            }
        }
        public ICommand SendToMainPage { get; }

        private readonly MaintenanceRecordRepository _maintenanceRepo;
        private readonly EquipmentRepository _equipmentRepo;
        private readonly NotificationService _notificationService;
        public MaintenanceViewModel() : this(null, null) { }
        public MaintenanceViewModel(Users user, NotificationService notificationService)
        {
            User = user;

            _maintenanceRepo = new MaintenanceRecordRepository();
            _equipmentRepo = new EquipmentRepository();
            _notificationService = notificationService;

            MaintenanceList = new ObservableCollection<MaintenanceRecord>(_maintenanceRepo.GetAllMaintenanceRecords());
            SendToMainPage = new RelayCommand<MaintenanceRecord>(SendToMainPageExecute);
        }



        void RemoveMaintenanceRecord(MaintenanceRecord maintenanceRecord)
        {
            _maintenanceRepo.RemoveMaintenanceRecord(maintenanceRecord);
            MaintenanceList.Remove(maintenanceRecord);
        }

        void SendToMainPageExecute(MaintenanceRecord maintenanceRecord)
        {
            if (maintenanceRecord == null)
                return;
            Equipments eq = _equipmentRepo.GetEquipmentById(maintenanceRecord.EquipmentId);
            if (eq != null)
            {
                eq.Status = "disponibil";

                _equipmentRepo.UpdateEquipment(eq);
            }
            _notificationService.HasNewEquipment = true;
            RemoveMaintenanceRecord(maintenanceRecord);
        }
    }
}
