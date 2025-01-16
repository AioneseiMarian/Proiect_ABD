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

        private ObservableCollection<Equipments> equipmentList;
        public ObservableCollection<Equipments> EquipmentList
        {
            get { return equipmentList; }
            set { equipmentList = value; OnPropertyChanged("EquipmentList"); }
        }

        public ICommand SaveCommand { get; }

        public EquipmentViewModel()
        {
            equipmentList = (new Equipments()).GetAllEquipments();

            SaveCommand = new RelayCommand(SaveChanges);
        }

        private void SaveChanges()
        {
            // Here, you would save the changes to the database
            foreach (var equipment in EquipmentList)
            {
                // Assuming you have a method to update the equipment in the database
                UpdateEquipmentInDatabase(equipment);
            }

            // Optionally, show a message or update UI to indicate success
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

    

