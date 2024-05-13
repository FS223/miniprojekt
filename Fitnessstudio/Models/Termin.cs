using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitnessstudio.Models
{
    internal class Termin
    {
        private int id;
        private string bezeichnung;
        private string beschreibung;
        private DateTime startZeit;
        private int dauer;
        private Kurs kurs;

        public int Id { get => id; }
        public string Bezeichnung { get => bezeichnung; set => bezeichnung = value; }
        public string Beschreibung { get => beschreibung; set => beschreibung = value; }
        public DateTime StartZeit { get => startZeit; set => startZeit = value; }
        public int Dauer { get => dauer; set => dauer = value; }
        public Kurs Kurs { get => kurs; set => kurs = value; }

        public Termin (int id, string bezeichnung, string? beschreibung, DateTime startZeit, int dauer, Kurs? kurs)
        {
            this.id = id;
            Bezeichnung = bezeichnung;
            Beschreibung = beschreibung;
            StartZeit = startZeit;
            Dauer = dauer;
            Kurs = kurs;
            Bezeichnung = bezeichnung;
            Beschreibung = beschreibung;
            StartZeit = startZeit;
            Dauer = dauer;
            Kurs = kurs;
        }

        
    }
}
