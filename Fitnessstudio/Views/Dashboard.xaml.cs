﻿using Fitnessstudio.ViewModels;
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
        }

        private void ButtonHome_Click(object sender, RoutedEventArgs e) => NavigateToPage("Pages/Home.xaml", CurrentAccount);
        private void ButtonStudio_Click(object sender, RoutedEventArgs e) => NavigateToPage("Pages/Studiosicht.xaml", CurrentAccount);
        private void ButtonKunden_Click(object sender, RoutedEventArgs e) => NavigateToPage("Pages/KundenVerwaltung.xaml", CurrentAccount);
        private void ButtonKurse_Click(object sender, RoutedEventArgs e) => NavigateToPage("Pages/Kurse.xaml", CurrentAccount);
        private void ButtonKundenAdmin_Click(object sender, RoutedEventArgs e) => NavigateToPage("Pages/KundenAdmin.xaml", CurrentAccount);

        private void NavigateToPage(string pageUri, object parameter)
        {
            var uri = new Uri(pageUri, UriKind.Relative);
            FrameWithinGrid.Source = uri;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            // Application.Current.Shutdown();
            // Verwende den Navigations-Event, um den DataContext zu setzen
            FrameWithinGrid.Navigated += (sender, e) =>
            {
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
                    // Andere Pages können erst mit gültigen ViewModel hinzugefügt werden.
                }
            };
        }
    }
}
