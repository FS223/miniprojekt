using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Fitnessstudio.Commands;
using Fitnessstudio.Models;
using Fitnessstudio.Views;
using Npgsql;
using Serilog;

namespace Fitnessstudio.ViewModels
{
    public class KundenAdminViewModel : BaseViewModel
    {
        private readonly DatabaseService databaseService;
        private ObservableCollection<PersonWithAddress> items;
        private int currentPage = 1;
        private int itemsPerPage = 10; 

        public ObservableCollection<PersonWithAddress> Items
        {
            get { return items; }
            set { items = value; OnPropertyChanged(nameof(Items)); }
        }

        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand NewCommand { get; }

        public Account CurrentAccount { get; set; }
        public readonly Dashboard dashboard;

        public KundenAdminViewModel(Account CurrentAccount, Dashboard dashboard)
        {
            this.CurrentAccount = CurrentAccount;
            this.dashboard = dashboard;
            databaseService = new DatabaseService();
            Items = new ObservableCollection<PersonWithAddress>();
            GetPersonenWithAdress();
            DeleteCommand = new DeletePersonCommand(this);
            EditCommand = new EditCommand(this);
            NewCommand = new NewCommand();
            Debug.WriteLine(CurrentAccount);
        }

        public async void GetPersonenWithAdress()
        {
            Items.Clear();
            try
            {
                var currentPage = 1; 
                var itemsPerPage = 10; 
                var Personen = await databaseService.GetPersonen(currentPage, itemsPerPage);
                var auth = new Auth();
                foreach (var person in Personen)
                {
                    var address = await databaseService.GetAnschriftByID(person.Id);
                    if (address != null)
                    {
                        PersonWithAddress kunde = new PersonWithAddress(person, address);
                        Items.Add(kunde);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while fetching persons");
            }
        }


        public void NextPage()
        {
            currentPage++;
            GetPersonenWithAdress();
        }

        public void PreviousPage()
        {
            if (currentPage > 1)
            {
                currentPage--;
                GetPersonenWithAdress();
            }
        }
    }
}
