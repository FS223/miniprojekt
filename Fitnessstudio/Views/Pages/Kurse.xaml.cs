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
        }

        private void KursHinufuegen_Click(object sender, RoutedEventArgs e) => NeuerKursFrame.Source = new Uri("../pages/Kursdetails.xaml", UriKind.Relative);
    }
}
