using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Proiect_ABD.Model;

namespace Proiect_ABD.Data
{
    internal class EquipmentMonitoringContext : DbContext
    {
        public EquipmentMonitoringContext() : base("name=Proiect_ABD.Properties.Settings.Proiect_ABDConnectionString2")
        {
        }
        public DbSet<Equipments> Equipments { get; set; }

        public DbSet<MaintenanceRecord> Maintenance { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        // public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // entity mapping if needeed
            base.OnModelCreating(modelBuilder);
        }
    }
}
