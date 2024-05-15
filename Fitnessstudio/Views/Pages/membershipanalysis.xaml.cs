using MindFusion.Mapping;
using ScottPlot;
using System;
using System.Collections.Generic;
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

namespace Fitnessstudio.Views.Pages
{
    /// <summary>
    /// Interaktionslogik für membershipanalysis.xaml
    /// </summary>
    public partial class membershipanalysis : Page
    {
        public membershipanalysis()
        {
            InitializeComponent();
            getKundenforChart();
        }

        public  async void getKundenforChart()
        {

            var plt = WpfPlot1.Plot;

            List<Kunde> kunde = new List<Kunde>();

            DatabaseService db = new DatabaseService();
            kunde = await db.GetKunden();

            int[,] memberniderlassung = new int[4,4];

            
            foreach (var kunden in kunde)
            {
                
                if (kunden.Mitgliedschaft == Mitgliedschaft.BRONZE)
                {
                    memberniderlassung[0, kunden.NiederlassungID-1]++;
                }
                else if (kunden.Mitgliedschaft== Mitgliedschaft.SILBER)
                {
                    memberniderlassung[1, kunden.NiederlassungID-1]++;
                }
                else if (kunden.Mitgliedschaft == Mitgliedschaft.GOLD)
                {
                    memberniderlassung[2, kunden.NiederlassungID - 1]++;
                }
                else if (kunden.Mitgliedschaft == Mitgliedschaft.PLATINUM)
                {
                    memberniderlassung[3, kunden.NiederlassungID-1]++;
                }
            }


            string[] labels = { "Bronze", "Silber", "Gold", "Platin" };
            double[] values = Array2todoublearray(memberniderlassung , 0);

            var pie = plt.AddPie(values);
            // Farben für die einzelnen Teile festlegen
            pie.SliceFillColors = new System.Drawing.Color[]
            {
                System.Drawing.Color.Brown,
                System.Drawing.Color.Silver,
                System.Drawing.Color.Gold,
                System.Drawing.Color.LightCoral
            };

            plt.Style(figureBackground: System.Drawing.Color.Black, dataBackground: System.Drawing.Color.Black);

            pie.DonutSize = .4;
            pie.SliceLabels = labels;
            pie.ShowPercentages = true;
            pie.ShowLabels = true;

            // Titel hinzufügen

            plt.Title("Gym Membership Categories", true, System.Drawing.Color.White);
            // Diagramm rendern
            WpfPlot1.Refresh();

        }

        public double[] Array2todoublearray(int[,] array2d, int reihe)
        {
            double[] array = new double[array2d.GetLength(0)];
            for( int i = 0; i < array2d.GetLength(0);i++)
            {
                array[i] = array2d[i,reihe];
            }
            return array;
        }
    }
}
