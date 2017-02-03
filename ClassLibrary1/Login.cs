using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace Util
{
    public static class Login
    {
        private static ConnexionDb connection = ConnexionDb.Instance();

        public static User Log(string login, string pwd)
        {
            return connection.checkConnection(login, pwd);
        }

        public static bool checkRole(User user, string role)
        {
            bool result = false;
            if(user.Id != 0)
            {
               List<Role> roles = user.Roles;
               result = roles.Any(p => p.Name == role);
            }

            return result;
        }
    }
}
