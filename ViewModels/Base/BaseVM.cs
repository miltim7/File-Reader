using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FileReader.ViewModels.Base;
public class BaseVM : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged<T>(out T field, T value, [CallerMemberName] string propName = "")
    {
        field = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}