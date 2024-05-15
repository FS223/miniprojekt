using Fitnessstudio.Commands;
using Fitnessstudio.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Fitnessstudio.ViewModels
{
    public class KundenVerwaltungViewModel : BaseViewModel
    {
        private readonly DatabaseService databaseService;

        public Account CurrentUser;
        private String vorname = "";
        public String Vorname { get { return vorname; } set { vorname = value; OnPropertyChanged(nameof(Vorname)); } }

        private String nachname = "";
        public String Nachname { get { return nachname; } set { nachname = value; OnPropertyChanged(nameof(Nachname)); } }

        private String land = "";
        public String Land { get { return land; } set { land = value; OnPropertyChanged(nameof(Land)); } }

        private String adresse = "";
        public String Adresse { get { return adresse; } set { adresse = value; OnPropertyChanged(nameof(Adresse)); } }

        private String iban = "";
        public String Iban { get { return iban; } set { iban = value; OnPropertyChanged(nameof(Iban)); } }

        private String passwort = "";
        public String Passwort { get { return passwort; } set { passwort = value; OnPropertyChanged(nameof(Passwort)); } }

        private String wpasswort = "";
        public String WPasswort { get { return wpasswort; } set { wpasswort = value; OnPropertyChanged(nameof(WPasswort)); } }

        private DateTime geburtstag = DateTime.MinValue;
        public DateTime Geburtstag { get { return geburtstag; } set { geburtstag = value; OnPropertyChanged(nameof (Geburtstag)); } }
        public ICommand SaveCommand { get; }

        public KundenVerwaltungViewModel(Account currentUser)
        {
            this.CurrentUser = currentUser;
            databaseService = new DatabaseService();
            SaveCommand = new SaveCommand(this);
            UpdateTable();
        }

        private async void UpdateTable()
        {
            try
            {
                if (CurrentUser.Benutzername != null)
                {
                    var Person = await databaseService.GetPersonByID(CurrentUser.PersonId);
                    if (Person != null)
                    {
                        Vorname = Person.Vorname;
                        Nachname = Person.Nachname;
                        Geburtstag = Person.Geburtsdatum;
                    }
                    var address = await databaseService.GetAnschriftByID(Person.AnschriftId);
                    if (address != null)
                    {                        
                        Land = address.Land;
                        Adresse = address.Strasse + " " + address.Hausnummer;
                        
                    }
                    var Kunde = await databaseService.GetKundeByID(1); // TODO
                    if (Kunde != null)
                    {
                        Iban = Kunde.Iban;
                        Passwort = "";
                        WPasswort = "";
                    }
                    
                }               
                
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while fetching persons");
            }           

        }
    }
}
