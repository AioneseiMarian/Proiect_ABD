using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace Proiect_ABD.Model
{
    public class Users : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged("Email"); }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged("Password"); }
        }

        private Roles role;

        public Roles Role
        {
            get { return role; }
            set { role = value; OnPropertyChanged("Role"); }
        }

        Proiect_ABDDataContext _context;
        public Users()
        {
            _context = new Proiect_ABDDataContext();
        }

        public ObservableCollection<Users> GetAllUsers()
        {
            ObservableCollection<Users> users = new ObservableCollection<Users>();

            foreach (var item in _context.Users)
            {
                users.Add(
                    new Users
                    {
                        Id = item._id,
                        Name = item._name,
                        Email = item._email,
                        Role = new Roles().GetRoleById(item._role_id)
                    }
                    );
            }
            return users;
        }
        
        public void AddUser(Users user)
        {
            User user_db = new User();
            user_db._name = user.Name;
            user_db._email = user.Email;
            user_db._password = HashPassword(user.Password);
            user_db._role_id = user.role.Id;

            Proiect_ABDDataContext context = new Proiect_ABDDataContext();
            context.Users.InsertOnSubmit(user_db);
            context.SubmitChanges();

        }

        public Users GetUserByEmailPassword(string username, string password)
        {
            var hashedPassword = HashPassword(password);

            var user = (from u in _context.Users
                        where u._name == username && u._password == hashedPassword
                        select u).FirstOrDefault();
            
            if (user == null)
            {
                Console.WriteLine("User not found");
                return null;
            }

            Users users = new Users();
            users.id = user._id;
            users.name = user._name;
            users.email = user._email;
            users.password = user._password;
            users.role = new Roles().GetRoleById(user._role_id);


            return users;
        }

        public Users GetUserById(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u._id == id);
            return new Users
            {
                Id = user._id,
                Name = user._name,
                Email = user._email,
                Role = new Roles().GetRoleById(user._role_id)
            };
        }


        public static string HashPassword(string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);

                StringBuilder hashString = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    hashString.Append(b.ToString("x2"));
                }

                return hashString.ToString();
            }
        }

        public void DeleteUser(Users user)
        {
            var context = new Proiect_ABDDataContext();
            var dbUser = context.Users.FirstOrDefault(u => u._id == user.Id);
            if (dbUser != null)
            {
                context.Users.DeleteOnSubmit(dbUser);
                context.SubmitChanges();
            }
        }
    }
}
