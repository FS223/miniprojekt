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
        Kurs kurs;
        List<TextBox> editableTB = new List<TextBox>();
        public Kursdetails()
        {
            InitializeComponent();
            FillEditableTB();
            foreach(TextBox tb in editableTB)
            {
                tb.IsEnabled = true;
            }
        }

        public Kursdetails(Kurs kurs)
        {
            InitializeComponent();
            FillEditableTB();

            KursdetailsSpeichern_btn.IsEnabled = false;
            KursdetailsBearbeiten_btn.Visibility = Visibility.Visible;
            KursdetailsLoeschen_btn.Visibility = Visibility.Visible;
            this.kurs = kurs;
            id_tb.Text = kurs.Id.ToString();
            bezeichnung_tb.Text = kurs.Bezeichnung;
            beschreibung_tb.Text = kurs.Beschreibung;
            kursleiter_tb.Text = kurs.KursLeiterId.ToString();
            minTeilnehmer_tb.Text = kurs.MinTeilnehmer.ToString();
            maxTeilnehmer_tb.Text = kurs.MaxTeilnehmer.ToString();
            //dauer_tb.Text = 
            preis_tb.Text = kurs.Preis.ToString();
        }

        private void FillEditableTB()
        {
            editableTB.Add(bezeichnung_tb);
            editableTB.Add(beschreibung_tb);
            editableTB.Add(kursleiter_tb);
            editableTB.Add(minTeilnehmer_tb);
            editableTB.Add(maxTeilnehmer_tb);
            //editableTB.Add(dauer_tb);
            editableTB.Add(preis_tb);
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

            CloseKursdetails();
        }

        private async void KursLoeschen_Click(object sender, RoutedEventArgs e)
        {
            DatabaseService databaseService = new DatabaseService();
            await databaseService.DeleteKursAsync(kurs);
            CloseKursdetails();
        }

        public void KursdetailsBearbeiten_Click(object sender, RoutedEventArgs e)
        {
            KursdetailsSpeichern_btn.IsEnabled = true;
            KursdetailsBearbeiten_btn.IsEnabled = false;
            foreach(TextBox tb in editableTB)
            {
                tb.IsEnabled = true;
            }
        }

        public void KursdetailsAbbrechen_Click(object sender, RoutedEventArgs e)
        {
            CloseKursdetails();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CloseKursdetails()
        {
            NavigationService.GoBack();
        }

        private static readonly Regex numberRegex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !numberRegex.IsMatch(text);
        }
    }
}
