using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class User
    {

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value;}
        }

        private string firstName;

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        private string lastName;

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        private string login;

        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private List<Role> roles;

        public List<Role> Roles
        {
            get { return roles; }
            set { roles = value; }
        }

        private List<Data> data;

        public List<Data> Data
        {
            get { return data; }
            set { data = value; }
        }

    }
}
