using System;
using System.Linq;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ZSearcher.Business.Interfaces;
using ZSearcher.Business.Services;
using System.Collections.Generic;
using System.Diagnostics;
using static System.Console;

namespace ZSearcher.Core.Entities
{
    public class Shell
    {
        private string Prompt { get; set; }
        private string PromptBase { get; set; }
        private string CurrentDirectory { get; set; }
        private List<string> CurrentResults { get; set; }
        private readonly string RootDirectory;

        private readonly IFileSearcherService FSService;
        private readonly IFileSystemService FileSystem;

        public Shell(string CurrentDirectory, IFileSearcherService FileSearcherService, IFileSystemService FileSystem, string PromptBase = "Base_Shell: ") {
            this.CurrentDirectory = CurrentDirectory;
            this.RootDirectory = CurrentDirectory;
            this.PromptBase = PromptBase;
            this.FSService = FileSearcherService;
            this.FileSystem = FileSystem;
            SetPromptBase(PromptBase);
        }

        public void Run() {
            string buffer = "";
            while (buffer != "exit") {
                buffer = ParseCommand();
                WriteLine(buffer);
            }
        }

        private string ParseCommand() {
            Write(Prompt);
            string[] cmds = ReadLine().Split(' ');

            switch(cmds.Length) {
                case 1:
                    switch(cmds[0]) {
                        case "help":    return $"help|info|fsinfo|mkindex|makefs|getfs|prompt|search|print|exit|";
                        case "help1":    return $"cd|";
                        case "info":    return $"RootDirectory: {RootDirectory}\nCurrentDirectory: {CurrentDirectory}\n";
                        case "fsinfo":  return FileSystem.GetInfo();
                        case "mkindex":
                                        FileSystem.CreateIndex(CurrentDirectory, "*.*");
                                        return $"FileIndex made for {CurrentDirectory}...";
                        case "makefs":
                                        FileSystem.CreateDataStructRepository(ref FileSystem.GetIndex());
                                        return "done...";
                        case "getfs":
                                        return "todo: foreach struct, print shit...";
                        case "prompt":
                                        Write("Set new Prompt: ");
                                        SetPromptBase(ReadLine());
                                        return "Prompt Set!";
                        case "search":
                                        var timer = new Stopwatch();
                                        string userInput = ReadLine();
                                        timer.Start();
                                        Search(userInput, "*.*");
                                        timer.Stop();
                                        return $"Search completed in {timer.ElapsedTicks}";
                        case "print":        
                                        PrintResults();
                                        return "done";
                        case "exit":    return "exit";
                        default:        return "DEFAULT";
                    }
                default:                return "unsupported nr of arguments";
            }
        }

        private void Search(string input, string extension) {
            CurrentResults = FSService.FindInFiles(CurrentDirectory, input, extension);
        }

        private void PrintResults() {
            if (CurrentResults == null) {
                WriteLine("no results...");
                return;
            } else {
                foreach (string str in CurrentResults) {
                    WriteLine(str);
                }
            }
        }

        private void SetPromptBase(string input) {
            PromptBase = input;
            Prompt = $"{PromptBase}[{CurrentDirectory}]$ ";
        }
    }
}
