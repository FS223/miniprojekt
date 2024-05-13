using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using Fitnessstudio.Commands;
using Fitnessstudio.Models;

namespace Fitnessstudio.ViewModels
{
    public class KundenAdminViewModel : BaseViewModel
    {
        private ObservableCollection<PersonWithAddress> items;
        public ObservableCollection<PersonWithAddress> Items { get { return items; } set { items = value; OnPropertyChanged(nameof(Items)); } }

        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand NewCommand { get; }

        public KundenAdminViewModel()
        {
            Items = new ObservableCollection<PersonWithAddress>();
            GetPersonen();
            DeleteCommand = new DeleteCommand();
            EditCommand = new EditCommand();
            NewCommand = new NewCommand();
        }

        private async void GetPersonen()
        {
            var databaseService = new DatabaseService();
            var Personen = await databaseService.GetPersonen();
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
    }
}
