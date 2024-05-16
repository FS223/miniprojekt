using DotNetEnv;
using Fitnessstudio.Models;
using Fitnessstudio.Views;
using Npgsql;
using ScottPlot.Renderable;
using ScottPlot.Statistics.Interpolation;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

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
                            return new Anschrift
                            {
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

        public async Task<Kunde> GetKundeByID(int id)
        {
            var connection = await db.GetConnection();
            using (connection)
            {
                if (connection.State == ConnectionState.Closed)
                {
                    await connection.OpenAsync();
                }
                await using (var cmd = new NpgsqlCommand("SELECT * FROM kunde WHERE id = @p1", connection))
                {
                    cmd.Parameters.AddWithValue("@p1", id);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            if (!Enum.TryParse(reader.GetString(reader.GetOrdinal("mitgliedschaft")), out Mitgliedschaft mitgliedschaft))
                            {
                                mitgliedschaft = Mitgliedschaft.BRONZE;
                            }
                            return new Kunde
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                PersonId = reader.GetInt32(reader.GetOrdinal("personId")),
                                Guthaben = (float)reader.GetDouble(reader.GetOrdinal("guthaben")),
                                Iban = reader.GetString(reader.GetOrdinal("iban")),
                                Bild = reader.IsDBNull(reader.GetOrdinal("bild")) ? null : reader.GetString(reader.GetOrdinal("bild")),
                                Mitgliedschaft = mitgliedschaft
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

        public async Task<Person> GetPersonByID(int id)
        {
            // Get a connection from the DB instance
            using (var conn = await db.GetConnection())
            {
                await using (var cmd = new NpgsqlCommand("SELECT * FROM person WHERE id = @p1", conn))
                {
                    cmd.Parameters.AddWithValue("@p1", id);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            Enum.TryParse(reader.GetString(reader.GetOrdinal("geschlecht")), out Geschlecht geschlecht);
                            return new Person
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

        public async Task<Account> GetAccountById(int id)
        {
            // Get a connection from the DB instance
            using (var conn = await db.GetConnection())
            {
                await using (var cmd = new NpgsqlCommand("SELECT * FROM account WHERE id = @p1", conn))
                {
                    cmd.Parameters.AddWithValue("@p1", id);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            Enum.TryParse(reader.GetString(reader.GetOrdinal("rolle")), out Rolle rolle);
                            return new Account
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Benutzername = reader.GetString(reader.GetOrdinal("benutzername")),
                                Email = reader.GetString(reader.GetOrdinal("email")),
                                Passwort = reader.GetString(reader.GetOrdinal("passwort")),
                                Rolle = rolle,
                                PersonId = reader.GetInt32(reader.GetOrdinal("personId"))
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

        public async Task<bool> UpdatePersonByID(int id, Person updatedPerson)
        {
            // Get a connection from the DB instance
            using (var conn = await db.GetConnection())
            {
                //*
                //  accountId = @accountId, 
                //  kundeId = @kundeId, 
                //  mitarbeiterId = @mitarbeiterId,
                //*// 

                await using (var cmd = new NpgsqlCommand(@"UPDATE person 
                                           SET vorname = @vorname, 
                                               nachname = @nachname, 
                                               geburtsdatum = @geburtsdatum, 
                                               geschlecht = @geschlecht, 
                                               anschriftId = @anschriftId
                                           WHERE id = @id", conn))

                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@vorname", updatedPerson.Vorname);
                    cmd.Parameters.AddWithValue("@nachname", updatedPerson.Nachname);
                    cmd.Parameters.AddWithValue("@geburtsdatum", updatedPerson.Geburtsdatum);
                    cmd.Parameters.AddWithValue("@geschlecht", updatedPerson.Geschlecht.ToString());
                    cmd.Parameters.AddWithValue("@anschriftId", updatedPerson.AnschriftId);
                    //cmd.Parameters.AddWithValue("@accountId", (object)updatedPerson.AccountId ?? DBNull.Value);
                    //cmd.Parameters.AddWithValue("@kundeId", (object)updatedPerson.KundeId ?? DBNull.Value);
                    //cmd.Parameters.AddWithValue("@mitarbeiterId", (object)updatedPerson.MitarbeiterId ?? DBNull.Value);


                    int rowsAffected = await cmd.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
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

        public async Task<List<Person>> GetPersonen(int currentPage, int itemsPerPage)
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
                            Iban = reader.IsDBNull(reader.GetOrdinal("iban")) ? null : reader.GetString(reader.GetOrdinal("iban")),
                            Bild = reader.IsDBNull(reader.GetOrdinal("bild")) ? null : reader.GetString(reader.GetOrdinal("bild")),
                            Mitgliedschaft = reader.IsDBNull(reader.GetOrdinal("mitgliedschaft")) ? (Mitgliedschaft?)null : EnumHelper.ConvertStringToEnum(reader.GetString(reader.GetOrdinal("mitgliedschaft"))),
                            NiederlassungID = reader.GetInt32(reader.GetOrdinal("niederlassungId"))
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
                            reader.GetDecimal(reader.GetOrdinal("preis"))
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
                    var command2 = new NpgsqlCommand("DELETE FROM messung WHERE id = @id", connection);
                    command2.Parameters.AddWithValue("@id", person.Id);
                    await command2.ExecuteNonQueryAsync();
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

        /// <summary>
        /// Ermittelt die aktuelle Anzahl an Personen zu einer bestimmten Uhrzeit
        /// </summary>
        /// <param name="currentTime"></param>
        /// <returns>Anzahl anwesender Personen</returns>
        public async Task<int> GetAktuelleAnzahlLeute(DateTime currentTime)
        {
            string timeString = currentTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            int amount = 0;
            var zeitenbuchung = new List<ZeitenBuchung>();
            var connection = await db.GetConnection();
            using (connection)
            {
                if (connection.State == ConnectionState.Closed)
                {
                    await connection.OpenAsync();
                }
                using (var command = new NpgsqlCommand("SELECT * FROM \"zeitenBuchung\" zb WHERE \"checkIn\" < '" + timeString + "' AND \"checkOut\" > '" + timeString + "'", connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        amount++;
                    }
                }
            }
            return amount;
        }


        /// <summary>
        /// To be Added
        /// </summary>
        /// <param name="currentTime"></param>
        /// <returns></returns>
        public async Task<int> GetGeplanteAnzahlLeute(DateTime currentTime)
        {
            string timeString = currentTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            int amount = 0;
            var zeitenbuchung = new List<ZeitenBuchung>();
            var connection = await db.GetConnection();
            using (connection)
            {
                if (connection.State == ConnectionState.Closed)
                {
                    await connection.OpenAsync();
                }
                using (var command = new NpgsqlCommand("SELECT * FROM \"zeitenBuchung\" zb WHERE \"checkIn\" < '" + timeString + "' AND \"checkOut\" > '" + timeString + "'", connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        amount++;
                        //zeitenbuchung.Add(new ZeitenBuchung
                        //{
                        //    Id = reader.GetInt32(reader.GetOrdinal("id")),
                        //    CheckIn = reader.GetDateTime(reader.GetOrdinal("checkIn")),
                        //    CheckOut = reader.GetDateTime(reader.GetOrdinal("checkOut")),
                        //    KundeId = reader.GetInt32(reader.GetOrdinal("kundeId"))
                        //});
                    }
                }
            }
            return amount;
        }

    }

    public static class EnumHelper
    {
        public static TEnum ConvertIntToEnum<TEnum>(int value) where TEnum : struct, Enum
        {
            if (Enum.IsDefined(typeof(TEnum), value))
            {
                return (TEnum)Enum.ToObject(typeof(TEnum), value);
            }
            else
            {
                throw new ArgumentException($"The value {value} does not correspond to any enum value in {typeof(TEnum).Name}.");
            }
        }


        public static Mitgliedschaft ConvertStringToEnum(string s)
        {
            switch (s.ToLower())
            {
                case "bronze":
                    return Mitgliedschaft.BRONZE;
                case "silber":
                    return Mitgliedschaft.SILBER;
                case "gold":
                    return Mitgliedschaft.GOLD;
                case "platinum":
                    return Mitgliedschaft.PLATINUM;
                default:
                    throw new ArgumentException("Invalid string: " + s);
            }
        }

    }


}
