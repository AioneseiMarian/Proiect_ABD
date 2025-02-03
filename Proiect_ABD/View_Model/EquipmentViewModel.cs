using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proiect_ABD.Model;
using Proiect_ABD;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Proiect_ABD.Data;
using Proiect_ABD.Utils;

namespace Proiect_ABD.View_Model
{
    public class EquipmentViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<Equipments> availableEquipments;
        public ObservableCollection<Equipments> AvailableEquipments
        {
            get { return availableEquipments; }
            set { availableEquipments = value; OnPropertyChanged(nameof(AvailableEquipments)); }
        }

        private Users currentUser;
        public Users CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; OnPropertyChanged(nameof(CurrentUser)); }
        }


        private ObservableCollection<Equipments> equipmentList;
        public ObservableCollection<Equipments> EquipmentList
        {
            get { return equipmentList; }
            set { equipmentList = value; OnPropertyChanged(nameof(EquipmentList)); }
        }

        public ICommand SendToMaintenanceCommand { get; }
        public ICommand SaveCommand { get; }

        private EquipmentRepository _equipmentRepository;
        private MaintenanceRecordRepository _maintenanceRecordRepository;
        private readonly NotificationService _notificationService;
        public EquipmentViewModel() : this(null, null) { }
        public EquipmentViewModel(Users user, NotificationService notificationService)
        {
            CurrentUser = user;

            // Initialize repositories
            _equipmentRepository = new EquipmentRepository();
            _maintenanceRecordRepository = new MaintenanceRecordRepository();  // Initialize this here
            _notificationService = notificationService;

            EquipmentList = new ObservableCollection<Equipments>(_equipmentRepository.GetAllEquipments());
            AvailableEquipments = new ObservableCollection<Equipments>(
                EquipmentList.Where(e => e.Status.Equals("disponibil", StringComparison.OrdinalIgnoreCase))
            );

            SendToMaintenanceCommand = new RelayCommand<Equipments>(SendToMaintenance);
            SaveCommand = new RelayCommand(SaveChanges);
        }

        private void SendToMaintenance(Equipments equipment)
        {
            //if (CurrentUser == null)
            //{
            //    MessageBox.Show("No user logged in", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}
            if (_maintenanceRecordRepository == null)
            {
                MessageBox.Show("Maintenance record repository not initialized", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (equipment == null) return;
            equipment.Status = "indisponibil";
            UpdateEquipmentInDatabase(equipment);
            AvailableEquipments.Remove(equipment);
            _notificationService.HasNewMaintenance = true;


            _maintenanceRecordRepository.AddMaintenanceRecord(
                    new MaintenanceRecord
                    {
                        EquipmentId = equipment.Id,
                        Equipment = equipment,
                        Date = DateTime.Now,
                        Description = "Sent to maintenance",
                        PerformedBy = CurrentUser
                    }
                );

        }

        private ObservableCollection<Equipments> GetAvailableEquipments()
        {
            return new ObservableCollection<Equipments>(
                EquipmentList.Where(e => e.Status.Equals("disponibil", StringComparison.OrdinalIgnoreCase))
            );
        }

        private ObservableCollection<Equipments> GetUnavailableEquipments()
        {
            return new ObservableCollection<Equipments>(
                EquipmentList.Where(e => e.Status.Equals("indisponibil", StringComparison.OrdinalIgnoreCase))
            );
        }

        private void SaveChanges()
        {
            foreach (var equipment in EquipmentList)
            {
                UpdateEquipmentInDatabase(equipment);
            }

        }

        private void UpdateEquipmentInDatabase(Equipments equipment)
        {
            _equipmentRepository.UpdateEquipment(equipment);
        }
    }
}

    

