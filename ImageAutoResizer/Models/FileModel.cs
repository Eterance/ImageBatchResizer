using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageBatchResizer.Models
{
    public class FileModel
    {
        public string Name { get; private set; }
        public string Path { get; private set; }
        public string ResizedPath { get; private set; }
        public FileModel(string name, string path)
        {
            Name = name;
            Path = path;
            ResizedPath = "";
        }
    }
}
