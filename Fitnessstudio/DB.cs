using System;
using Npgsql;
using System.Data;

public static class DB
{
    private static readonly string _connectionString = $"{SQL_DATA}";

    private static DataTable ExecuteQuery(string query)
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

    public static DataTable GetData(String query) { return ExecuteQuery(query); }

    public static void SetData(String query) { ExecuteQuery(query); }

    public static void AddData(String query) { ExecuteQuery(query); }

    // ============== Kundenverwaltung ==============

    public static DataTable GetKunden() {
        return GetData("SELECT * FROM kunde");
    }

    // ==============  Kursverwaltung  ==============

    public static DataTable GetKurse() {
        return GetData("SELECT * FROM kurs");
    }

    // ==============   Datenanalyse   ==============
    
    public static DataTable GetDaten() {
        return GetData("SELECT * FROM daten");
    }
    
}
