using System;
using Npgsql;
using System.Data;

public static class DB
{
    private readonly string _connectionString;

    private PostgresDatabase()
    {
        _connectionString = $"Host={host};Username={user};Password={pass};Database={database}";
    }

    private DataTable ExecuteQuery(string query)
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

    public DataTable getData(String query) {
        PostgresDatabase db = new PostgresDatabase();

        DataTable result = db.ExecuteQuery(query);
        return result;
    }

    public DataTable getKursData() {
        PostgresDatabase db = new PostgresDatabase();

        string query = "SELECT * FROM kursverwaltung";
        DataTable result = db.ExecuteQuery(query);
        return result;
    }
}
