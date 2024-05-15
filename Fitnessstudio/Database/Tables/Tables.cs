using System.Windows.Input;
using Fitnessstudio.Models;

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
        public List<Person> Personen { get; set; }
        public Niederlassung Niederlassung { get; set; }
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
        public Anschrift Anschrift { get; set; }
        public int? AccountId { get; set; }
        public Account Account { get; set; }
        public int? KundeId { get; set; }
        public Kunde Kunde { get; set; }
        public int? MitarbeiterId { get; set; }
        public Mitarbeiter Mitarbeiter { get; set; }
        public string BgColor { get; set; }
        public string Character { get; set; }
        public string Name { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
    }

    public class Kunde
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public float Guthaben { get; set; }
        public string Iban { get; set; }
        public string Bild { get; set; }
        public Mitgliedschaft? Mitgliedschaft { get; set; }
        public List<Kurs> Kurse { get; set; }
        public List<ZeitenBuchung> ZeitenBuchung { get; set; }
        public Niederlassung Niederlassung { get; set; }
        public List<Messung> Messung { get; set; }
        public int NiederlassungID { get; set; }
    }

    public class Messung
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public float Gewicht { get; set; }
        public float Groesse { get; set; }
        public float Fettanteil { get; set; }
        public float Muskelmasse { get; set; }
        public int KundeId{ get; set; }
        public float Bmi { get; set; }
    }

    public class Mitarbeiter
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public List<Kurs> Kurse { get; set; }
        public Niederlassung Niederlassung { get; set; }
    }

    public class ZeitenBuchung
    {
        public int Id { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public Kunde Kunde { get; set; }
    }

    public class Niederlassung
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Anschrift Anschrift { get; set; }
        public List<Mitarbeiter> Mitarbeiter { get; set; }
        public List<Kunde> Kunden { get; set; }
    }

    public class Termin
    {
        public int Id { get; set; }
        public string Bezeichnung { get; set; }
        public string Beschreibung { get; set; }
        public DateTime StartZeit { get; set; }
        public int Dauer { get; set; }
        public Kurs Kurs { get; set; }
    }


    public enum Geschlecht
    {
        MAENNLICH,
        WEIBLICH,
        DIVERS
    }

    public enum Rolle
    {
        ADMINISTRATOR,
        PERSONAL,
        KUNDE
    }

    public enum Mitgliedschaft
    {
        BRONZE = 2,
        SILBER = 3 ,
        GOLD = 4,
        PLATINUM = 5
    }

}
