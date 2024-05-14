namespace Fitnessstudio
{
    public class Kurs
    {
        private int id;
        private string bezeichnung;
        private string beschreibung;
        private int kursLeiterId;
        private int minTeilnehmer = 0;
        private int maxTeilnehmer;
        private decimal preis;
        private List<Termin> termine;
        private List<Kunde> teilnehmer;

        public Kurs(int id, string bezeichnung, string? beschreibung, int kursLeiterId, int minTeilnehmer, int maxTeilnehmer, decimal preis, List<Termin>? termine = null, List<Kunde>? teilnehmer = null)
        {
            this.id = id;
            Bezeichnung = bezeichnung;
            Beschreibung = beschreibung;
            KursLeiterId = kursLeiterId;
            MinTeilnehmer = minTeilnehmer;
            MaxTeilnehmer = maxTeilnehmer;
            Preis = preis;
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
        public List<Termin> Termine { get => termine; set => termine = value; }
        public List<Kunde> Teilnehmer { get => teilnehmer; set => teilnehmer = value; }

        public void HinzufuegenTeilnehmer(Kunde teilnehmer)
        {
            Teilnehmer.Add(teilnehmer);
        }

        public void EntfernenTeilnehmer(int kundeId)
        {
            foreach (Kunde kunde in Teilnehmer)
            {
                if (kunde.Id == kundeId)
                {
                    Teilnehmer.Remove(kunde);
                    break;
                }
            }
        }

        public void EntfernenTeilnehmer(Kunde kunde)
        {
            Teilnehmer.Remove(kunde);
        }

        public void HinzufuegenTermin(Termin termin)
        {
            Termine.Add(termin);
        }

        public void EntfernenTermin(int terminId)
        {
            foreach (Termin termin in Termine)
            {
                if (termin.Id == terminId)
                {
                    Termine.Remove(termin);
                    break;
                }
            }
        }

        public void EntfernenTermin(Termin termin)
        {
            Termine.Remove(termin);
        }
    }
}
