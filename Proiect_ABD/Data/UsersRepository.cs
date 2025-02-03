using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proiect_ABD.Model;
using System.Data.Entity;
using System.Data;

namespace Proiect_ABD.Data
{
    public class UsersRepository
    {
        private readonly EquipmentMonitoringContext _context;

        public UsersRepository()
        {
            _context = new EquipmentMonitoringContext();
        }
        public List<Users> GetAllUsers()
        {
            var users = _context.Users
                        .Include(u => u.Role)
                        .Select(u => new
                        {
                            u.Id,
                            u.Name,
                            u.Email,
                            u.RoleId,
                            RoleName = u.Role.Name
                        })
                        .ToList();
            return users.Select(u => new Users
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                RoleId = u.RoleId,
                Role = new Roles { Id = u.RoleId, Name = u.RoleName }
            }).ToList();
        }
        public Users GetUserById(int id)
        {
            var user = _context.Users
                           .Include(u => u.Role)
                           .Where(u => u.Id == id)
                           .Select(u => new
                           {
                               u.Id,
                               u.Name,
                               u.Email,
                               u.RoleId,
                               RoleName = u.Role.Name
                           })
                           .FirstOrDefault();

            if (user == null) return null;

            return new Users
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                RoleId = user.RoleId,
                Role = new Roles { Id = user.RoleId, Name = user.RoleName }
            };
        }
        public Users GetUserByEmailPassword(string email, string password)
        {
            var hashedPassword = Users.HashPassword(password);

            var user = _context.Users
                   .Include(u => u.Role)
                   .FirstOrDefault(u => u.Name == email && u.Password == hashedPassword);

            if (user == null) return null;

            return new Users
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                RoleId = user.RoleId,
                Role = new Roles { Id = user.Role.Id, Name = user.Role.Name }
            };
        }
        public void AddUser(Users user)
        {
            var dbUser = new Users
            {
                Name = user.Name,
                Email = user.Email,
                Password = Users.HashPassword(user.Password),
                RoleId = user.RoleId,
            };

            _context.Users.Add(dbUser);
            _context.SaveChanges();
        }
        public void DeleteUser(int userId)
        {
            var dbUser = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (dbUser != null)
            {
                _context.Users.Remove(dbUser);
                _context.SaveChanges();
            }
        }
        public void UpdateUser(Users user)
        {
            var dbUser = _context.Users.FirstOrDefault(u => u.Id == user.Id);
            if (dbUser != null)
            {
                dbUser.Name = user.Name;
                dbUser.Email = user.Email;
                dbUser.Password = user.Password;
                dbUser.RoleId = user.RoleId;

                _context.SaveChanges();
            }
        }
    }
}
