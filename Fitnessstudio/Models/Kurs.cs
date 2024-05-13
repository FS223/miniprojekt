using Npgsql.PostgresTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitnessstudio.Models
{
    internal class Kurs
    {
        private int id;
        private string bezeichnung;
        private string beschreibung;
        private int kursLeiterId;
        private int minTeilnehmer = 0;
        private int maxTeilnehmer;
        private decimal preis;
        private int dauer;
        private List<Termin> termine;
        private List<Kunde> teilnehmer;

        public Kurs(int id, string bezeichnung, string? beschreibung, int kursLeiterId, int minTeilnehmer, int maxTeilnehmer, decimal preis, int dauer, List<Termin>? termine = null, List<Kunde>? teilnehmer = null) 
        { 
            this.id = id;
            Bezeichnung = bezeichnung;
            Beschreibung = beschreibung;
            kursLeiterId = kursLeiterId;
            MinTeilnehmer = minTeilnehmer;
            MaxTeilnehmer = maxTeilnehmer;
            Preis = preis;
            Dauer = dauer;
            Termine = termine;
            Teilnehmer = teilnehmer;
        }

        public int Id { get => id; }
        public string Bezeichnung { get => bezeichnung; set => bezeichnung = value; }
        public string Beschreibung { get => beschreibung; set => beschreibung = value; }
        public int KursLeiterId { get => kursLeiterId; set => kursLeiterId = value; }
        public int MinTeilnehmer { get => minTeilnehmer; set => minTeilnehmer = value; }
        public int MaxTeilnehmer { get => maxTeilnehmer; set => maxTeilnehmer = value; }
        public decimal Preis { get => preis; set => preis = value; }
        public int Dauer { get => dauer; set => dauer = value; }
        public List<Termin> Termine { get => termine; set => termine = value; }
        public List<Kunde> Teilnehmer { get => teilnehmer; set => teilnehmer = value; }

        public void hinzufuegenTeilnehmer(Kunde teilnehmer)
        {
            Teilnehmer.Add(teilnehmer);
        }

        public void entfernenTeilnehmer(int kundeId)
        {
            foreach(Kunde kunde in Teilnehmer)
            {
                if (kunde.Id == kundeId)
                {
                    Teilnehmer.Remove(kunde);
                }
            }
        }

        public void entfernenTeilnehmer(Kunde kunde)
        {
            Teilnehmer.Remove(kunde);
        }

        public void hinzufuegenTerin(Termin termin)
        {
            Termine.Add(termin);
        }

        public void entfernenTermin(int terminId)
        {
            foreach(Termin termin in Termine)
            {
                if (termin.Id == terminId)
                {
                    Termine.Remove(termin);
                }
            }
        }

        public void entfernenTermin(Termin termin)
        {
            Termine.Remove(termin);
        }
    }
}
