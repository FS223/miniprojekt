using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Fitnessstudio.Views
{
    /// <summary>
    /// Interaction logic for Kurse.xaml
    /// </summary>
    public partial class Kurse : Page
    {
        public Kurse()
        {
            InitializeComponent();

            //Test();
            KurseAbrufen();
        }

        /// <summary>
        /// Ruft asynchron alle in der Datenbank enthaltenen Kurseinträge ab.
        /// </summary>
        public async void KurseAbrufen()
        {
            DatabaseService db = new DatabaseService();
            List<Kurs> kursliste = await db.GetKurse();
            //foreach (Kurs kurs in kursliste)
            //{
            //    MessageBox.Show(kurs.Bezeichnung);
            //}
            KurslisteFuellen(kursliste);

        }

        /// <summary>
        /// Füllt die grobe Kurslistenübersicht mit den bereits auf der Datenbank zu findenden Kurseinträgen.
        /// </summary>
        /// <param name="kursliste"></param>
        public void KurslisteFuellen(List<Kurs> kursliste = null)
        {
            if (kursliste != null)
            {
                int counter = 0;
                foreach (Kurs kurs in kursliste)
                {
                    string teilnehmer = $"{kurs.MinTeilnehmer}/{kurs.MaxTeilnehmer}";

                    // Create the Grid
                    Grid myGrid = new Grid();

                    // Define the Columns
                    ColumnDefinition colDef1 = new ColumnDefinition();
                    ColumnDefinition colDef2 = new ColumnDefinition();
                    ColumnDefinition colDef3 = new ColumnDefinition();
                    // Set Width in percentage
                    colDef1.Width = new GridLength(50, GridUnitType.Star);
                    colDef2.Width = new GridLength(20, GridUnitType.Star);
                    colDef3.Width = new GridLength(20, GridUnitType.Star);
                    // Add definitions
                    myGrid.ColumnDefinitions.Add(colDef1);
                    myGrid.ColumnDefinitions.Add(colDef2);
                    myGrid.ColumnDefinitions.Add(colDef3);
                    // Label Kursbezeichnung
                    Label l1 = new Label();
                    l1.Content = kurs.Bezeichnung;
                    l1.Margin = new Thickness(5);
                    Grid.SetRow(l1, counter);
                    Grid.SetColumn(l1, 0);
                    // Label Teilnehmer Anzahl / Teilnehmer Maximum
                    Label l2 = new Label();
                    l2.Content = teilnehmer;
                    l2.Margin = new Thickness(5);
                    Grid.SetRow(l2, counter);
                    Grid.SetColumn(l2, 1);
                    // Lösch-Button
                    Button btn_del = new Button
                    {
                        Margin = new Thickness(5),
                        Content = new Image
                        {
                            Height = 25,
                            Source = new BitmapImage(new Uri("/Images/redDeleteIcon.png", UriKind.Relative))//,
                        }
                    };
                    btn_del.SetResourceReference(Control.StyleProperty, "DeleteButtonStyle");
                    Grid.SetRow(btn_del, counter);
                    Grid.SetColumn(btn_del, 2);
                    // Adds elements to created grid
                    myGrid.Children.Add(l1);
                    myGrid.Children.Add(l2);
                    myGrid.Children.Add(btn_del);
                    // Adds grid to the listview
                    KursListe.Items.Add(myGrid);

                    counter++;  // Counter for listview rows
                }
            }
        }

        private void KursHinufuegen_Click(object sender, RoutedEventArgs e) => NeuerKursFrame.Source = new Uri("../pages/Kursdetails.xaml", UriKind.Relative);
    }
}
