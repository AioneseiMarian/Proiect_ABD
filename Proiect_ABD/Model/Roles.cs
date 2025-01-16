using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_ABD.Model
{
    public class Roles
    {
		private int id;
		public int Id
		{
			get { return id; }
			set { id = value; }
		}

		private string name;
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

        Proiect_ABDDataContext _context;
     
		public Roles()
		{
            _context = new Proiect_ABDDataContext();
		}

        public ObservableCollection<Roles> GetAllRoles()
        {
            ObservableCollection<Roles> roles = new ObservableCollection<Roles>();

            foreach (var item in _context.Roles)
            {
                roles.Add(
                    new Roles
                    {
                        Id = item._role_id,
                        Name = item._role_name
                    }
                    );
            }
            return roles;
        }

        public Roles GetRoleById(int id)
        {
            var role = _context.Roles.FirstOrDefault(r => r._role_id == id);
            return new Roles
            {
                Id = role._role_id,
                Name = role._role_name
            };
        }

    }
}
