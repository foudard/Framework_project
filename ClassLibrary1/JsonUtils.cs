using Classes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public static class JsonUtils
    {

        private static JSchema animalSchema;

        public static JSchema AnimalSchema
        {
            get { return animalSchema; }
            set { animalSchema = value; }
        }

        private static JSchema recetteSchema;

        public static JSchema RecetteSchema
        {
            get { return recetteSchema; }
            set { recetteSchema = value; }
        }


        //private static JSchema animalSchema = JSchema.Parse(AnimalJsonSchema);
        //private static JSchema recetteSchema = JSchema.Parse(RecetteJsonSchema);

        public static bool IsValidJson(string strInput)
        {
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    Console.WriteLine(jex.Message);
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static string getJsonObjectType(string json)
        {
            List<Type> classes = ObjectsUtil.getAllClasses();
            string className = "";
            foreach(Type typeClass in classes)
            {
                if (GetJson(json, typeClass.Name))
                {
                    className = typeClass.Name;
                    break;        
                }              
            }

            return className;
        }

        public static bool GetJson(string jdata, string objType)
        {

            JObject jobj = JObject.Parse(jdata);

            AnimalSchema = JSchema.Parse(AnimalJsonSchema);
            RecetteSchema = JSchema.Parse(RecetteJsonSchema);

            switch (objType)
            {
                case "Animal":
                    return jobj.IsValid(AnimalSchema);

                case "Recette":
                    return jobj.IsValid(RecetteSchema);

                default:
                    return false;

            }
        }
       

        private static string AnimalJsonSchema = @"{
        'description': 'un animal',
        'type': 'object',
        'properties': {
            'Nom': {'type':'string'},
            'Espece': {'type': 'string'},
            'Couleur': {'type': 'string'}
        },
        'required': [
            'Nom',
            'Espece'
        ]
        }";

        private static string RecetteJsonSchema = @"{
        'description': 'Une recette',
        'type': 'object',
        'properties': {
            'Nom': {'type':'string'},
            'Ingredients': {
                'type': 'array',
                'items': {'type': 'string'}
             },
            'Temps': {'type': 'integer'},
        },
        'required': [
            'Nom',
            'Ingredients'
        ]
        }";
    }
}
