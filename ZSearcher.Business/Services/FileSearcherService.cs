using System;
using static System.Console;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using ZSearcher.Business.Interfaces;

namespace ZSearcher.Business.Services
{
    public class FileSearcherService : IFileSearcherService {
        public List<string> FindInFiles(string file_path, string search_string, string file_extension) {
            string[] filePaths = Directory.GetFiles(file_path, file_extension, SearchOption.AllDirectories);
            List<string> results = new();

            Parallel.ForEach(filePaths, fp => ReadFile(fp, search_string, results));

            return results;
        }

        public void ReadFile(string file_path, string search_string, List<string> results) {
            string[] fileData = File.ReadAllLines(file_path);

            Parallel.ForEach(fileData, line => {
                if (line.Contains(search_string)) {
                    results.Add($"{file_path}, {line}");
                }
            });
        }
    }
}
