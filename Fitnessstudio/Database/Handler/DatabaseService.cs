using Fitnessstudio.Models;
using Npgsql;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitnessstudio
{
    internal class DatabaseService
    {
        DB db;
        public DatabaseService()
        {
            db = new DB();
        }

        public async Task<List<string>> GetTables()
        {
            var tables = new List<string>();

            var connection = await db.GetConnection();
            using (connection)
            {
                if (connection.State == ConnectionState.Closed)
                {
                    await connection.OpenAsync();
                }
                var command = new NpgsqlCommand("SELECT table_name FROM information_schema.tables WHERE table_schema = 'public'", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tables.Add(reader.GetString(0));
                    }
                }
            }
            return tables;
        }

        public async Task<List<Anschrift>> GetAnschriften()
        {
            var anschriften = new List<Anschrift>();
            var connection = await db.GetConnection();
            using (connection)
            {
                if (connection.State == ConnectionState.Closed)
                {
                    await connection.OpenAsync();
                }
                using (var command = new NpgsqlCommand("SELECT * FROM anschrift", connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        anschriften.Add(new Anschrift
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Land = reader.GetString(reader.GetOrdinal("land")),
                            Plz = reader.GetString(reader.GetOrdinal("plz")),
                            Ort = reader.GetString(reader.GetOrdinal("ort")),
                            Strasse = reader.GetString(reader.GetOrdinal("strasse")),
                            Hausnummer = reader.GetString(reader.GetOrdinal("hausnummer")),
                            Zusatz = reader.GetString(reader.GetOrdinal("zusatz"))
                        });
                    }
                }
            }
            return anschriften;
        }

        public async Task<Anschrift?> GetAnschriftByID(int id)
        {
            // Get a connection from the DB instance
            using (var conn = await db.GetConnection())
            {
                await using (var cmd = new NpgsqlCommand("SELECT * FROM anschrift WHERE id = @p1", conn))
                {
                    cmd.Parameters.AddWithValue("@p1", id);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Anschrift {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Land = reader.GetString(reader.GetOrdinal("land")),
                                Plz = reader.GetString(reader.GetOrdinal("plz")),
                                Ort = reader.GetString(reader.GetOrdinal("ort")),
                                Strasse = reader.GetString(reader.GetOrdinal("strasse")),
                                Hausnummer = reader.GetString(reader.GetOrdinal("hausnummer")),
                                Zusatz = reader.GetString(reader.GetOrdinal("zusatz"))
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public async Task<List<Account>> GetAccounts()
        {
            var accounts = new List<Account>();
            var connection = await db.GetConnection();
            using (connection)
            {
                if (connection.State == ConnectionState.Closed)
                {
                    await connection.OpenAsync();
                }
                using (var command = new NpgsqlCommand("SELECT * FROM account", connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        accounts.Add(new Account
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Benutzername = reader.GetString(reader.GetOrdinal("benutzername")),
                            Email = reader.GetString(reader.GetOrdinal("email")),
                            Passwort = reader.GetString(reader.GetOrdinal("passwort")),
                            Rolle = (Rolle)reader.GetInt32(reader.GetOrdinal("rolle")),
                            PersonId = reader.GetInt32(reader.GetOrdinal("personId"))
                        });
                    }
                }
            }
            return accounts;
        }

        public async Task<List<Person>> GetPersonen()
        {
            var persons = new List<Person>();
            var connection = await db.GetConnection();
            using (connection)
            {
                // Doppelte Connection
                if (connection.State == ConnectionState.Closed)
                {
                    await connection.OpenAsync();
                }
                using (var command = new NpgsqlCommand("SELECT * FROM person", connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Enum.TryParse(reader.GetString(reader.GetOrdinal("geschlecht")), out Geschlecht geschlecht);
                        persons.Add(new Person
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Vorname = reader.GetString(reader.GetOrdinal("vorname")),
                            Nachname = reader.GetString(reader.GetOrdinal("nachname")),
                            Geburtsdatum = reader.GetDateTime(reader.GetOrdinal("geburtsdatum")),
                            Geschlecht = geschlecht,
                            AnschriftId = reader.GetInt32(reader.GetOrdinal("anschriftId")),
                            // Fehlende Datenbank Spalten, dadurch Exceptions....
                            // AccountId = reader.IsDBNull(reader.GetOrdinal("accountId")) ? null : reader.GetInt32(reader.GetOrdinal("accountId")),
                            // KundeId = reader.IsDBNull(reader.GetOrdinal("kundeId")) ? null : reader.GetInt32(reader.GetOrdinal("kundeId")),
                            // MitarbeiterId = reader.IsDBNull(reader.GetOrdinal("mitarbeiterId")) ? null : reader.GetInt32(reader.GetOrdinal("mitarbeiterId"))
                        });
                    }
                }
            }
            return persons;
        }

        public async Task<List<Kunde>> GetKunden()
        {
            var kunden = new List<Kunde>();
            var connection = await db.GetConnection();
            using (connection)
            {
                if (connection.State == ConnectionState.Closed)
                {
                    await connection.OpenAsync();
                }
                using (var command = new NpgsqlCommand("SELECT * FROM kunde", connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        kunden.Add(new Kunde
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            PersonId = reader.GetInt32(reader.GetOrdinal("personId")),
                            Guthaben = reader.GetFloat(reader.GetOrdinal("guthaben")),
                            Iban = reader.GetString(reader.GetOrdinal("iban")),
                            Bild = reader.IsDBNull(reader.GetOrdinal("bild")) ? null : reader.GetString(reader.GetOrdinal("bild")),
                            Mitgliedschaft = (Mitgliedschaft)reader.GetInt32(reader.GetOrdinal("mitgliedschaft"))
                        });
                    }
                }
            }
            return kunden;
        }

        public async Task<List<Messung>> GetMessungen()
        {
            var messungen = new List<Messung>();
            var connection = await db.GetConnection();
            using (connection)
            {
                if (connection.State == ConnectionState.Closed)
                {
                    await connection.OpenAsync();
                }
                using (var command = new NpgsqlCommand("SELECT * FROM messung", connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        messungen.Add(new Messung
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Datum = reader.GetDateTime(reader.GetOrdinal("datum")),
                            Gewicht = reader.GetFloat(reader.GetOrdinal("gewicht")),
                            Groesse = reader.GetFloat(reader.GetOrdinal("groesse")),
                            Fettanteil = reader.GetFloat(reader.GetOrdinal("fettanteil")),
                            Muskelmasse = reader.GetFloat(reader.GetOrdinal("muskelmasse")),
                            KundeId = reader.GetInt32(reader.GetOrdinal("kundeId")),
                            Bmi = reader.GetFloat(reader.GetOrdinal("bmi"))
                        });
                    }
                }
            }
            return messungen;
        }

        public async Task<List<Mitarbeiter>> GetMitarbeiter()
        {
            var mitarbeiter = new List<Mitarbeiter>();
            var connection = await db.GetConnection();
            using (connection)
            {
                if (connection.State == ConnectionState.Closed)
                {
                    await connection.OpenAsync();
                }
                using (var command = new NpgsqlCommand("SELECT * FROM mitarbeiter", connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        mitarbeiter.Add(new Mitarbeiter
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            PersonId = reader.GetInt32(reader.GetOrdinal("personId"))
                        });
                    }
                }
            }
            return mitarbeiter;
        }

        public async Task<NpgsqlConnection> GetConnection()
        {
            NpgsqlConnection connection = new NpgsqlConnection("your_connection_string_here");
            return connection;
        }


        public async Task<List<Kurs>> GetKurse()
        {
            var kurse = new List<Kurs>();
            var connection = await db.GetConnection();
            using (connection)
            {
                if (connection.State == ConnectionState.Closed)
                {
                    await connection.OpenAsync();
                }
                using (var command = new NpgsqlCommand("SELECT * FROM kurs", connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        kurse.Add(new Kurs(
                            reader.GetInt32(reader.GetOrdinal("id")),
                            reader.GetString(reader.GetOrdinal("bezeichnung")),
                            reader.IsDBNull(reader.GetOrdinal("beschreibung")) ? null : reader.GetString(reader.GetOrdinal("beschreibung")),
                            reader.GetInt32(reader.GetOrdinal("kursLeiterId")),
                            reader.GetInt32(reader.GetOrdinal("minTeilnehmer")),
                            reader.GetInt32(reader.GetOrdinal("maxTeilnehmer")),
                            reader.GetDecimal(reader.GetOrdinal("preis")),
                            reader.GetInt32(reader.GetOrdinal("dauer"))
                            ));
                        
                       
                    }
                }
            }
            return kurse;
        }

        public async void DeletePersonAsync(Person person)
        {
            try
            {
                var connection = await db.GetConnection();
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
                var connection = await db.GetConnection();
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
