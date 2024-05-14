﻿using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Fitnessstudio.Commands;
using Fitnessstudio.Models;
using Npgsql;
using Serilog;

namespace Fitnessstudio.ViewModels
{
    public class KundenAdminViewModel : BaseViewModel
    {
        private readonly DatabaseService databaseService;
        private ObservableCollection<PersonWithAddress> items;

        public ObservableCollection<PersonWithAddress> Items { get { return items; } set { items = value; OnPropertyChanged(nameof(Items)); } }

        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand NewCommand { get; }

        private Account CurrentAccount { get; set; }



        public KundenAdminViewModel(Account CurrentAccount)
        {
            databaseService = new DatabaseService();
            Items = new ObservableCollection<PersonWithAddress>();
            GetPersonenWithAdress();
            DeleteCommand = new DeletePersonCommand(databaseService.DeletePersonAsync);
            EditCommand = new EditCommand();
            NewCommand = new NewCommand();
            this.CurrentAccount = CurrentAccount;
            Debug.WriteLine(CurrentAccount);
        }

        private async void GetPersonenWithAdress()
        {
            try
            {
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
            catch (Exception ex)
            {
                Log.Error(ex, "Error while fetching persons");
            }
        }

        // @Riad, habe dein Code in DatabaseSevice umgelagert.
    }
}
