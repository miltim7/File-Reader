using System;
using System.Windows.Input;

namespace FileReader.Tools;
public class RelayCommand : ICommand
{
    private readonly Action action;
    private readonly Func<bool> predicate;
    public RelayCommand(Action action, Func<bool> predicate = null)
    {
        this.action = action;
        this.predicate = predicate;
    }

    public bool CanExecute(object? parameter) => predicate == null || predicate();

    public void Execute(object? parameter) => action.Invoke();

    public event EventHandler? CanExecuteChanged;

    public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}