using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitnessstudio.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly DatabaseService databaseService;
        private String welcomeText = "Welcome back ";
        public String WelcomeText { get { return welcomeText; } set { welcomeText = value; OnPropertyChanged(nameof(WelcomeText)); } }
        public Account CurrentUser { get; }
        public HomeViewModel(Account CurrentUser) 
        {
            this.databaseService = new();
            this.CurrentUser = CurrentUser;
            UpdateTable();
        }

        private async void UpdateTable()
        {
            try
            {
                if (CurrentUser.Benutzername != null)
                {
                    var Person = await databaseService.GetPersonByID(CurrentUser.Id);
                    if (Person != null)
                    {
                        WelcomeText += Person.Vorname;
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
