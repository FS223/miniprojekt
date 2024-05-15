using Fitnessstudio.ViewModels;
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
using System.Windows.Shapes;

namespace Fitnessstudio.Views
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        private Account CurrentAccount;
        public Dashboard(Account currentAccount)
        {
            InitializeComponent();
            CurrentAccount = currentAccount;
            NavigateToPage("Pages/Home.xaml", CurrentAccount);
        }

        private void ButtonHome_Click(object sender, RoutedEventArgs e) => NavigateToPage("Pages/Home.xaml", CurrentAccount);
        private void ButtonStudio_Click(object sender, RoutedEventArgs e) => NavigateToPage("Pages/Membershipanalysis.xaml", CurrentAccount);
        private void ButtonKunden_Click(object sender, RoutedEventArgs e) => NavigateToPage("Pages/KundenVerwaltung.xaml", CurrentAccount);
        private void ButtonKurse_Click(object sender, RoutedEventArgs e) => NavigateToPage("Pages/Kurse.xaml", CurrentAccount);
        private void ButtonAdmin_Click(object sender, RoutedEventArgs e) => NavigateToPage("Pages/KundenAdmin.xaml", CurrentAccount);

        private void NavigateToPage(string pageUri, object parameter)
        {
            var uri = new Uri(pageUri, UriKind.Relative);
            FrameWithinGrid.Source = uri;
            FrameWithinGrid.Navigated += (sender, e) => {
                if (e.Content is Page page)
                {
                    if (page is KundenAdmin kundenAdminPage)
                    {
                        kundenAdminPage.SetDataContext(new KundenAdminViewModel(CurrentAccount));
                    }
                    else if (page is KundenVerwaltung kundenVerwaltungPage)
                    {
                        kundenVerwaltungPage.SetDataContext(new KundenVerwaltungViewModel(CurrentAccount));
                    }
                    else if (page is Home homePage)
                    {
                        homePage.SetDataContext(new HomeViewModel(CurrentAccount));
                    }
                    // Andere Pages können erst mit gültigen ViewModel hinzugefügt werden.
                }
            };
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
