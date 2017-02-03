using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objets
{
    public class Recette
    {
        private string nom;

        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        private List<string> ingredients;

        public List<string> Ingredients
        {
            get { return ingredients; }
            set { ingredients = value; }
        }

        private int temps;

        public int Temps
        {
            get { return temps; }
            set { temps = value; }
        }
    }
}
