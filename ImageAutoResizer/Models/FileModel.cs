using ImageBatchResizer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageBatchResizer.Models
{
    public class FileModel : ViewModelBase, IModel
    {
        private string _Name = "";
        private bool _Processed = false;
        private string _ResizedPath = "";
        private string _Path = "";
        private bool _Chosen;

        public bool Chosen
        {
            get => _Chosen;
            set
            {
                SetValue(ref _Chosen, value);
            }
        }
        public string DisplayName
        {
            get => _Name;
            private set
            {
                SetValue(ref _Name, value);
            }
        }
        public string Path
        {
            get => _Path;
            private set
            {
                SetValue(ref _Path, value);
            }
        }
        public string ResizedPath
        {
            get => _ResizedPath;
            set
            {
                SetValue(ref _ResizedPath, value);
            }
        }
        public bool Processed
        {
            get => _Processed;
            set
            {
                SetValue(ref _Processed, value);
            }
        }
        public FileModel(string name, string path)
        {
            DisplayName = name;
            Path = path;
            ResizedPath = "";
        }
    }
}
