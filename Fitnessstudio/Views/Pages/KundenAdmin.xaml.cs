using Fitnessstudio.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Fitnessstudio.Views.Pages
{
    public partial class KundenAdmin : Page
    {
        private KundenAdminViewModel viewModel;

        public KundenAdmin()
        {
            InitializeComponent();
        }

        public void SetDataContext(object dataContext)
        {
            if (dataContext is KundenAdminViewModel kundenAdminViewModel)
            {
                viewModel = kundenAdminViewModel;
                DataContext = viewModel;
                UpdateDataGrid();
            }
        }

        private void UpdateDataGrid()
        {
            membersDataGrid.ItemsSource = viewModel.Items;
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            viewModel.NextPage();
            UpdateDataGrid();
        }

        private void PreviousPage_Click(object sender, RoutedEventArgs e)
        {
            viewModel.PreviousPage();
            UpdateDataGrid();
        }

        private void membersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
