using FileReader.Models;
using FileReader.Tools;
using FileReader.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Threading;

namespace FileReader.ViewModels;

public class MainVM : BaseVM
{
    CancellationTokenSource tokenSource;

    int symbol;
    int word;
    int sentence;

    public MainVM()
    {
        AddFiles();
    }
    private void AddFiles()
    {
        tokenSource = new CancellationTokenSource();

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


    private int symbolCount;
    public int SymbolCount
    {
        get => symbolCount;
        set => OnPropertyChanged(out symbolCount, value);
    }

    private int words;
    public int Words
    {
        get => words;
        set => OnPropertyChanged(out words, value);
    }

    private int sentences;
    public int Sentences
    {
        get => sentences;
        set => OnPropertyChanged(out sentences, value);
    }

    private double progressBar;
    public double ProgressBar
    {
        get => progressBar;
        set => OnPropertyChanged(out progressBar, value);
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
        var task = new Task(() =>
        {
            StreamReader reader = new StreamReader(SelectedItem.File);
            while(!reader.EndOfStream)
            {
                if(tokenSource.IsCancellationRequested)
                    break;

                int nextChar = reader.Read();

                if(nextChar == -1)
                    break;

                char c = (char) nextChar;

                if(c == ' ')
                    word++;

                if(c == '.')
                    sentence++;

                symbol++;

                Console.Write(c);

                Thread.Sleep(10);

                ProgressBar += 0.01;
            }
        });

        task.Start();
    }



    private RelayCommand cancel;
    public RelayCommand Cancel
    {
        get => cancel ??= new RelayCommand(DoCancel);
        set => OnPropertyChanged(out cancel, value);
    }
    private void DoCancel()
    {
        SymbolCount = symbol;
        Sentences = sentence;
        Words = word;

        tokenSource.Cancel();
    }
}