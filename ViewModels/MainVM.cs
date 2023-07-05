using FileReader.Tools;
using FileReader.ViewModels.Base;
using System.Windows.Data;

namespace FileReader.ViewModels;

public class MainVM : BaseVM
{
    private string symbolCount;
    public string SymbolCount
    {
        get => symbolCount;
        set => OnPropertyChanged(out symbolCount, value);
    }

    private string words;
    public string Words
    {
        get => words;
        set => OnPropertyChanged(out words, value);
    }

    private string sentences;
    public string Sentences
    {
        get => sentences;
        set => OnPropertyChanged(out sentences, value);
    }


    private RelayCommand analize;
    public RelayCommand Analize
    {
        get => analize ??= new RelayCommand(DoAnalize);
        set => OnPropertyChanged(out analize, value);
    }
    private void DoAnalize()
    {

    }



    private RelayCommand cancel;
    public RelayCommand Cancel
    {
        get => cancel ??= new RelayCommand(DoCancel);
        set => OnPropertyChanged(out cancel, value);
    }
    private void DoCancel()
    {

    }
}