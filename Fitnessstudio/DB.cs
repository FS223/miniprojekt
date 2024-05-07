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

    // ============== Datenbankfunktionen ==============

    public async DataTable getData(String query) { return await ExecuteQuery(query); }

    public async void setData(String query) { await ExecuteQuery(query); }

    public async void addData(String query) { await ExecuteQuery(query); }

    // ============== Kundenverwaltung ==============

    public async DataTable getKunden() {
        return await getData("SELECT * FROM kunde");
    }

    // ==============  Kursverwaltung  ==============

    public async DataTable getKurse() {
        return await getData("SELECT * FROM kurs");
    }

    // ==============   Datenanalyse   ==============
    
    public async DataTable getDaten() {
        return await getData("SELECT * FROM daten");
    }
    
}
