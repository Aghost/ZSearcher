using System;
using static System.Console;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using ZSearcher.Business.Interfaces;
using ZSearcher.Business.Types;

namespace ZSearcher.Business.Services
{
    public class FileSystemService : IFileSystemService {
        private List<DataStruct> FileRepository = new List<DataStruct>();
        private string[] FileIndex;

        public void CreateIndex(string file_path, string file_extension) {
            FileIndex = Directory.GetFiles(file_path, file_extension, SearchOption.AllDirectories);
        }

        public void CreateDataStructRepository(ref string[] files) {
            int i = 0;
            foreach (string fpath in files) {
                FileRepository.Add(new DataStruct() {
                        path = fpath.ToCharArray(),
                        data = "ik ben de data van {i}".ToCharArray()
                });

                //File.ReadAllLines(path);
                i++;
            }
            WriteLine($"added {i} items to the File Repository");
        }

        public ref string[] GetIndex() {
            return ref FileIndex;
        }

        public List<DataStruct> GetRepository() {
            return FileRepository;
        }

        public string GetInfo() {
            int totalfiles = FileRepository.Count;

            return $"FileRepository Count: {totalfiles}";
        }
    }
}
