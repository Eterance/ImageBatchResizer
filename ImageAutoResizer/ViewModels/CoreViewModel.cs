using ImageBatchResizer.Models;
using ImageBatchResizer.Views;
using Ookii.Dialogs.Wpf;
using Prism.Commands;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Compression.Zlib;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Tga;
using SixLabors.ImageSharp.Formats.Tiff;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Windows;
using System.Windows.Input;

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
        private List<string> _SupportedExtensions = new List<string>()
        {
            ".bmp",
            ".jpeg",
            ".jpg",
            ".png",
            ".tga",
            ".tiff",
            ".tif",
            ".webp"
        };
        private FileModel? _SelectedPath;
        private string _OuputPathErrorContent = "";
        private string _ConsoleContent;
        private int _SliderMinimum = 100;
        private int _SliderMaximum = 1;
        private string _CompressDescrption = "有损压缩质量";
        private bool _IsEnableCompressAdjust = true;
        private bool _IsWebpLossyMode = true;
        private bool _IsWebpEncoder = true;

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
        public bool IsEnableCompressAdjust
        {
            get => _IsEnableCompressAdjust;
            set
            {
                SetValue(ref _IsEnableCompressAdjust, value);
            }
        }
        public bool IsWebpEncoder
        {
            get => _IsWebpEncoder;
            set
            {
                SetValue(ref _IsWebpEncoder, value);
            }
        }
        public bool IsWebpLossyMode
        {
            get => _IsWebpLossyMode;
            set
            {
                SetValue(ref _IsWebpLossyMode, value);
                if (IsWebpLossyMode)
                {
                    LossyCompressUI();
                }
                else 
                {
                    LosslessCompressUI();
                }
            }
        }
        public int SliderMinimum
        {
            get => _SliderMinimum;
            set
            {
                SetValue(ref _SliderMinimum, value);
            }
        }
        public int SliderMaximum
        {
            get => _SliderMaximum;
            set
            {
                SetValue(ref _SliderMaximum, value);
            }
        }
        public string CompressDescrption
        {
            get => _CompressDescrption;
            set
            {
                SetValue(ref _CompressDescrption, value);
            }
        }
        public ObservableCollection<FormatModel> FormatList { get; set; }

        public FormatModel SelectedFormatModel
        {
            get => _selectedFormatModel;
            set
            {
                SetValue(ref _selectedFormatModel, value);
                if (value.Name == ".webp")
                {
                    IsWebpEncoder = true;
                }
                else 
                {
                    IsWebpEncoder = false;
                }
                // 根据编码器的不同，改变压缩的描述
                if (value.Name == ".jpg" || (value.Name == ".webp" && IsWebpLossyMode))
                {
                    LossyCompressUI();
                }
                else if(value.Name == ".png" || value.Name == ".tif" || (value.Name == ".webp" && !IsWebpLossyMode))
                {
                    LosslessCompressUI();
                }
                else
                {
                    IsEnableCompressAdjust = false;
                }
            }
        }

        private void LossyCompressUI()
        {
            IsEnableCompressAdjust = true;
            CompressDescrption = "有损压缩质量";
            SliderMaximum = 100;
            SliderMinimum = 0;
        }

        private void LosslessCompressUI()
        {
            IsEnableCompressAdjust = true;
            CompressDescrption = "无损压缩等级";
            SliderMaximum = 9;
            SliderMinimum = 0;
        }

        private void InitFormatList()
        {
            FormatList = new ObservableCollection<FormatModel>
            {
                new(".bmp", new BmpEncoder()),
                new(".jpg", new JpegEncoder()),
                new(".png", new PngEncoder()),
                new(".tga", new TgaEncoder()),
                new(".tif", new TiffEncoder())
            };
            SelectedFormatModel = new(".webp", new WebpEncoder());
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

        #region 错误Label
        public string OuputPathErrorContent
        {
            get => _OuputPathErrorContent;
            set
            {
                SetValue(ref _OuputPathErrorContent, value);
            }
        }

        #endregion

        public string ConsoleContent
        {
            get => _ConsoleContent;
            private set
            {
                SetValue(ref _ConsoleContent, value);
            }
        }

        private void Append2Console(string message)
        {
            ConsoleContent = $"{ConsoleContent}\n\n{DateTime.Now} {message}";
        }

        #region 处理中

        public int ProcessedCount
        {
            get => _ProcessedCount;
            set
            {
                SetValue(ref _ProcessedCount, value);
                UpdateProcessedPercent();
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

        #endregion

        public FileModel? SelectedPath
        {
            get => _SelectedPath;
            set
            {
                SetValue(ref _SelectedPath, value);
            }
        }

        public ObservableCollection<FileModel> InputFileList { get; set; } = new();
        public DelegateCommand AppendFilesCommand { get; private set; }
        public DelegateCommand OpenConsoleCommand { get; }
        public DelegateCommand DeleteChosenCommand { get; }
        public DelegateCommand DeleteAllCommand { get; }
        public DelegateCommand StartCommand { get; }
        public DelegateCommand ClearConsoleCommand { get; }
        public DelegateCommand ResetOuputPathErrorContentCommand { get; }

        private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateProcessedPercent();
        }

        private void UpdateProcessedPercent()
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
            Append2Console("正在初始化");
            InitFormatList();
            InitResamplerList();
            InitResizeModeList();
            SelectOutputFolderCommand = new DelegateCommand(() => 
            {
                var dlg = new VistaFolderBrowserDialog();
                var result = dlg.ShowDialog();
                if (result == true)
                {
                    OutputPath = dlg.SelectedPath;
                }
            });
            InputFileList.CollectionChanged += OnCollectionChanged;
            AppendFilesCommand = new DelegateCommand(() =>
            {
                var dialog = new VistaOpenFileDialog();
                dialog.Multiselect = true;
                dialog.CheckFileExists = true;
                var result = dialog.ShowDialog();
                if (result == true)
                {
                    HandleAppendFiles(dialog.FileNames);
                }
            });
            OpenConsoleCommand = new DelegateCommand(() =>
            {
                if (ConsoleWindow.SingleConsoleWindow == null)
                {
                    ConsoleWindow.SingleConsoleWindow = new(this);
                }
                ConsoleWindow.SingleConsoleWindow.Show();
            });
            DeleteChosenCommand = new DelegateCommand(() => 
            {
                if (SelectedPath != null) 
                {
                    InputFileList.Remove(SelectedPath);
                }
            });
            DeleteAllCommand = new DelegateCommand(() =>
            {
                InputFileList.Clear();
            });
            StartCommand = new DelegateCommand(Start);
            ClearConsoleCommand = new DelegateCommand(() => ConsoleContent = "");
            ResetOuputPathErrorContentCommand = new DelegateCommand(() => OuputPathErrorContent = "");

            Append2Console("初始化完毕");
        }

        private void Start()
        {
            // 锁定可编辑的参数
            IsEnableParametersPanel = false;
            if (CheckParameterLegitimacy() == false)
            {
                IsEnableParametersPanel = true;
            }
            else 
            {
                IsEnableParametersPanel = true;
            }

        }

        private bool CheckParameterLegitimacy()
        {
            var result = true;
            result = CheckOutputPathLegitimacy();
            return result;
        }

        private bool CheckOutputPathLegitimacy() 
        {
            var result = true;
            OutputPath = OutputPath.Trim();
            if (Directory.Exists(OutputPath) == false)
            {
                try
                {
                    Directory.CreateDirectory(OutputPath);
                    Append2Console($"已创建文件夹\"{OutputPath}\"");
                }
                catch (Exception ex)
                {
                    Append2Console($"文件夹\"{OutputPath}\"不存在，或者路径不合法。\n{ex}");
                    OuputPathErrorContent = $"文件夹\"{OutputPath}\"不存在，或者路径不合法。";
                    return false;
                }
            }
            if (Directory.GetFiles(OutputPath).Length != 0)
            {
                Append2Console($"文件夹\"{OutputPath}\"不为空");
                OuputPathErrorContent = $"文件夹\"{OutputPath}\"不为空";
                result = false;
            }
            return result;
        }

        private void MainProcess() 
        {
            List<string> list = new List<string>();
            if (IsAcceptBmp) list.Add(".bmp");
            if (IsAcceptJpeg) list.Add(".jpg"); list.Add(".jpeg");
            if (IsAcceptPng) list.Add(".png");
            if (IsAcceptTga) list.Add(".tga");
            if (IsAcceptTiff) list.Add(".tif"); list.Add(".tiff");
            if (IsAcceptWebP) list.Add(".webp");
            foreach (var item in InputFileList)
            {
                var originalExt = Path.GetExtension(item.Name);
                if (originalExt != null && list.Contains(originalExt) == false)
                {
                    Append2Console($"{item} 扩展名不在允许读取的格式中，跳过");
                    ProcessedCount++;
                    continue;
                }
                var imageInfo = Image.Identify(item.Path);
                var originWidth = imageInfo.Width;
                var originHeigth = imageInfo.Height;
                // 限制适应竖图
                int inputResUpperLimitWidth = InputResUpperLimitWidth;
                int inputResUpperLimitHeight = InputResUpperLimitHeight;
                int inputResLowerLimitWidth = InputResLowerLimitWidth;
                int inputResLowerLimitHeight = InputResLowerLimitHeight;
                if (IsInputResLimitationAdaptPortraitImage && (originHeigth > originWidth))
                {
                    inputResUpperLimitWidth = InputResUpperLimitHeight;
                    inputResUpperLimitHeight = InputResUpperLimitWidth;
                    inputResLowerLimitWidth = InputResLowerLimitHeight;
                    inputResLowerLimitHeight = InputResLowerLimitWidth;
                }
                // 跳过不在分辨率限制内的
                if (IsEnableInputResUpperLimit && (originWidth > inputResUpperLimitWidth || originHeigth > inputResUpperLimitHeight))
                {
                    Append2Console($"{item} 大小（{originWidth} × {originHeigth}）超出上限，跳过");
                    ProcessedCount++;
                    continue;
                }
                if (IsEnableInputResLowerLimit && (originWidth < inputResLowerLimitWidth || originHeigth < inputResLowerLimitHeight))
                {
                    Append2Console($"{item} 大小（{originWidth} × {originHeigth}）低于下限，跳过");
                    ProcessedCount++;
                    continue;
                }
                var multiple_upper = 1.00;
                var multiple_lower = 0.10;
                var current_multiple = 1;
                var disk_cost = 0;
                var start_time = DateTime.Now;
                var temp_filename = Path.Combine(OutputPath, $"temp{SelectedFormatModel.Name}");
                // 设置编码器参数
                SetupSelectedEncoder();
                try
                {
                    using (Image originImage = Image.Load(item.Path))
                    {
                        Append2Console($"{item.Name} 开始处理");
                        if (IsFileSizeFirstMode)
                        {
                            // 先尝试原尺寸压缩，如果满足要求就不用缩小了
                            originImage.Save(temp_filename, SelectedFormatModel.Encoder);
                            long fileSizeInBytes = new FileInfo(temp_filename).Length;
                            Append2Console($"{item.Name} 直接保存的大小：{fileSizeInBytes / 1024} KiB");
                        }
                        else 
                        {
                            //todo
                        }
                    }
                }
                catch (Exception ex)
                {
                    //todo
                }
            }
        }

        private void SetupSelectedEncoder()
        {
            if (SelectedFormatModel.Name == ".webp")
            {
                SelectedFormatModel.Encoder = new WebpEncoder()
                {
                    FileFormat = (IsWebpLossyMode) ? WebpFileFormatType.Lossy : WebpFileFormatType.Lossless,
                    Quality = Quality
                };
            }
            else if (SelectedFormatModel.Name == ".jpg")
            {
                SelectedFormatModel.Encoder = new JpegEncoder()
                {
                    Quality = Quality
                };
            }
            else if (SelectedFormatModel.Name == ".png")
            {
                SelectedFormatModel.Encoder = new PngEncoder()
                {
                    CompressionLevel = (PngCompressionLevel)Quality
                };
            }
            else if (SelectedFormatModel.Name == ".tif")
            {
                SelectedFormatModel.Encoder = new TiffEncoder()
                {
                    CompressionLevel = (DeflateCompressionLevel)Quality
                };
            }
        }

        // https://stackoverflow.com/questions/40104765/bind-event-in-mvvm-and-pass-event-arguments-as-command-parameter
        public void DataGrid_Drop(object sender, DragEventArgs e)
        {
            // https://stackoverflow.com/questions/5662509/drag-and-drop-files-into-wpf
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] filesOrFolders = (string[])e.Data.GetData(DataFormats.FileDrop);
                HandleAppendFiles(filesOrFolders);
            }
        }

        private void HandleAppendFiles(string[] paths)
        {
            int added = 0;
            foreach (string path in paths)
            {
                if (File.Exists(path) && !IsInputFileListAlreadyContains(path) && IsSupportExtension(path))
                {
                    added++;
                    InputFileList.Add(new(Path.GetFileName(path), path));
                }
            }
            Append2Console($"试图添加 {paths.Length} 个文件，实际添加 {added} 个");
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

        private bool IsSupportExtension(string filePath)
        {
            foreach (var ext in _SupportedExtensions)
            {
                if (Path.GetExtension(filePath) == ext)
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
