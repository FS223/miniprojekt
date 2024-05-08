using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitnessstudio
{
    internal class User
    {

        private string username;
        private string password;
        private string email;
        private int id;

        public User(string username, string password, string email, int id)
        {
            this.username = username;
            this.password = password;
            this.email = email;
            this.id = id;
        }

        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Email { get => email; set => email = value; }
        public int Id { get => id; set => id = value; }


        public override string ToString()
        {
            return $"ID: {id}, Username: {username}, Password: {password},  Email: {email}";
        }
    }
}
