using Fitnessstudio.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitnessstudio.Commands
{
    public class SaveCommand : CommandBase
    {
        private KundenVerwaltungViewModel viewModel;
        public SaveCommand(KundenVerwaltungViewModel viewModel) 
        { 
            this.viewModel = viewModel;
        }
        public override void Execute(object? parameter)
        {
            Debug.WriteLine(message: "Execute Save");
            Update();


        }

        private async void Update()
        {
            var databaseService = new DatabaseService();
            Person currentPerson = await databaseService.GetPersonByID(viewModel.CurrentUser.Id);
            if (viewModel.CurrentUser.Rolle == Rolle.ADMINISTRATOR)
            {
                await databaseService.UpdatePersonByID(viewModel.CurrentUser.Id, new Person {
                    Id = currentPerson.Id,
                    Vorname = viewModel.Vorname,
                    Nachname = viewModel.Nachname,
                    Geburtsdatum = viewModel.Geburtstag,
                    Geschlecht = currentPerson.Geschlecht, // Angenommen Geschlecht ist ein Enum
                    AnschriftId = currentPerson.AnschriftId,    
                });

                // TODO Anschrift IBAN PASSWORT überarbeiten
            }
            else
            {

            }
        }
    }
}
