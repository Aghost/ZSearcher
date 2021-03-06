using System;
using static System.Console;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using ZSearcher.Business.Interfaces;

namespace ZSearcher.Business.Services
{
    public class AdvancedSearcherService : IFileSearcherService {
        public List<string> FindInFiles(string file_path, string search_string, string file_extension) {
            string[] filePaths = Directory.GetFiles(file_path, file_extension, SearchOption.AllDirectories);

            List<string> results = new();

            Array.ForEach(filePaths, fp => ReadFile(fp, search_string, results));

            return results;
        }

        public void ReadFile(string file_path, string search_string, List<string> results) {
            string[] fileData = File.ReadAllLines(file_path);

            char[] tmp1 = file_path.ToCharArray();
            char[] tmp2 = search_string.ToCharArray();

            Parallel.ForEach(fileData, line => KMPSearch(tmp1, tmp2, results) );

            //KMPSearch("banaan".ToCharArray(), "aardappelbanaanaardappelbanaan".ToCharArray());
        }

        public void KMPSearch(char[] pattern, char[] line, List<string> results) {
            int patternLength = pattern.Length;
            int lineLength = line.Length;

            int[] longest_fix = new int[patternLength];
            int patternIndex = 0;

            ComputeLongest(pattern, patternLength, longest_fix);

            int lineIndex = 0;

            while (lineIndex < lineLength) {
                if (pattern[patternIndex] == line[lineIndex]) {
                    patternIndex++;
                    lineIndex++;
                }
                if (patternIndex == patternLength) {
                    results.Add($"found pattern at index {lineIndex - patternIndex}, ending at {(lineIndex - patternIndex) + patternLength}");
                    patternIndex = longest_fix[patternIndex - 1];
                }
                else if (lineIndex < lineLength && pattern[patternIndex] != line[lineIndex]) {
                    if (patternIndex != 0)
                        patternIndex = longest_fix[patternIndex - 1];
                    else
                        lineIndex++;
                }
            }

            //results.Add($"{new string(pattern)} {new string(line)} {patternLength} {lineLength}");
        }

        private void ComputeLongest(char[] pattern, int M, int[] longest_fix) {
            int len = 0;
            int i = 1;
            longest_fix[0] = 0;

            while (i < M) {
                if (pattern[i] == pattern[len]) {
                    longest_fix[i] = len++;
                    i++;
                } else {
                    if  (len != 0) {
                        len = longest_fix[len -1];
                    } else {
                        longest_fix[i] = len;
                        i++;
                    }
                }
            }
        }
    }
}
