using ScottPlot;
using ScottPlot.WPF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Fitnessstudio
{
    /// <summary>
    /// Interaktionslogik für Stoßzeiten.xaml
    /// </summary>
    public partial class Stoßzeiten : Page
    {
        /// <summary>
        /// Konstruktor, Erstellen des Startes und definieren von Berechtigungen
        /// </summary>
        public Stoßzeiten()
        {
            InitializeComponent();
            Timeplot.RightClicked -= Timeplot.DefaultRightClickEvent;
            PlotBarChart();
            Timeplot.Configuration.Pan = false;
            Timeplot.Configuration.Zoom = false;

        }

        /// <summary>
        /// Generieren von dem Bar Plot
        /// </summary>
        private async void PlotBarChart()
        {
            double maxCapacity = 20;
            // Aktuelle Zeit bestimmen und entsprechend im 2 Stunden Rahmen +- den Bereich festlegen
            string currentHourS = DateTime.Now.ToString("HH");
            string currentMinuteS = DateTime.Now.ToString("mm");
            int currentMinute = Convert.ToInt32(currentMinuteS);
            int currentHour = Convert.ToInt32(currentHourS);

            // Stunde aufrunden auf jede viertelstunde
            switch (Math.Round(Convert.ToDouble(currentMinute) / 15))
            {
                case 0:
                    currentMinute = 0; break;
                case 1:
                    currentMinute = 15; break;
                case 2:
                    currentMinute = 30; break;
                case 3:
                    currentMinute = 45; break;
                case 4:
                    currentMinute = 0;
                    currentHour += 1;
                    break;
                default:
                    break;
            }

            DateTime currentDateTime = DateTime.Parse(currentHour + ":" + currentMinute);
            

            DateTime startTime = currentDateTime.AddHours(-2); // Setzt den Start auf 2 Stunden vor der aktuellen Zeit
            DateTime endTime = currentDateTime.AddHours(2);
            endTime = endTime.AddMinutes(30);// Setzt die EndTime auf 2,5h nach der aktuellen Zeit

            TimeSpan interval = TimeSpan.FromMinutes(30); // Halbstündiges Intervall

            var times = Enumerable.Range(0, (int)((endTime - startTime).TotalMinutes / interval.TotalMinutes))
                                  .Select(i => startTime.AddMinutes(i * interval.TotalMinutes))
                                  .ToArray();


            // Berechne die Anzahl der anwesenden Personen für jedes Intervall
            double[] peoplePresent = new double[times.Length];
            for (int i = 0; i < times.Length; i++)
            {
                DatabaseService Db = new DatabaseService();
                peoplePresent[i] = await Db.GetAktuelleAnzahlLeute(times[i]);
            }

            
            
            // Erstelle den Bar Plot
            var plt = Timeplot.Plot;
            plt.Clear();
            plt.AddBar(peoplePresent);
            var bars = plt.AddBar(peoplePresent);
            bars.BorderLineWidth = 1;
            plt.XTicks(times.Select(time => time.ToString("HH:mm")).ToArray());
            plt.XLabel("Zeit");
            plt.YLabel("Anzahl Personen");
            plt.Title("Halbstündliche Auslastung");
            // Setze die y-Achse von 0 bis 100%
            plt.SetAxisLimits(yMin: 0, yMax: maxCapacity);

            #region Styles

            plt.Style(System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.White, System.Drawing.Color.White,null,System.Drawing.Color.White); //Styles            

            bars.Color = System.Drawing.Color.White; // Style der Säulen an sich

            #endregion


                Timeplot.Refresh();


        }

       
    }
}


