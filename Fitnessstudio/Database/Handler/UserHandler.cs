using Npgsql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Fitnessstudio
{
    internal class UserHandler
    {

        public UserHandler() { }

        
        public async Task<User?> GetUser(int id)
        {
            var conn = await Database.GetConnection();
            await using var cmd = new NpgsqlCommand("SELECT * FROM account WHERE id = @p1", conn) 
            {
                Parameters =
                {
                    new NpgsqlParameter("@p1", id)
                }
            };

            string username = "", password = "", email = "";

            var reader = await cmd.ExecuteReaderAsync();
            if (reader.Read())
            {
                username = reader.GetString(reader.GetOrdinal("benutzername"));
                password = reader.GetString(reader.GetOrdinal("passwort"));
                email = reader.GetString(reader.GetOrdinal("email"));
                User user = new User(username, password, email, id);
                return user;
            }
            return null;
        }

        public async Task<User?> GetUser(string username, string password, string salt)
        {
            var conn = await Database.GetConnection();
            await using var cmd = new NpgsqlCommand("SELECT * FROM account WHERE benutzername = @p1 AND passwort = @p2", conn)
            {
                Parameters =
                {
                    new NpgsqlParameter("@p1", username),
                    new NpgsqlParameter("@p2", BCrypt.Net.BCrypt.HashPassword(password, salt))
                }
            };

            var reader = await cmd.ExecuteReaderAsync();
            if (reader.Read())
            {
                username = reader.GetString(reader.GetOrdinal("benutzername"));
                password = reader.GetString(reader.GetOrdinal("passwort"));
                string email = reader.GetString(reader.GetOrdinal("email"));
                int id = reader.GetInt32(reader.GetOrdinal("id"));
                User user = new User(username, password, email, id);
                return user;
            }
            return null;
        }

       

        public async void HandleLogin(string username, string password, Label errorLabel)
        {
            if (username.Length > 0 && password.Length > 0)
            {
                string salt = BCrypt.Net.BCrypt.GenerateSalt(10); //10rounds
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

                Trace.WriteLine(hashedPassword);

                string hs = BCrypt.Net.BCrypt.HashString(password);

                Trace.WriteLine("hs: " + hs);

                User? user = await GetUser(username, password, salt);
                //Trace.WriteLine(BCrypt.Net.BCrypt.Verify(password, hashedPassword));
                Trace.WriteLine(user == null);
                if (!BCrypt.Net.BCrypt.Verify(password, hashedPassword))
                {
                    errorLabel.Content = "Username oder Passwort falsch!";
                    return;
                }

                //Verify user
                errorLabel.Content = "Erfolgreich eingeloggt! " ;
            }
            else
            {
                errorLabel.Content = "Username oder Passwort Feld ist leer!";
            }
        }
    }
}
