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
        public Stoßzeiten()
        {
            InitializeComponent();
            Timeplot.RightClicked -= Timeplot.DefaultRightClickEvent;
            PlotBarChart();
            Timeplot.Configuration.Pan = false;
            Timeplot.Configuration.Zoom = false;
            //string currentTime = DateTime.Now.ToString("HH:mm");
            //DateTime timeOpen = new(1985, 09, 24, 9, 30, 0); // 9:30 AM
            //DateTime timeClose = new(1985, 09, 24, 16, 0, 0); // 4:00 PM
            //TimeSpan timeSpan = TimeSpan.FromMinutes(30); // 10 minute bins
            //double[] values = { 5, 10, 7, 13 };
            ////Timeplot.Plot.Add.Bars(values);

            //Timeplot.Plot.Axes.DateTimeTicksBottom();
            //// tell the plot to autoscale with no padding beneath the bars
            //Timeplot.Plot.Axes.Margins(bottom: 0);

            //Timeplot.Plot.SavePng("demo.png", 400, 300);




            //List<OHLC> prices = new();
            //for (DateTime dt = timeOpen; dt <= timeClose; dt += timeSpan)
            //{
            //    double open = Generate.RandomNumber(20, 40) + prices.Count;
            //    double close = Generate.RandomNumber(20, 40) + prices.Count;
            //    double high = Math.Max(open, close) + Generate.RandomNumber(5);
            //    double low = Math.Min(open, close) - Generate.RandomNumber(5);
            //    prices.Add(new OHLC(open, high, low, close, dt, timeSpan));
            //}




            //Timeplot.SavePng("demo.png", 400, 300);

        }


        private void PlotBarChart()
        {

            // Simuliere Check-In und Check-Out Daten
            string currentHourS = DateTime.Now.ToString("HH");
            string currentMinuteS = DateTime.Now.ToString("mm");
            int currentMinute = Convert.ToInt32(currentMinuteS);
            int currentHour = Convert.ToInt32(currentHourS);
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

            DateTime startTime = currentDateTime.AddHours(-2); // Start um 8 Uhr morgens
            DateTime endTime = currentDateTime.AddHours(2); // Endet um 20 Uhr
            TimeSpan interval = TimeSpan.FromMinutes(30); // Halbstündiges Intervall

            var times = Enumerable.Range(0, (int)((endTime - startTime).TotalMinutes / interval.TotalMinutes))
                                  .Select(i => startTime.AddMinutes(i * interval.TotalMinutes))
                                  .ToArray();

            Random rand = new Random();
            var checkIns = times.Select(time => rand.Next(0, 60)).ToArray(); // Zufällige Check-Ins
            var checkOuts = times.Select(time => rand.Next(0, 60)).ToArray(); // Zufällige Check-Outs

            // Berechne die Anzahl der anwesenden Personen für jedes Intervall
            int[] peoplePresent = new int[times.Length];
            int currentPeople = 0;
            for (int i = 0; i < times.Length; i++)
            {
                currentPeople += checkIns[i];
                currentPeople -= checkOuts[i];
                if (currentPeople < 0)
                {
                    currentPeople = 0;
                }
                if (currentPeople > 100)
                {
                    currentPeople = 99;
                }
                peoplePresent[i] = currentPeople;
            }

            // Normiere die Werte auf die prozentuale Auslastung (angenommenes Maximum: 100 Personen)
            double maxCapacity = 100.0;
            double[] utilization = peoplePresent.Select(p => (p / maxCapacity) * 100).ToArray();

            // Erstelle den Bar Plot
            var plt = Timeplot.Plot;
            plt.Clear();
            plt.AddBar(utilization);
            plt.Style(System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.White,System.Drawing.Color.White);
            var bars = plt.AddBar(utilization);
            bars.BorderLineWidth = 1;
            plt.XTicks(times.Select(time => time.ToString("HH:mm")).ToArray());
            plt.XLabel("Zeit");
            plt.YLabel("Auslastung (%)");
            plt.Title("Halbstündliche Auslastung");

            // Setze die y-Achse von 0 bis 100%
            plt.SetAxisLimits(yMin: 0, yMax: 100);

            Timeplot.Refresh();


        }
    }
}


