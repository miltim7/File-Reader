using FileReader.Models;
using FileReader.Tools;
using FileReader.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Printing;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace FileReader.ViewModels;

public class MainVM : BaseVM
{
    CancellationTokenSource tokenSource;
    bool makeReset = true;
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

    private string contentName;
    public string ContentName
    {
        get => contentName;
        set => OnPropertyChanged(out contentName, value);
    }

    private string content;
    public string Content

    {
        get => content;
        set => OnPropertyChanged(out content, value);
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
        if(makeReset)
        {
            ProgressBar = 0;
            Sentences = 0;
            Words = 0;
            SymbolCount = 0;
            Content = string.Empty;
            ContentName = string.Empty;
        }

        makeReset = true;
        tokenSource = new CancellationTokenSource();
        double progress = GetProgress();
        StreamReader reader = new StreamReader(SelectedItem.File);
        ContentName = SelectedItem.File;
        var task = new Task(() =>
        {
            while(!reader.EndOfStream)
            {
                if(tokenSource.IsCancellationRequested)
                    break;

                int nextChar = reader.Read();

                if(nextChar == -1)
                    break;

                char c = (char) nextChar;

                Console.Write(c);

                Content += c;

                Thread.Sleep(10);

                ProgressBar += progress;
            }
        });

        task.Start();
    }

    private double GetProgress() => 100 / (double)File.ReadAllText(SelectedItem.File).Length;

    private void SetStats(string content)
    {
        SymbolCount = content.Length;
        Words = content.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Length;
        
        Regex regex = new Regex(@"\b[^.!?]*[.!?](?=\s|$)");
        MatchCollection matches = regex.Matches(content);

        Sentences = matches.Count;
    }

    private RelayCommand cancel;
    public RelayCommand Cancel
    {
        get => cancel ??= new RelayCommand(DoCancel);
        set => OnPropertyChanged(out cancel, value);
    }
    private void DoCancel()
    {
        SetStats(Content);
        tokenSource.Cancel();
    }

    private RelayCommand continueCommand;
    public RelayCommand Continue
    {
        get => continueCommand ??= new RelayCommand(DoContinue);
        set => OnPropertyChanged(out continueCommand, value);
    }
    private void DoContinue()
    {
        SetStats(Content);
        makeReset = false;
        DoAnalize();
    }
}