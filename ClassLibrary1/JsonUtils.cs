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
    public class JsonUtils
    {
        private static JSchema animalSchema = JSchema.Parse(AnimalJsonSchema);
        private static JSchema recetteSchema = JSchema.Parse(RecetteJsonSchema);

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

        public static bool GetJson(string jdata, string objType)
        {
            JObject jobj = JObject.Parse(jdata);

            switch (objType)
            {
                case "Animal":
                    return jobj.IsValid(animalSchema);

                case "Recette":
                    return jobj.IsValid(recetteSchema);

                default:
                    return false;

            }
        }


        public static string AnimalJsonSchema = @"{
        'description': 'un animal',
        'type': 'object',
        'properties':
        {
            'Nom': {'type':'string', 'required': 'true'},
            'Espece': {'type': 'string', 'required': 'true'},
            'Couleur': {'type': 'string'}
        }
        }";

        public static string RecetteJsonSchema = @"{
        'description': 'Une recette',
        'type': 'object',
        'properties':
        {
            'Nom': {'type':'string', 'required': 'true'},
            'Ingredients': {
                'type': 'array',
                'items': {'type': 'string'},
                'required': 'true' 
             },
            'Temps': {'type': 'int'},
        }
        }";
    }
}
