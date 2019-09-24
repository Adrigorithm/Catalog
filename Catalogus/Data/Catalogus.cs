using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Catalogus.Data
{
    public static class Catalogus
    {
        [JsonProperty]
        private static List<Boek> boeken = new List<Boek>();
        private static List<Boek> boekenSelectie = new List<Boek>();
        private static bool bufferOnopgeslagen = false;

        public static List<Boek> Boeken { get => boeken; set => boeken = value; }

        public static List<Boek> getBoekenSelectie()
        {
            return boekenSelectie;
        }

        public static void addBoekenSelectie(Boek boek)
        {
            boekenSelectie.Add(boek);
        }

        public static void resetBoekenSelectie()
        {
            boekenSelectie.Clear();
        }
        public static List<Boek> getBoeken()
        {
            return boeken;
        }

        public static Data.Boek getBoek(int index)
        {
            return getBoeken()[index];
        }

        public static void setBoek(Data.Boek boek)
        {
            for(int i = 0; i < boeken.Count; i++)
            {
                if (boeken[i].Id.Equals(boek.Id))
                {
                    boeken[i] = boek;
                }
            }
        }

        public static List<Boek> getBoekenContains(string property, string value)
        {
            value = value.ToLower();
            List<Boek> boekSelectie = new List<Boek>();
            Type typeBoek = typeof(Data.Boek);

            foreach (Boek boek in boeken)
            {
                if (typeBoek.GetProperty(property).GetValue(boek).ToString().ToLower().Contains(value))
                {
                    boekSelectie.Add(boek);
                }
            }

            return boekSelectie;
        }

        public static List<Boek> getBoeken(string property, string value)
        {
            value = value.ToLower();
            List<Boek> boekSelectie = new List<Boek>();
            Type typeBoek = typeof(Data.Boek);

            foreach (Boek boek in boeken)
            {
                if (typeBoek.GetProperty(property).GetValue(boek).ToString().ToLower().Equals(value))
                {
                    boekSelectie.Add(boek);
                }
            }
            
            return boekSelectie;
        }

        public static bool getBufferOnopgeslagen()
        {
            return bufferOnopgeslagen;
        }

        public static void setBufferOnopgeslagen(bool buffer)
        {
            bufferOnopgeslagen = buffer;
        }

        public static void addBoek(Boek boek)
        {
            boeken.Add(boek);
        }

        public static void loadBoeken()
        {
            if (File.Exists("catalogus.json"))
            {
                boeken = JsonConvert.DeserializeObject<List<Boek>>(File.ReadAllText("catalogus.json"));
            }
        }

        public static void saveBoeken()
        {
            File.WriteAllText("catalogus.json", JsonConvert.SerializeObject(boeken));
        }

        public static List<Data.Boek> getDuplicates(Data.Boek boek)
        {
            List<Data.Boek> duplicates_local = new List<Data.Boek>();
            foreach(Data.Boek boek_local in getBoeken())
            {
                if (boek.Titel.ToLower().Equals(boek_local.Titel.ToLower()) && boek.Auteur.ToLower().Equals(boek_local.Auteur.ToLower()) && boek.Deel == boek_local.Deel)
                {
                    duplicates_local.Add(boek_local);
                }
            }
            return duplicates_local;
        }
    }
}
