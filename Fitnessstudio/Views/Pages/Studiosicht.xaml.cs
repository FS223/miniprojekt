using Syncfusion.UI.Xaml.Scheduler;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;
using Npgsql;
using System.Windows.Media;

namespace Fitnessstudio.Views
{
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
            Schedule.ViewType = SchedulerViewType.TimelineDay;
            Schedule.TimelineViewSettings.TimeRulerFormat = "hh mm";

            LoadAppointments(); // Lade Termine beim Initialisieren der Seite
        }

        private async void LoadAppointments()
        {
            try
            {
                DB db = new DB();

                // Verbindung zur Datenbank herstellen
                using (var connection = await db.GetConnection())
                {
                    // Prüfen, ob die Verbindung geöffnet ist
                    if (connection.State != System.Data.ConnectionState.Open)
                    {
                        await connection.OpenAsync();
                    }

                    // SQL-Abfrage zum Abrufen der Termine aus der Tabelle Termin
                    var sql = "SELECT bezeichnung, termin.\"startZeit\", dauer FROM termin";

                    // Befehl zum Ausführen der SQL-Abfrage
                    using (var command = new NpgsqlCommand(sql, connection))
                    {
                        // Daten aus der Abfrage lesen
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            string hexCode = "#d8042c";
                            Color color = (Color)ColorConverter.ConvertFromString(hexCode);
                            var scheduleAppointmentCollection = new ScheduleAppointmentCollection();

                            while (await reader.ReadAsync())
                            {
                                // Daten aus der Datenbank lesen
                                string bezeichnung = reader.GetString(0);
                                DateTime startZeit = reader.GetDateTime(1);
                                int dauerMinutes = reader.GetInt16(2);
                                TimeSpan dauer = TimeSpan.FromMinutes(dauerMinutes);
                                // Endzeit berechnen
                                DateTime endZeit = startZeit.Add(dauer);

                                // ScheduleAppointment erstellen und zur Sammlung hinzufügen
                                scheduleAppointmentCollection.Add(new ScheduleAppointment()
                                {
                                    StartTime = startZeit,
                                    EndTime = endZeit,
                                    Subject = bezeichnung + "\nPaderborn",
                                    Location = "Paderborn",
                                    AppointmentBackground = new SolidColorBrush(color)

                                });
                            }

                            // Datenquelle für den Scheduler setzen
                            Schedule.ItemsSource = scheduleAppointmentCollection;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Fehlerbehandlung
                Console.WriteLine("Fehler beim Laden der Termine: " + ex.Message);
            }
        }
    }
}
