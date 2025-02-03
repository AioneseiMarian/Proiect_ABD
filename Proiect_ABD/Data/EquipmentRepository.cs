using Proiect_ABD.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_ABD.Data
{
    public class EquipmentRepository
    {
        public List<Equipments> GetAllEquipments()
        {
            using (var context = new EquipmentMonitoringContext())
            {
                return context.Equipments.ToList();
            }
        }
        public Equipments GetEquipmentById(int id)
        {
            using (var context = new EquipmentMonitoringContext())
            {
                return context.Equipments.FirstOrDefault(e => e.Id == id);
            }
        }
        public void AddEquipment(Equipments equipment)
        {
            using (var context = new EquipmentMonitoringContext())
            {
                context.Equipments.Add(equipment);
                context.SaveChanges();
            }
        }

        public void UpdateEquipment(Equipments equipment)
        {
            using (var context = new EquipmentMonitoringContext())
            {
                context.Entry(equipment).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void DeleteEquipment(int id)
        {
            using (var context = new EquipmentMonitoringContext())
            {
                var equipment = context.Equipments.Find(id);
                if (equipment != null)
                {
                    context.Equipments.Remove(equipment);
                    context.SaveChanges();
                }
            }
        }
    }
}
