using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public class UserUtil
    {
        private static ConnexionDb connection = ConnexionDb.Instance();

        public static List<User> getUsers ()
        {
            return connection.getUsers();
        }

        //public static User CheckUserByName(string firstname, string lastname)
        //{
        //    //return connection.CheckUserByName(firstname, lastname);
        //}

        public static List<Data> getDatas(int id)
        {
            return connection.getDatas(id);
        }
    }
}
