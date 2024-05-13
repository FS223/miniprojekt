using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Fitnessstudio.ViewModels
{
    public class KundenAdminViewModel : BaseViewModel
    {
        private ObservableCollection<Person> items;
        public ObservableCollection<Person> Items { get { return items; } set { items = value; OnPropertyChanged(nameof(Items)); } }

        public KundenAdminViewModel()
        {
            Items = new ObservableCollection<Person>();
            GetPersonen();
        }

        private async void GetPersonen()
        {
            var databaseService = new DatabaseService();
            var Personen = await databaseService.GetPersonen();
            foreach (var person in Personen)
            {
                Items.Add(person);
            }
        }
    }
}
