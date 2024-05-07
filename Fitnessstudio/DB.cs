using System;
using Npgsql;
using System.Data;

public static class DB
{
    private readonly string _connectionString = $"Host={host};Username={user};Password={pass};Database={database}";

    private async DataTable ExecuteQuery(string query)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            using (var command = new NpgsqlCommand(query, connection))
            {
                var dataTable = new DataTable();
                var dataAdapter = new NpgsqlDataAdapter(command);
                dataAdapter.Fill(dataTable);
                return dataTable;
            }
        }
    }

    public async DataTable getData(String query) {
        DataTable result = await ExecuteQuery(query);
        return result;
    }

    public async DataTable getKursData() {
        string query = "SELECT * FROM kursverwaltung";
        DataTable result = await ExecuteQuery(query);
        return result;
    }
}
