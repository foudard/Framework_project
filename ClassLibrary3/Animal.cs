using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objets
{
    class Animal
    {
        private string nom;

        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        private string espece;

        public string Espece
        {
            get { return espece; }
            set { espece = value; }
        }

        private int couleur;

        public int Couleur
        {
            get { return couleur; }
            set { couleur = value; }
        }
    }
}
