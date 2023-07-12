using ImageBatchResizer.ViewModels;
using System;
using System.Collections.Generic;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Collections.ObjectModel;
using ImageBatchResizer.Models;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Tga;
using SixLabors.ImageSharp.Formats.Tiff;
using Prism.Commands;
using System.Windows.Input;
using Prism.Services.Dialogs;
using Microsoft.Win32;
using Ookii.Dialogs.Wpf;
using System.Linq;
using System.Windows;
using System.IO;
using System.Collections.Specialized;

namespace ImageBatchResizer.ViewModels
{
    public class CoreViewModel : ViewModelBase
    {
        private bool _isEnableParametersPanel = true;
        private FormatModel _selectedFormatModel;
        private ResamplerModel _selectedResamplerModel;
        private ResizeModeModel _selectedResizeModeModel;
        private bool _isEnableInputResUpperLimit = false;
        private bool _isEnableInputResLowerLimit = true;
        private bool _isInputResLimitationAdaptPortraitImage = false;
        private bool _isAcceptBmp = true;
        private bool _isAcceptJpeg = true;
        private bool _isAcceptTga = true;
        private bool _isAcceptPng = true;
        private bool _isAcceptTiff = true;
        private bool _isAcceptWebP = true;
        private bool _isFileSizeFirstMode = true;
        private double _targetSizeLowerLimit = 300;
        private double _targetSizeUpperLimit = 350;
        private int _binarySearchTimesLimit = 20;
        private bool _isDeleteSmallerThanTarget = false;
        private int _targetResolutionWidthLowerLimit = 1920;
        private int _targetResolutionHeightLowerLimit = 1200;
        private bool _isEnableResLowerLimit = false;
        private bool _isOutputResLimitationAdaptPortraitImage = false;
        private bool _fileSizeFirst_DoNothing = true;
        private bool _fileSizeFirst_Delete = false;
        private bool _fileSizeFirst_IgnoreSizeLimit = false;
        private bool _isEnableTargetSizeLimit = false;
        private bool _resFirst_DoNothing = true;
        private bool _resFirst_Delete = false;
        private bool _resFirst_IgnoreSizeLimit = false;
        private int _quality = 90;
        private string _outputPath = "";
        private string _outputFileNamePattern = "{name}";
        private int _InputResUpperLimitWidth = 10000;
        private int _InputResUpperLimitHeight = 8000;
        private int _InputResLowerLimitWidth = 1920;
        private int _InputResLowerLimitHeight = 1200;
        private bool _isEnableOriginFileName = true;
        private bool _isEnableIndex = false;
        private bool _isEnableTime;
        private bool _isEnableQuality;
        private int _ProcessedCount = 0;
        private string _ProcessedInstruction = "0/0";
        private double _ProcessedPercent = 0;

        public bool IsEnableParametersPanel
        {
            get => _isEnableParametersPanel;
            set
            {
                SetValue(ref _isEnableParametersPanel, value);
            }
        }

        #region 输入参数
        public bool IsEnableInputResUpperLimit
        {
            get => _isEnableInputResUpperLimit;
            set
            {
                SetValue(ref _isEnableInputResUpperLimit, value);
            }
        }
        public int InputResUpperLimitWidth
        {
            get => _InputResUpperLimitWidth;
            set
            {
                SetValue(ref _InputResUpperLimitWidth, value);
            }
        }
        public int InputResUpperLimitHeight
        {
            get => _InputResUpperLimitHeight;
            set
            {
                SetValue(ref _InputResUpperLimitHeight, value);
            }
        }

        public bool IsEnableInputResLowerLimit
        {
            get => _isEnableInputResLowerLimit;
            set
            {
                SetValue(ref _isEnableInputResLowerLimit, value);
            }
        }
        public int InputResLowerLimitWidth
        {
            get => _InputResLowerLimitWidth;
            set
            {
                SetValue(ref _InputResLowerLimitWidth, value);
            }
        }
        public int InputResLowerLimitHeight
        {
            get => _InputResLowerLimitHeight;
            set
            {
                SetValue(ref _InputResLowerLimitHeight, value);
            }
        }

        public bool IsInputResLimitationAdaptPortraitImage
        {
            get => _isInputResLimitationAdaptPortraitImage;
            set
            {
                SetValue(ref _isInputResLimitationAdaptPortraitImage, value);
            }
        }

        public bool IsAcceptBmp
        {
            get => _isAcceptBmp;
            set
            {
                SetValue(ref _isAcceptBmp, value);
            }
        }
        public bool IsAcceptJpeg
        {
            get => _isAcceptJpeg;
            set
            {
                SetValue(ref _isAcceptJpeg, value);
            }
        }
        public bool IsAcceptPng
        {
            get => _isAcceptPng;
            set
            {
                SetValue(ref _isAcceptPng, value);
            }
        }
        public bool IsAcceptTga
        {
            get => _isAcceptTga;
            set
            {
                SetValue(ref _isAcceptTga, value);
            }
        }
        public bool IsAcceptTiff
        {
            get => _isAcceptTiff;
            set
            {
                SetValue(ref _isAcceptTiff, value);
            }
        }
        public bool IsAcceptWebP
        {
            get => _isAcceptWebP;
            set
            {
                SetValue(ref _isAcceptWebP, value);
            }
        }
        #endregion

        public bool IsFileSizeFirstMode
        {
            get => _isFileSizeFirstMode;
            set
            {
                SetValue(ref _isFileSizeFirstMode, value);
            }
        }

        #region 输出参数
        public double TargetSizeLowerLimit
        {
            get => _targetSizeLowerLimit;
            set
            {
                SetValue(ref _targetSizeLowerLimit, value);
            }
        }
        public double TargetSizeUpperLimit
        {
            get => _targetSizeUpperLimit;
            set
            {
                SetValue(ref _targetSizeUpperLimit, value);
            }
        }
        public int BinarySearchTimesLimit
        {
            get => _binarySearchTimesLimit;
            set
            {
                SetValue(ref _binarySearchTimesLimit, value);
            }
        }
        public bool IsDeleteSmallerThanTarget
        {
            get => _isDeleteSmallerThanTarget;
            set
            {
                SetValue(ref _isDeleteSmallerThanTarget, value);
            }
        }
        public bool IsEnableResLowerLimit
        {
            get => _isEnableResLowerLimit;
            set
            {
                SetValue(ref _isEnableResLowerLimit, value);
            }
        }

        public int TargetResolutionWidthLowerLimit
        {
            get => _targetResolutionWidthLowerLimit;
            set
            {
                SetValue(ref _targetResolutionWidthLowerLimit, value);
            }
        }
        public int TargetResolutionHeightLowerLimit
        {
            get => _targetResolutionHeightLowerLimit;
            set
            {
                SetValue(ref _targetResolutionHeightLowerLimit, value);
            }
        }
        public bool IsOutputResLimitationAdaptPortraitImage
        {
            get => _isOutputResLimitationAdaptPortraitImage;
            set
            {
                SetValue(ref _isOutputResLimitationAdaptPortraitImage, value);
            }
        }
        public bool FileSizeFirst_DoNothing
        {
            get => _fileSizeFirst_DoNothing;
            set
            {
                SetValue(ref _fileSizeFirst_DoNothing, value);
            }
        }
        public bool FileSizeFirst_Delete
        {
            get => _fileSizeFirst_Delete;
            set
            {
                SetValue(ref _fileSizeFirst_Delete, value);
            }
        }
        public bool FileSizeFirst_IgnoreSizeLimit
        {
            get => _fileSizeFirst_IgnoreSizeLimit;
            set
            {
                SetValue(ref _fileSizeFirst_IgnoreSizeLimit, value);
            }
        }
        public bool IsEnableTargetSizeLimit
        {
            get => _isEnableTargetSizeLimit;
            set
            {
                SetValue(ref _isEnableTargetSizeLimit, value);
            }
        }
        public bool ResFirst_DoNothing
        {
            get => _resFirst_DoNothing;
            set
            {
                SetValue(ref _resFirst_DoNothing, value);
            }
        }
        public bool ResFirst_Delete
        {
            get => _resFirst_Delete;
            set
            {
                SetValue(ref _resFirst_Delete, value);
            }
        }
        public bool ResFirst_IgnoreSizeLimit
        {
            get => _resFirst_IgnoreSizeLimit;
            set
            {
                SetValue(ref _resFirst_IgnoreSizeLimit, value);
            }
        }
        #endregion

        #region 通用输出参数
        public int Quality
        {
            get => _quality;
            set
            {
                SetValue(ref _quality, value);
            }
        }
        public ObservableCollection<FormatModel> FormatList { get; set; }

        public FormatModel SelectedFormatModel
        {
            get => _selectedFormatModel;
            set
            {
                SetValue(ref _selectedFormatModel, value);
            }
        }

        private void InitFormatList()
        {
            FormatList = new ObservableCollection<FormatModel>
            {
                new("Bmp", new BmpEncoder()),
                new("Jpeg", new JpegEncoder()),
                new("Png", new PngEncoder()),
                new("Tga", new TgaEncoder()),
                new("Tiff", new TiffEncoder())
            };
            SelectedFormatModel = new("Webp", new WebpEncoder());
            FormatList.Add(SelectedFormatModel);
        }

        public ObservableCollection<ResamplerModel> ResamplerList { get; set; }
        public ResamplerModel SelectedResamplerModel
        {
            get => _selectedResamplerModel;
            set
            {
                SetValue(ref _selectedResamplerModel, value);
            }
        }

        private void InitResamplerList()
        {
            ResamplerList = new ObservableCollection<ResamplerModel>
            {
                new("Bicubic", KnownResamplers.Bicubic),
                new("Box", KnownResamplers.Box),
                new("CatmullRom", KnownResamplers.CatmullRom),
                new("Hermite", KnownResamplers.Hermite),
                new("Lanczos2", KnownResamplers.Lanczos2),
            };
            SelectedResamplerModel = new("Lanczos3", KnownResamplers.Lanczos3);
            ResamplerList.Add(SelectedResamplerModel);
            ResamplerList.Add(new("Lanczos5", KnownResamplers.Lanczos5));
            ResamplerList.Add(new("Lanczos8", KnownResamplers.Lanczos8));
            ResamplerList.Add(new("MitchellNetravali", KnownResamplers.MitchellNetravali));
            ResamplerList.Add(new("NearestNeighbor", KnownResamplers.NearestNeighbor));
            ResamplerList.Add(new("Robidoux", KnownResamplers.Robidoux));
            ResamplerList.Add(new("RobidouxSharp", KnownResamplers.RobidouxSharp));
            ResamplerList.Add(new("Spline", KnownResamplers.Spline));
            ResamplerList.Add(new("Triangle", KnownResamplers.Triangle));
            ResamplerList.Add(new("Welch", KnownResamplers.Welch));
        }

        public ObservableCollection<ResizeModeModel> ResizeModeList { get; set; }
        public ResizeModeModel SelectedResizeModeModel
        {
            get => _selectedResizeModeModel;
            set
            {
                SetValue(ref _selectedResizeModeModel, value);
            }
        }

        private void InitResizeModeList()
        {
            ResizeModeList = new ObservableCollection<ResizeModeModel>
            {
                new("Crop", SixLabors.ImageSharp.Processing.ResizeMode.Crop),
                new("Max", SixLabors.ImageSharp.Processing.ResizeMode.Max),
            };
            SelectedResizeModeModel = new("Min", SixLabors.ImageSharp.Processing.ResizeMode.Min);
            ResizeModeList.Add(SelectedResizeModeModel);
            ResizeModeList.Add(new("Pad", SixLabors.ImageSharp.Processing.ResizeMode.Pad));
            ResizeModeList.Add(new("Stretch", SixLabors.ImageSharp.Processing.ResizeMode.Stretch));
        }

        public string OutputPath
        {
            get => _outputPath;
            set
            {
                SetValue(ref _outputPath, value);
            }
        }
        public ICommand SelectOutputFolderCommand { get; private set; }
        public string OutputFileNamePattern
        {
            get => _outputFileNamePattern;
            set
            {
                SetValue(ref _outputFileNamePattern, value);
            }
        }
        private string GetDash()
        {
            if (OutputFileNamePattern != "")
            {
                return "-";
            }
            return "";
        }
        private void ProcessOutputFileNamePattern(bool value, string PlaceHolder)
        {
            if (value)
            {
                OutputFileNamePattern = @$"{OutputFileNamePattern}{GetDash()}{PlaceHolder}";
            }
            else
            {
                OutputFileNamePattern = OutputFileNamePattern.Replace(@$"-{PlaceHolder}", "");
                OutputFileNamePattern = OutputFileNamePattern.Replace(PlaceHolder, "");
                OutputFileNamePattern = OutputFileNamePattern.Trim('-');
                OutputFileNamePattern = OutputFileNamePattern.Trim();
            }
        }
        public bool IsEnableOriginFileName
        {
            get => _isEnableOriginFileName;
            set
            {
                ProcessOutputFileNamePattern(value, "{name}");
                SetValue(ref _isEnableOriginFileName, value);
            }
        }
        public bool IsEnableIndex
        {
            get => _isEnableIndex;
            set
            {
                ProcessOutputFileNamePattern(value, "{index}");
                SetValue(ref _isEnableIndex, value);
            }
        }
        public bool IsEnableTime
        {
            get => _isEnableTime;
            set
            {
                ProcessOutputFileNamePattern(value, "{time}");
                SetValue(ref _isEnableTime, value);
            }
        }
        public bool IsEnableQuality
        {
            get => _isEnableQuality;
            set
            {
                ProcessOutputFileNamePattern(value, "{quality}");
                SetValue(ref _isEnableQuality, value);
            }
        }

        #endregion

        public int ProcessedCount
        {
            get => _ProcessedCount;
            set
            {
                SetValue(ref _ProcessedCount, value);
            }
        }
        public string ProcessedInstruction
        {
            get => _ProcessedInstruction;
            set
            {
                SetValue(ref _ProcessedInstruction, value);
            }
        }
        public double ProcessedPercent
        {
            get => _ProcessedPercent;
            set
            {
                SetValue(ref _ProcessedPercent, value);
            }
        }

        public ObservableCollection<FileModel> InputFileList { get; set; } = new();
        private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            ProcessedInstruction = @$"{ProcessedCount}/{InputFileList.Count}";
            try
            {
                ProcessedPercent = ProcessedCount / InputFileList.Count;
            }
            catch (DivideByZeroException dbze)
            {
                ProcessedPercent = 0;
            }
        }

        public CoreViewModel()
        {
            InitFormatList();
            InitResamplerList();
            InitResizeModeList();
            SelectOutputFolderCommand = new DelegateCommand(() => 
            {
                var dlg = new VistaFolderBrowserDialog();
                var result = dlg.ShowDialog();
                if (result == true) OutputPath = dlg.SelectedPath;
            });
            InputFileList.CollectionChanged += OnCollectionChanged;
        }

        // https://stackoverflow.com/questions/40104765/bind-event-in-mvvm-and-pass-event-arguments-as-command-parameter
        public void DataGrid_Drop(object sender, DragEventArgs e)
        {
            // https://stackoverflow.com/questions/5662509/drag-and-drop-files-into-wpf
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] filesOrFolders = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string file in filesOrFolders) 
                {
                    HandleDropFiles(file); 
                }
            }
        }

        private void HandleDropFiles(string path)
        {
            if (File.Exists(path) && !IsInputFileListAlreadyContains(path))
            {
                InputFileList.Add(new(Path.GetFileName(path), path));
            }
        }

        private bool IsInputFileListAlreadyContains(string path)
        {
            foreach (var file in InputFileList)
            {
                if (file.Path == path)
                {
                    return true;
                }
            }
            return false;
        }

        private void Resize()
        {
            // Open the file automatically detecting the file type to decode it.
            // Our image is now in an uncompressed, file format agnostic, structure in-memory as
            // a series of pixels.
            // You can also specify the pixel format using a type parameter (e.g. Image<Rgba32> image = Image.Load<Rgba32>("foo.jpg"))
            using (Image image = Image.Load("foo.jpg"))
            {
                // Resize the image in place and return it for chaining.
                // 'x' signifies the current image processing context.
                image.Mutate(x => x.Resize(image.Width / 2, image.Height / 2));

                // The library automatically picks an encoder based on the file extension then
                // encodes and write the data to disk.
                // You can optionally set the encoder to choose.
                image.Save("bar.jpg");
            } // Dispose - releasing memory into a memory pool ready for the next image you wish to process.
        }
    }
}
