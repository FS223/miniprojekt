using Fitnessstudio.Views.Pages;
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
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
            course.MouseLeftButtonDown += navigateToCourse;
            account.MouseLeftButtonDown += navigateToAccount;

        }

        public void SetDataContext(object dataContext)
        {
            this.DataContext = dataContext;
        }

        private void navigateToCourse(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Kurse());
        }
        private void navigateToAccount(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new KundenVerwaltung());
        }


    }
}
