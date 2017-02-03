using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Data
    {
        private string dataJson;

        public string DataJson
        {
            get { return dataJson; }
            set { dataJson = value; }
        }

        private int userId;

        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }


    }
}
