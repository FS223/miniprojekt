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

    // ============== Kundenverwaltung ==============

    public async DataTable getKunden() {
        string query = "SELECT * FROM kunde";
        DataTable result = await ExecuteQuery(query);
        return result;
    }

    // ==============  Kursverwaltung  ==============

    public async DataTable getKurse() {
        string query = "SELECT * FROM kurs";
        DataTable result = await ExecuteQuery(query);
        return result;
    }

    // ==============   Datenanalyse   ==============
    
    public async DataTable getDaten() {
        string query = "SELECT * FROM daten";
        DataTable result = await ExecuteQuery(query);
        return result;
    }

    
}
