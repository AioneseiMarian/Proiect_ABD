using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_ABD.Model
{
    public class Roles
    {
		private int id;
        [Key]
        [Column("_role_id")]
        public int Id
		{
			get { return id; }
			set { id = value; }
		}

		private string name;
        [Column("_role_name")]
        public string Name
		{
			get { return name; }
			set { name = value; }
		}
    }
}
