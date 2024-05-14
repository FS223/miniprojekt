using System;
using System.Collections.ObjectModel;
using System.Data;
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



        public KundenAdminViewModel()
        {
            databaseService = new DatabaseService();
            Items = new ObservableCollection<PersonWithAddress>();
            GetPersonen();
            DeleteCommand = new DeletePersonCommand(DeletePersonAsync);
            EditCommand = new EditCommand();
            NewCommand = new NewCommand();
        }

        private async void GetPersonen()
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

        public async void DeletePersonAsync(Person person)
        {
            try
            {
                var connection = await databaseService.GetConnection();
                using (connection)
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        await connection.OpenAsync();
                    }

                    var command = new NpgsqlCommand("DELETE FROM person WHERE id = @id", connection);
                    command.Parameters.AddWithValue("@id", person.Id);

                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting person");
            }
        }


        public async Task AddAnschrift(Anschrift anschrift)
        {
            try
            {
                var connection = await databaseService.GetConnection();
                using (connection)
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        await connection.OpenAsync();
                    }

                    var command = new NpgsqlCommand("INSERT INTO anschrift (land, plz, ort, strasse, hausnummer, zusatz) VALUES (@land, @plz, @ort, @strasse, @hausnummer, @zusatz)", connection);
                    command.Parameters.AddWithValue("@land", anschrift.Land);
                    command.Parameters.AddWithValue("@plz", anschrift.Plz);
                    command.Parameters.AddWithValue("@ort", anschrift.Ort);
                    command.Parameters.AddWithValue("@strasse", anschrift.Strasse);
                    command.Parameters.AddWithValue("@hausnummer", anschrift.Hausnummer);
                    command.Parameters.AddWithValue("@zusatz", anschrift.Zusatz);

                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while adding address");
            }
        }
    }
}
