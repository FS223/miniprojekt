using Fitnessstudio.Models;
using Fitnessstudio.ViewModels;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using ScottPlot;

namespace Fitnessstudio.Commands
{
    public class EditCommand : CommandBase
    {
        private KundenAdminViewModel ViewModel;
        public EditCommand(KundenAdminViewModel viewModel)
        {
            this.ViewModel = viewModel;
        }
            

        public override void Execute(object? parameter)
        {
            Debug.WriteLine("Edit Command");
            if (parameter is Button button)
            {
                if (button.DataContext is PersonWithAddress person)
                {
                    try
                    {
                        OpenWindow(person);
                    }
                    catch (Exception ex)
                    {
                        {
                            Log.Error(ex, "Error");
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }

        private async void OpenWindow(PersonWithAddress person)
        {
            try
            {
                DatabaseService db = new DatabaseService();
                Account selectedUser = await db.GetAccountById(person.Person.Id);
                this.ViewModel.dashboard.NavigateToPage("Pages/KundenVerwaltung.xaml", selectedUser);

            }
            catch (Exception ex)
            {
                {
                    Log.Error(ex, "Error");
                    MessageBox.Show(ex.Message);
                }
            }
        }

        
    }
}
