using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatenbankGUI
{
    internal class Database
    {

        private static NpgsqlDataSource dataSource;

        public Database()
        {
            InitDatabaseAsync();
        }

        private async void InitDatabaseAsync()
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder("Host=fs223.de;Username=YourGroup;Password=YourPassword;Database=fitnessstudio");
            dataSource = dataSourceBuilder.Build();
        }



        public static async ValueTask<NpgsqlConnection> GetConnection()
        {
            return await dataSource.OpenConnectionAsync();
        }

    }
}
