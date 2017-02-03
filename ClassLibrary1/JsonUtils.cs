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
            JSchema animalSchema = JSchema.Parse(AnimalJsonSchema);
            JSchema recetteSchema = JSchema.Parse(RecetteJsonSchema);

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

        public static string RecetteJsonSchema = @"{
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
