using DotNetEnv;
using Npgsql;
using System;
using System.Threading.Tasks;

namespace Fitnessstudio
{
    public class DB
    {
        private readonly string? _connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

        public DB()
        {
            Env.Load();
            _connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new ArgumentNullException("CONNECTION_STRING not found in .env");
            }
        }

        public async Task<NpgsqlConnection> GetConnection()
        {
            NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }
    }
}