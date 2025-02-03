using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Proiect_ABD.Model;

namespace Proiect_ABD.Data
{
    public class MaintenanceRecordRepository
    {
        public List<MaintenanceRecord> GetAllMaintenanceRecords()
        {
            using (var context = new EquipmentMonitoringContext())
            {
                return context.Maintenance
                              .Include(m => m.PerformedBy)
                              .Include(m => m.Equipment)
                              .ToList();
            }
        }
        public MaintenanceRecord GetMaintenanceRecordById(int id)
        {
            using (var context = new EquipmentMonitoringContext())
            {
                return context.Maintenance
                              .Include(m => m.PerformedBy)
                              .Include(m => m.Equipment)
                              .FirstOrDefault(m => m.Id == id);
            }
        }
        public void AddMaintenanceRecord(MaintenanceRecord maintenanceRecord)
        {
            using (var context = new EquipmentMonitoringContext())
            {
                if (maintenanceRecord.PerformedBy != null)
                {
                    // Ensure the user is attached to the context
                    maintenanceRecord.PerformedBy = context.Users.FirstOrDefault(u => u.Id == maintenanceRecord.PerformedBy.Id);
                }
                context.Maintenance.Add(maintenanceRecord);
                context.SaveChanges();
            }
        }
        public void RemoveMaintenanceRecord(MaintenanceRecord maintenanceRecord)
        {
            using (var context = new EquipmentMonitoringContext())
            {
                // locate the record in the context based on its pm
                var record = context.Maintenance.FirstOrDefault(m => m.Id == maintenanceRecord.Id);
                if (record != null)
                {
                    context.Maintenance.Remove(record);
                    context.SaveChanges();
                }
            }
        }
    }
}
