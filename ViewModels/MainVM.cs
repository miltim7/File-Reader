using FileReader.Models;
using FileReader.Tools;
using FileReader.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Documents;
using System.Windows.Input;

namespace FileReader.ViewModels;

public class MainVM : BaseVM
{
    public MainVM()
    {
        AddFiles();
    }
    private void AddFiles()
    {
        Files = new ObservableCollection<FileModel>(new List<FileModel>() {
            new FileModel() { File = "chess.txt" },
            new FileModel() { File = "c#.txt" },
            new FileModel() { File = "c++.txt" },
            new FileModel() { File = "github.txt" },
            new FileModel() { File = "javascript.txt" },
            new FileModel() { File = "idea.txt" },
            new FileModel() { File = "sql.txt" },
            new FileModel() { File = "python.txt" }});

    }
    private ObservableCollection<FileModel> files;
    public ObservableCollection<FileModel> Files
    {
        get => files;
        set => OnPropertyChanged(out files, value);
    }

    private FileModel selectedItem;
    public FileModel SelectedItem
    {
        get => selectedItem;
        set => OnPropertyChanged(out selectedItem, value);
    }


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


    private ICommand analize;
    public ICommand Analize
    {
        get
        {
            analize ??= new RelayCommand(DoAnalize);
            return analize;
        }
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