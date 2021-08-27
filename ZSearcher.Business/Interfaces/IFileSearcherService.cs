using System;
using System.Collections.Generic;

namespace ZSearcher.Business.Interfaces
{
    public interface IFileSearcherService {
        /* private string FilePath { get; set; }
        private string[] Keywords { get; set; }
        private int filesRead { get; set; }
        private int totalFilesRead { get; set; } */

        public List<string> FindInFiles(string file_path, string search_string, string file_extension);
        public void ReadFile(string file_path, string search_string, List<string> results);
    }
}
