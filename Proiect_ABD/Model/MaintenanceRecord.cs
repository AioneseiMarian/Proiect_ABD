using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proiect_ABD.Model
{
    [Table("Maintenance")]
    public class MaintenanceRecord
    {
        private int _id;
        private int _equipmentId;
        private string _name;
        private DateTime _date;
        private string _description;
        private int _performedById;
        [Key]
        [Column("_maintenance_id")]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        [Required]
        [Column("_equipment_id")]
        public int EquipmentId { get; set; }

        [ForeignKey(nameof(EquipmentId))]
        public virtual Equipments Equipment { get; set; }
        //[Column("_description")]
        //public string Name
        //{
        //    get { return _name; }
        //    set { _name = value; }
        //}

        [Required]
        [Column("_performed_at")]
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        [Column("_description")]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        [Required]
        [Column("_performed_by")]
        public int PerformedById { get; set; }

        [ForeignKey(nameof(PerformedById))]
        public virtual Users PerformedBy { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
