using System;

namespace FileReader.Tools;
public class RelayCommand
{
    private readonly Action action;

    public event EventHandler? CanExecuteChanged;
    public RelayCommand(Action action)
    {
        this.action = action;
    }

    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter) => action.Invoke();
}