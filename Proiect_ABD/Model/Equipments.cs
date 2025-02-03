using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Key]
        [Column("_id")]
        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }
        [Required]
        [Column("_name")]
        [MaxLength(100)]
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }
        [Required]
        [Column("_status")]
        [MaxLength(20)]
        public string Status
        {
            get { return status; }
            set { status = value; OnPropertyChanged("Status"); }
        }
        [Required]
        [Column("_last_update")]
        public DateTime LastUpdate
        {
            get { return lastUpdate; }
            set { lastUpdate = value; OnPropertyChanged("LastUpdate"); }
        }
    }
}
