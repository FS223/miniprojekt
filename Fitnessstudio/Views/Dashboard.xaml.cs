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
using System.Windows.Shapes;

namespace Fitnessstudio.Views
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public Dashboard()
        {
            InitializeComponent();
        }
        private void ButtonHome_Click(object sender, RoutedEventArgs e) => FrameWithinGrid.Source = new Uri("Pages/Home.xaml", UriKind.Relative);
        private void ButtonStudio_Click(object sender, RoutedEventArgs e) => FrameWithinGrid.Source = new Uri("Pages/Studiosicht.xaml", UriKind.Relative);
        private void ButtonKunden_Click(object sender, RoutedEventArgs e) => FrameWithinGrid.Source = new Uri("Pages/KundenVerwaltung.xaml", UriKind.Relative);
        private void ButtonKurse_Click(object sender, RoutedEventArgs e) => FrameWithinGrid.Source = new Uri("Pages/Kurse.xaml", UriKind.Relative);
        private void ButtonKundenAdmin_Click(object sender, RoutedEventArgs e) => FrameWithinGrid.Source = new Uri("Pages/KundenAdmin.xaml", UriKind.Relative);
    }
}
