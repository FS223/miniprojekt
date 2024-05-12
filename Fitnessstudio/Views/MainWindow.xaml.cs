using Fitnessstudio.Views;
using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
        }

        private void ButtonHome_Click(object sender, RoutedEventArgs e) => FrameWithinGrid.Source = new Uri("Pages/Home.xaml", UriKind.Relative);     
        private void ButtonStudio_Click(object sender, RoutedEventArgs e) => FrameWithinGrid.Source = new Uri("Pages/Studiosicht.xaml", UriKind.Relative);
        private void ButtonKunden_Click(object sender, RoutedEventArgs e) => FrameWithinGrid.Source = new Uri("Pages/Kunden.xaml", UriKind.Relative);
        private void ButtonKurse_Click(object sender, RoutedEventArgs e) => FrameWithinGrid.Source = new Uri("Pages/Kurse.xaml", UriKind.Relative);
    }
}