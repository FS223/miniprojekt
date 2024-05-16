using Fitnessstudio;
using Fitnessstudio.Models;
using Fitnessstudio.ViewModels;
using ScottPlot.Renderable;
using Serilog;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

public class DeletePersonCommand : ICommand
{
    private readonly KundenAdminViewModel ViewModel;
    public DeletePersonCommand(KundenAdminViewModel viewModel)
    {
        this.ViewModel = viewModel;
    }

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
        return true; // muss noch bearbeitet werden xd 
    }

    public void Execute(object parameter)
    {
        if (parameter is Button button)
        {
            Debug.Write("Execute Delete");
            if (button.DataContext is PersonWithAddress person)
            {
                try
                {
                    DatabaseService db = new DatabaseService();
                    db.DeletePersonAsync(person.Person);
                    ViewModel.dashboard.NavigateToPage("Pages/KundenAdmin.xaml", ViewModel.CurrentAccount);
                    MessageBox.Show("Benutzer gelöscht!");
                }
                catch (Exception ex)
                {                {
                    Log.Error(ex, "Error while deleting persons");
                    MessageBox.Show(ex.Message);
                }
            }
            }
        }
    }
}
