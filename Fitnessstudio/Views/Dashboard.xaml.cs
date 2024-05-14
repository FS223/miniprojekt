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

        private void ButtonHome_Click(object sender, RoutedEventArgs e)
        {
            FrameWithinGrid.Source = new Uri("Pages/Home.xaml", UriKind.Relative);
            Header.Content = "Home";
        }
        private void ButtonStudio_Click(object sender, RoutedEventArgs e)
        {
            FrameWithinGrid.Source = new Uri("Pages/Studiosicht.xaml", UriKind.Relative);
            Header.Content = "Studioübersicht";
        }
        private void ButtonKunden_Click(object sender, RoutedEventArgs e)
        {
            FrameWithinGrid.Source = new Uri("Pages/Kunden.xaml", UriKind.Relative);
            Header.Content = "Kundenverwaltung";
        }

        private void ButtonKurse_Click(object sender, RoutedEventArgs e)
        {
            FrameWithinGrid.Source = new Uri("Pages/Kurse.xaml", UriKind.Relative);
            Header.Content = "Kursverwaltung";
        }


        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
