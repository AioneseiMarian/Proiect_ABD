using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_ABD.Utils
{
    public static class UserSession
    {
        public static int UserId { get; set; }
        public static string UserName { get; set; }

        public static string Password { get; set; } = string.Empty;
        public static string RoleName { get; set; }
        public static int RoleId { get; set; }

        public static void Clear()
        {
            UserId = 0;
            UserName = string.Empty;
            RoleName = string.Empty;
            RoleId = 0;
        }
    }
}
