using System;
using ZSearcher.Core.Entities;
using ZSearcher.Business.Services;
using ZSearcher.Business.Interfaces;

namespace ZSearcher.App
{
    class Program
    {
        static void Main(string[] args) {
            const string root = "../../0_LIBRARY";

            //IFileSearcherService SearchService = new FileSearcherService();
            IFileSearcherService SearchService = new AdvancedSearcherService();
            IFileSystemService FileSystemService = new FileSystemService();

            var zShell = new Shell(root, SearchService, FileSystemService);
            zShell.Run();
        }
    }
}
