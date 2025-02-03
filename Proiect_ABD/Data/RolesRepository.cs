using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proiect_ABD.Model;
using System.Data.Entity;

namespace Proiect_ABD.Data
{
    public class RolesRepository
    {
        public List<Roles> GetAllRoles()
        {
            using (var context = new EquipmentMonitoringContext())
            {
                return context.Roles.ToList();
            }
        }
        public Roles GetRoleById(int id)
        {
            using (var context = new EquipmentMonitoringContext())
            {
                return context.Roles.FirstOrDefault(r => r.Id == id);
            }
        }
        //public void AddRole(Roles role)
        //{
        //    using (var context = new EquipmentMonitoringContext())
        //    {
        //        context.Roles.Add(role);
        //        context.SaveChanges();
        //    }
        //}
        //public void RemoveRole(Roles role)
        //{
        //    using (var context = new EquipmentMonitoringContext())
        //    {
        //        var existingRole = context.Roles.FirstOrDefault(r => r.Id == role.Id);
        //        if (existingRole != null)
        //        {
        //            context.Roles.Remove(existingRole);
        //            context.SaveChanges();
        //        }
        //    }
        //}
    }
}
