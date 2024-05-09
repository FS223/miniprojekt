using DotNetEnv;
using Npgsql;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Fitnessstudio
{
    public class DB
    {
        private string? _connectionString;

        public DB()
        {
            var root = Directory.GetCurrentDirectory();
            var dotenv = System.IO.Path.Combine(root, ".env");
            Env.Load(dotenv);

            _connectionString = Environment.GetEnvironmentVariable("TEST");

            if (string.IsNullOrEmpty(_connectionString))
            {
                //throw new ArgumentNullException("TEST not found in .env");
            }

            _connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
            if (string.IsNullOrEmpty(_connectionString))
            {
                //throw new ArgumentNullException("CONNECTION_STRING not found in .env");
            }

            Trace.WriteLine("Connection string: " + _connectionString);
        }

        public async Task<NpgsqlConnection> GetConnection()
        {
            NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }
    }
}