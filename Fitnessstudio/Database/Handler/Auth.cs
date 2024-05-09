using Npgsql;
using System.Diagnostics;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Fitnessstudio
{
    public class Auth
    {
        DB db;

        public Auth()
        {
            db = new DB();
        }

        public async Task<bool> HandleLoginAsync(string username, string password)
        {
            Account? user = await GetUserByUsernameAsync(username);
            if (user != null)
            {
                return BCrypt.Net.BCrypt.Verify(password, user.Passwort);
            }
            else
            {
                return false;
            }
        }

        // for each user in db check if password is hashed and hash it if not update password
        public async Task UpdatePasswordsAsync()
        {
            using (var conn = await db.GetConnection())
            {
                await using (var cmd = new NpgsqlCommand("SELECT * FROM account", conn))
                {
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var user = new Account
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Benutzername = reader.GetString(reader.GetOrdinal("benutzername")),
                                Email = reader.GetString(reader.GetOrdinal("email")),
                                Passwort = reader.GetString(reader.GetOrdinal("passwort")),
                                Rolle = Util.ConvertToRolle(reader.GetString(reader.GetOrdinal("rolle"))),
                                PersonId = reader.GetInt32(reader.GetOrdinal("personId"))
                            };

                            // if password is not hashed hash it
                            if (!user.Passwort.StartsWith("$2a$"))
                            {
                                UpdatePasswordAsync(user.Benutzername, user.Passwort);
                            }

                        }
                    }
                }
            }
        }

        private async void UpdatePasswordAsync(string username, string password)
        {
            var user = await GetUserByUsernameAsync(username);
            if (user != null)
            {
                user.Passwort = BCrypt.Net.BCrypt.HashPassword(password);
                await UpdateUserAsync(user);
            }
        }


        private async Task UpdateUserAsync(Account user)
        {
            using (var conn = await db.GetConnection())
            {
                await using (var cmd = new NpgsqlCommand("UPDATE account SET passwort = @p3 WHERE id = @p6", conn))
                {
                    cmd.Parameters.AddWithValue("@p3", user.Passwort);
                    cmd.Parameters.AddWithValue("@p6", user.Id);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }


        private async Task<Account?> GetUserByIdAsync(int id)
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
                            return new Account
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Benutzername = reader.GetString(reader.GetOrdinal("benutzername")),
                                Email = reader.GetString(reader.GetOrdinal("email")),
                                Passwort = reader.GetString(reader.GetOrdinal("passwort")),
                                Rolle = Util.ConvertToRolle(reader.GetString(reader.GetOrdinal("rolle"))),
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

        private async Task<Account?> GetUserByUsernameAsync(string username)
        {
            // Get a connection from the DB instance
            using (var conn = await db.GetConnection())
            {
                await using (var cmd = new NpgsqlCommand("SELECT * FROM account WHERE benutzername = @p1", conn))
                {
                    cmd.Parameters.AddWithValue("@p1", username);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Account
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Benutzername = reader.GetString(reader.GetOrdinal("benutzername")),
                                Email = reader.GetString(reader.GetOrdinal("email")),
                                Passwort = reader.GetString(reader.GetOrdinal("passwort")),
                                Rolle = Util.ConvertToRolle(reader.GetString(reader.GetOrdinal("rolle"))),
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
    }
}
