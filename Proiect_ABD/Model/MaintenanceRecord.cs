using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_ABD.Model
{
    public class MaintenanceRecord
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private int _equipmentId;
        public int EquipmentId
        {
            get { return _equipmentId; }
            set { _equipmentId = value; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }


        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        private Users _performedBy;
        public Users PerformedBy
        {
            get { return _performedBy; }
            set { _performedBy = value; }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        private readonly Proiect_ABDDataContext _context;
        public MaintenanceRecord()
        {
            _context = new Proiect_ABDDataContext();
        }

        public ObservableCollection<MaintenanceRecord> GetAllMaintenanceRecords()
        {
            ObservableCollection<MaintenanceRecord> maintenanceRecords = new ObservableCollection<MaintenanceRecord>();
            foreach (var item in _context.Maintenances)
            {
                maintenanceRecords.Add(
                    new MaintenanceRecord
                    {
                        Id = item._maintenance_id,
                        EquipmentId = item._equipment_id,
                        Name = (new Equipments()).GetEquipmentById(item._equipment_id).Name,
                        Date = item._performed_at,
                        Description = item._description,
                        PerformedBy = (new Users()).GetUserById(item._performed_by)
                    }
                    );
            }
            return maintenanceRecords;
        }

        public void AddMaintenanceRecord(MaintenanceRecord maintenanceRecord)
        {
            _context.Maintenances.InsertOnSubmit(new Maintenance
            {
                _equipment_id = maintenanceRecord.EquipmentId,
                _performed_at = maintenanceRecord.Date,
                _description = maintenanceRecord.Description,
                _performed_by = maintenanceRecord.PerformedBy.Id
            });
            _context.SubmitChanges();
        }

        public void RemoveFromMaintenanceRecord(MaintenanceRecord maintenanceRecord)
        {
            var record = _context.Maintenances.FirstOrDefault(m => m._maintenance_id == maintenanceRecord.Id);
            _context.Maintenances.DeleteOnSubmit(record);
            _context.SubmitChanges();
        }
    }
}
