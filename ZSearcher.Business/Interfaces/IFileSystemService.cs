using System;
using ZSearcher.Business.Types;
using System.Collections.Generic;

namespace ZSearcher.Business.Interfaces
{
    public interface IFileSystemService {
        public void CreateIndex(string file_path, string file_extension);
        public void CreateDataStructRepository(ref string[] files);
        public ref string[] GetIndex();
        public List<DataStruct> GetRepository();
        public string GetInfo();
    }
}
