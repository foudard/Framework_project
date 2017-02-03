using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public class ObjectsUtil
    {
        public static List<Type> getAllClasses()
        {
            var asm = Assembly.Load("ClassLibrary3, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            var q = from t in asm.GetTypes()
                    select t;

            return q.ToList();
        }


        public static T getObjectFromJson<T>(string json) 
        { 
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
