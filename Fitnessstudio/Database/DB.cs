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
            _connectionString = "Host=db.fs223.de;Username=gruppe2;Password=FS223@Gruppe2!;Database=fitnessstudio";
        }

        public async Task<NpgsqlConnection> GetConnection()
        {
            NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }
    }
}