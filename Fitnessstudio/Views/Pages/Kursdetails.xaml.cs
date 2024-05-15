using Npgsql.PostgresTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Fitnessstudio.Views.Pages
{
    /// <summary>
    /// Interaktionslogik für Kursdetails.xaml
    /// </summary>
    public partial class Kursdetails : Page
    {
        public Kursdetails()
        {
            InitializeComponent();
        }

        public void KursdetailsSpeichern_Click(object sender, RoutedEventArgs e)
        {
            string bezeichnung = bezeichnung_tb.Text;
            string beschreibung = beschreibung_tb.Text;
            int kursleiterId;
            int.TryParse(kursleiter_tb.Text, out kursleiterId);

            int minTeilnehmer = 0;
            int.TryParse(minTeilnehmer_tb.Text, out minTeilnehmer);

            int maxTeilnehmer;
            int.TryParse(maxTeilnehmer_tb.Text, out maxTeilnehmer);

            float preis = 0;
            float.TryParse(preis_tb.Text, out preis);

            Kurs kurs = new Kurs(bezeichnung, beschreibung, kursleiterId, minTeilnehmer, maxTeilnehmer, preis);

            DatabaseService databaseService = new DatabaseService();
            Task addKursTask = databaseService.AddKurs(kurs);
        }

        public void KursdetailsBearbeiten_Click(object sender, RoutedEventArgs e)
        {
            KursdetailsSpeichern_btn.IsEnabled = true;
            KursdetailsBearbeiten_btn.IsEnabled = false;
        }

        public void KursdetailsAbbrechen_Click(object sender, RoutedEventArgs e)
        {
  
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private static readonly Regex numberRegex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !numberRegex.IsMatch(text);
        }
    }
}
