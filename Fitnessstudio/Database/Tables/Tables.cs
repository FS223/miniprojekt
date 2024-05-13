using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitnessstudio
{
    public class Anschrift
    {
        public int Id { get; set; }
        public string Land { get; set; }
        public string Plz { get; set; }
        public string Ort { get; set; }
        public string Strasse { get; set; }
        public string Hausnummer { get; set; }
        public string Zusatz { get; set; }
    }

    public class Account
    {
        public int Id { get; set; }
        public string Benutzername { get; set; }
        public string Email { get; set; }
        public string Passwort { get; set; }
        public Rolle Rolle { get; set; }
        public int PersonId { get; set; }
    }

    public class Person
    {
        public int Id { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public DateTime Geburtsdatum { get; set; }
        public Geschlecht Geschlecht { get; set; }
        public int AnschriftId { get; set; }
        public int? AccountId { get; set; }
        public int? KundeId { get; set; }
        public int? MitarbeiterId { get; set; }
    }

    public class Kunde
    {
        public int Id { get; set; }
        public int? PersonId { get; set; }
        public float Guthaben { get; set; }
        public string Iban { get; set; }
        public string Bild { get; set; }
        public Mitgliedschaft Mitgliedschaft { get; set; }
    }

    public class Messung
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public float Gewicht { get; set; }
        public float Groesse { get; set; }
        public float Fettanteil { get; set; }
        public float Muskelmasse { get; set; }
        public int KundeId { get; set; }
        public float Bmi { get; set; }
    }

    public class Mitarbeiter
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
    }

    public enum Geschlecht
    {
        Maennlich,
        Weiblich,
        Divers
    }

    public enum Rolle
    {
        ADMINISTRATOR,
        PERSONAL,
        KUNDE
    }

    public enum Mitgliedschaft
    {
        Keine,
        Bronze,
        Silber,
        Gold,
        Platinum
    }


}
