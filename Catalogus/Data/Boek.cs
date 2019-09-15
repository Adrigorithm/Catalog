using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogus.Data
{
    public class Boek
    {
        private int id;
        private string titel;
        private string subtitel;
        private string auteur;
        private int deel;

        public Boek(int id, string titel, string subtitel, string auteur, int deel)
        {
            this.id = id;
            this.titel = titel;
            this.subtitel = subtitel;
            this.auteur = auteur;
            this.deel = deel;
        }

        public int getId()
        {
            return id;
        }

        public string Titel { get => titel; set => titel = value; }
        public string Subtitel { get => subtitel; set => subtitel = value; }
        public string Auteur { get => auteur; set => auteur = value; }
        public int Deel { get => deel; set => deel = value; }
    }
}
