using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_ABD.Model
{
    public class Equipments : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private string status;
        private DateTime lastUpdate;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }

        public string Status
        {
            get { return status; }
            set { status = value; OnPropertyChanged("Status"); }
        }

        public DateTime LastUpdate
        {
            get { return lastUpdate; }
            set { lastUpdate = value; OnPropertyChanged("LastUpdate"); }
        }

        private readonly Proiect_ABDDataContext _context;
        public Equipments()
        { 
            _context = new Proiect_ABDDataContext();
        }

        public ObservableCollection<Equipments> GetAllEquipments()
        {
            ObservableCollection<Equipments> equipments = new ObservableCollection<Equipments>();

            foreach (var item in _context.Equipments)
            {
                equipments.Add(
                    new Equipments
                    {
                        Id = item._id,
                        Name = item._name,
                        Status = item._status,
                        LastUpdate = item._last_update
                    }
                    );
            }

            return equipments;
        }
    
        public void UpdateEquipmentById(Equipments equipment ,int id)
        {
            
            foreach (var item in _context.Equipments)
            {
                //if(item._id == id)
                //{
                //    item = equipment;
                //}
            }
        }
        
    }
}
