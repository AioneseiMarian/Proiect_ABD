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

namespace Proiect_ABD.View_Model
{
    public class EquipmentViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private ObservableCollection<Equipments> availableEquipments;
        public ObservableCollection<Equipments> AvailableEquipments
        {
            get { return availableEquipments; }
            set { availableEquipments = value; OnPropertyChanged("AvailableEquipments"); }
        }

        private Users currentUser;
        public Users CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; OnPropertyChanged("CurrentUser"); }
        }


        private ObservableCollection<Equipments> equipmentList;
        public ObservableCollection<Equipments> EquipmentList
        {
            get { return equipmentList; }
            set { equipmentList = value; OnPropertyChanged("EquipmentList"); }
        }

        public ICommand SendToMaintenanceCommand { get; }
        public ICommand SaveCommand { get; }

        public EquipmentViewModel() : this(null) { }
        public EquipmentViewModel(Users user)
        {
            CurrentUser = user;
            equipmentList = (new Equipments()).GetAllEquipments();
            availableEquipments = GetAvailableEquipments();

            SendToMaintenanceCommand = new RelayCommand<Equipments>(Equipments => SendToMaintenance(Equipments));
            SaveCommand = new RelayCommand(SaveChanges);
        }

        private void SendToMaintenance(Equipments equipment)
        {
            equipment.Status = "indisponibil";
            UpdateEquipmentInDatabase(equipment);
            availableEquipments.Remove(equipment);

            (new MaintenanceRecord()).AddMaintenanceRecord(
                    new MaintenanceRecord
                    {
                        EquipmentId = equipment.Id,
                        Date = DateTime.Now,
                        Description = "Sent to maintenance",
                        PerformedBy = CurrentUser
                    }
                );

        }

        private ObservableCollection<Equipments> GetAvailableEquipments()
        {
            return new ObservableCollection<Equipments>(EquipmentList.Where(e => e.Status == "disponibil"));
        }

        private ObservableCollection<Equipments> GetUnavailableEquipments()
        {
            return new ObservableCollection<Equipments>(EquipmentList.Where(e => e.Status == "indisponibil"));
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
            // Logic to update equipment in the database
            // For example, using Entity Framework or ADO.NET
            var context = new Proiect_ABDDataContext();
            var dbEquipment = context.Equipments.FirstOrDefault(e => e._id == equipment.Id);
            if (dbEquipment != null)
            {
                if (dbEquipment._name != equipment.Name || dbEquipment._status != equipment.Status)
                {
                    dbEquipment._name = equipment.Name;
                    dbEquipment._status = equipment.Status;
                    dbEquipment._last_update = DateTime.Now;
                }
                context.SubmitChanges();
            }
        }
    }
}

    

