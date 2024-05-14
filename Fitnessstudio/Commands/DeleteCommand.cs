using Fitnessstudio;
using Fitnessstudio.Models;
using System;
using System.Windows.Input;

public class DeletePersonCommand : ICommand
{
    private readonly Action<Person> _execute;

    public DeletePersonCommand(Action<Person> execute)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
    }

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
        return true; // muss noch bearbeitet werden xd 
    }

    public void Execute(object parameter)
    {
        if (parameter is Person person)
        {
            _execute(person);
        }
    }
}
