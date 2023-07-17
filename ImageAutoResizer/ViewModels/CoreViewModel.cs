using ImageBatchResizer.Models;
using ImageBatchResizer.Views;
using Ookii.Dialogs.Wpf;
using Prism.Commands;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Compression.Zlib;
using SixLabors.ImageSharp.Formats;
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
using System.Threading;
using System.Threading.Tasks;
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
        private string _FullConsoleContent = "";
        private string _LiteConsoleContent = "";
        private int _SliderMinimum = 100;
        private int _SliderMaximum = 1;
        private string _CompressDescrption = "有损压缩质量";
        private bool _IsEnableCompressAdjust = true;
        private bool _IsWebpLossyMode = true;
        private bool _IsWebpEncoder = true;
        private CancellationTokenSource? conncetingTokenSource;
        private bool _IsEnableStartButton = true;
        private string _StartButtonContent = "开始转换";
        private bool _IsEnableWebpUniqueArgs = false;
        private int _EncodingMethod = 3;
        private int _ParallelCount = 1;
        private bool _IsCheckDontCoverExist = true;

        public bool IsEnableParametersPanel
        {
            get => _isEnableParametersPanel;
            set
            {
                SetValue(ref _isEnableParametersPanel, value);
            }
        }
        public bool IsEnableStartButton
        {
            get => _IsEnableStartButton;
            set
            {
                SetValue(ref _IsEnableStartButton, value);
            }
        }
        public string StartButtonContent
        {
            get => _StartButtonContent;
            set
            {
                SetValue(ref _StartButtonContent, value);
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
                if (value == true)
                {
                    IsEnableWebpUniqueArgs = true;
                }
                else
                {
                    IsEnableWebpUniqueArgs = false;
                }
            }
        }
        public int EncodingMethod
        {
            get => _EncodingMethod;
            set
            {
                SetValue(ref _EncodingMethod, value);
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
                    LosslessCompressUI(100);
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

        public bool IsEnableWebpUniqueArgs
        {
            get => _IsEnableWebpUniqueArgs;
            set
            {
                SetValue(ref _IsEnableWebpUniqueArgs, value);
            }
        }

        public int MaxParallelCount
        {
            get => Environment.ProcessorCount;
        }

        public int ParallelCount
        {
            get => _ParallelCount;
            set
            {
                SetValue(ref _ParallelCount, value);
            }
        }

        public bool IsCheckDontCoverExist
        {
            get => _IsCheckDontCoverExist;
            set
            {
                SetValue(ref _IsCheckDontCoverExist, value);
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
                else if(value.Name == ".png" || value.Name == ".tif")
                {
                    LosslessCompressUI();
                }
                else if (value.Name == ".webp" && !IsWebpLossyMode)
                {
                    LosslessCompressUI(100);
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

        private void LosslessCompressUI(int sliderMaximum = 9)
        {
            IsEnableCompressAdjust = true;
            CompressDescrption = "无损压缩等级";
            SliderMaximum = sliderMaximum;
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

        public string FullConsoleContent
        {
            get => _FullConsoleContent;
            private set
            {
                SetValue(ref _FullConsoleContent, value);
            }
        }
        public string LiteConsoleContent
        {
            get => _LiteConsoleContent;
            private set
            {
                SetValue(ref _LiteConsoleContent, value);
            }
        }

        private void Append2FullConsole(string message, bool liteAlso = false)
        {
            lock (FullConsoleContent)
            {
                FullConsoleContent = $"{FullConsoleContent}\n\n{DateTime.Now} {message}";
            }
            if (liteAlso)
            {
                Append2LiteConsole(message);
            }
        }
        private void Append2LiteConsole(string message)
        {
            lock (LiteConsoleContent)
            {
                LiteConsoleContent = $"{LiteConsoleContent}\n\n{DateTime.Now} {message}";
            }
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
        public DelegateCommand DeleteChosenCommand { get; }
        public DelegateCommand DeleteAllCommand { get; }
        public DelegateCommand ResetAllCommand { get; }
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
                ProcessedPercent = ProcessedCount / (double)InputFileList.Count * 100;
            }
            catch (DivideByZeroException dbze)
            {
                ProcessedPercent = 0;
            }
        }

        public CoreViewModel()
        {
            Append2FullConsole("正在初始化");
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
                ProcessedCount = 0;
            });
            ResetAllCommand = new DelegateCommand(() =>
            {
                foreach (var item in InputFileList)
                {
                    item.Processed = false;
                    item.ResizedPath = "";
                }
                ProcessedCount = 0;
            });
            StartCommand = new DelegateCommand(async () => 
            {
                var t = Task.Run(async() => await Start());
                await t;
            });
            ClearConsoleCommand = new DelegateCommand(() =>
            {
                FullConsoleContent = "";
                LiteConsoleContent = "";
            });
            ResetOuputPathErrorContentCommand = new DelegateCommand(() => OuputPathErrorContent = "");

            Append2FullConsole("初始化完毕");
        }

        private async Task Start()
        {
            IsEnableStartButton = false;
            if (conncetingTokenSource == null)
            {
                // 锁定可编辑的参数
                IsEnableParametersPanel = false;
                if (CheckParameterLegitimacy() == false)
                {
                    IsEnableParametersPanel = true;
                    IsEnableStartButton = true;
                }
                else
                {
                    ProcessedCount = 0;
                    conncetingTokenSource = new();
                    StartButtonContent = "中止转换";
                    IsEnableStartButton = true;
                    await MainProcessAsync(conncetingTokenSource.Token);
                    StartButtonContent = "开始转换";
                    Append2FullConsole("处理结束", true);
                    IsEnableStartButton = true;
                    IsEnableParametersPanel = true;
                    conncetingTokenSource.Dispose();
                    conncetingTokenSource = null;
                }
            }
            else 
            {
                conncetingTokenSource.Cancel();
                Append2FullConsole("中止请求已发出，正在等待中止请求被响应", true);
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
            OutputPath = OutputPath.Trim();
            if (Directory.Exists(OutputPath) == false)
            {
                try
                {
                    Directory.CreateDirectory(OutputPath);
                    Append2FullConsole($"已创建文件夹\"{OutputPath}\"", true);
                }
                catch (Exception ex)
                {
                    Append2FullConsole($"文件夹\"{OutputPath}\"不存在，或者路径不合法。\n{ex}", true);
                    OuputPathErrorContent = $"文件夹\"{OutputPath}\"不存在，或者路径不合法。";
                    return false;
                }
            }
            OuputPathErrorContent = "";
            return true;
        }

        private async Task MainProcessAsync(CancellationToken token = default)
        {
            var total_start_time = DateTime.Now;
            long total_disk_cost = 0;
            var success_index = 1;
            Append2FullConsole($"开始时间：{total_start_time}", true);
            List<string> list = new List<string>();
            if (IsAcceptBmp) list.Add(".bmp");
            if (IsAcceptJpeg) list.Add(".jpg"); list.Add(".jpeg");
            if (IsAcceptPng) list.Add(".png");
            if (IsAcceptTga) list.Add(".tga");
            if (IsAcceptTiff) list.Add(".tif"); list.Add(".tiff");
            if (IsAcceptWebP) list.Add(".webp");

            SemaphoreSlim semaphore = new SemaphoreSlim(ParallelCount);
            List<Task<(bool, bool, long)>> tasks = new();
            foreach (var item in InputFileList)
            {
                if (token.IsCancellationRequested == true)
                {
                    break;
                }
                if (item.Processed)
                {
                    Append2FullConsole($"{item.Name} 此前已被处理，跳过", true);
                    ProcessedCount++;
                    continue;
                }
                try
                {
                    await semaphore.WaitAsync(token); // 等待信号量
                }
                catch (OperationCanceledException ocex) 
                {
                    break;
                }
                if (token.IsCancellationRequested == true)
                {
                    break;
                }
                tasks.Add(Task.Run(async () => 
                {
                    try
                    {
                        var result = await SingleProcessAsync(list, item, token);
                        semaphore.Release();
                        return result;
                    }
                    catch (OperationCanceledException ocex)
                    {
                        return (false, false, 0);
                    }
                })); 
            }

            var results = await Task.WhenAll(tasks); // 等待所有任务完成，并获取返回值
            int saved_count = 0;
            foreach ((bool isProcessed, bool isSaved, long disk_cost) in results)
            {
                if (isSaved)
                {
                    saved_count++;
                    total_disk_cost += disk_cost;
                }
            }
            Append2FullConsole($"已处理 {ProcessedCount} 个，保存 {saved_count} 个，总耗时：{(DateTime.Now - total_start_time).TotalSeconds} 秒，磁盘总写入 {Math.Round((double)total_disk_cost / 1024, 3)} KiB", true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="item"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="OperationCanceledException"></exception>
        private async Task<(bool, bool, long)> SingleProcessAsync(List<string> list, FileModel item, CancellationToken token = default)
        {
            long disk_cost = 0;
            var originalExt = Path.GetExtension(item.Name);
            if (originalExt != null && list.Contains(originalExt) == false)
            {
                Append2FullConsole($"{item.Name} 扩展名不在允许读取的格式中，跳过");
                item.Processed = true;
                ProcessedCount++;
                return (true, false, disk_cost);
            }
            var imageInfo = await Image.IdentifyAsync(item.Path);
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
                Append2FullConsole($"{item} 大小（{originWidth} × {originHeigth}）超出上限，跳过");
                item.Processed = true;
                ProcessedCount++;
                return (true, false, disk_cost);
            }
            if (IsEnableInputResLowerLimit && (originWidth < inputResLowerLimitWidth || originHeigth < inputResLowerLimitHeight))
            {
                Append2FullConsole($"{item} 大小（{originWidth} × {originHeigth}）低于下限，跳过");
                item.Processed = true;
                ProcessedCount++;
                return (true, false, disk_cost);
            }
            var start_time = DateTime.Now;
            var temp_filename = Path.Combine(OutputPath, $"temp{SelectedFormatModel.Name}");
            // 设置编码器参数
            SetupSelectedEncoder();

            using (Image originImage = await Image.LoadAsync(item.Path))
            {
                Append2FullConsole($"{item.Name} 开始处理");
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    if (IsFileSizeFirstMode)
                    {
                        // 先尝试原尺寸压缩，如果满足要求就不用缩小了
                        if (token.IsCancellationRequested) throw new OperationCanceledException(token);
                        var st1 = DateTime.Now;
                        await originImage.SaveAsync(memoryStream, SelectedFormatModel.Encoder, token);
                        Append2FullConsole($"SaveAsync 耗时 {(DateTime.Now - st1).TotalSeconds} 秒");

                        long fileSizeInBytes = memoryStream.Length;
                        //disk_cost += fileSizeInBytes;
                        //total_disk_cost += disk_cost;
                        Append2FullConsole($"{item.Name} 分辨率未改变 ({originWidth}×{originHeigth})，直接保存的大小：{Math.Round((double)fileSizeInBytes / 1024, 3)} KiB");
                        if (fileSizeInBytes < TargetSizeUpperLimit)
                        {
                            item.Processed = true;
                            if (fileSizeInBytes < TargetSizeLowerLimit && IsDeleteSmallerThanTarget)
                            {
                                //File.Delete(temp_filename);
                                Append2FullConsole($"{item.Name} 直接转换的结果被删除，因为小于文件大小区间。耗时：{(DateTime.Now - start_time).TotalSeconds} 秒，磁盘写入 {Math.Round((double)disk_cost / 1024, 3)} KiB");
                                item.Processed = true;
                                ProcessedCount++;
                                return (true, false, disk_cost);
                            }
                            else
                            {
                                var final_filename = Path.Combine(OutputPath, $"{RenameResult(Path.GetFileNameWithoutExtension(item.Name))}{SelectedFormatModel.Name}");
                                var existence = File.Exists(final_filename);
                                if (File.Exists(final_filename) && IsCheckDontCoverExist)
                                {
                                    item.ResizedPath = $"文件已存在：{final_filename}";
                                    Append2FullConsole($"{final_filename} 已存在未覆盖，耗时：{(DateTime.Now - start_time).TotalSeconds} 秒，磁盘写入 {Math.Round((double)disk_cost / 1024, 3)} KiB", true);
                                    ProcessedCount++;
                                    return (false, false, disk_cost);
                                }
                                else
                                {
                                    await File.WriteAllBytesAsync(final_filename, memoryStream.ToArray(), token);
                                    disk_cost += memoryStream.Length;
                                    item.ResizedPath = final_filename;
                                    Append2FullConsole($"{item.Name} 的结果已{(existence?"覆盖":"保存")}至 {final_filename}，耗时：{(DateTime.Now - start_time).TotalSeconds} 秒，磁盘写入 {Math.Round((double)disk_cost / 1024, 3)} KiB", true);
                                    item.Processed = true;
                                    ProcessedCount++;
                                    return (true, true, disk_cost);
                                }
                            }
                        }
                        else
                        {
                            (int resized_width, int resized_height, _) = await BinarySearchShrink(item.Name, originImage, SelectedFormatModel.Encoder, memoryStream, token);
                            // 重新读取一下实际处理后的边长
                            memoryStream.Position = 0;
                            imageInfo = await Image.IdentifyAsync(memoryStream);
                            resized_width = imageInfo.Width;
                            resized_height = imageInfo.Height;
                            fileSizeInBytes = memoryStream.Length;
                            var width_percentage = ((resized_width / (double)originImage.Width) * 100).ToString("f2");
                            var height_percentage = ((resized_height / (double)originImage.Height) * 100).ToString("f2");
                            var pixel_percentage = ((resized_width * resized_height) / (double)(originImage.Width * originImage.Height) * 100).ToString("f2");

                            if (IsEnableResLowerLimit)
                            {
                                (bool isUnder, int true_width_limit, int true_height_limit) = IsUnderResolutionLimit(resized_width, resized_height);
                                if (isUnder)
                                {
                                    if (FileSizeFirst_Delete)
                                    {
                                        //File.Delete(temp_filename);
                                        Append2FullConsole($"{item.Name} 转换的结果被删除，{resized_width}×{resized_height}, {width_percentage}% 宽 {height_percentage}% 高, {pixel_percentage}% 像素, 耗时：{(DateTime.Now - start_time).TotalSeconds} 秒，磁盘写入 {Math.Round((double)disk_cost / 1024, 3)} KiB");
                                        item.Processed = true;
                                        ProcessedCount++;
                                        return (true, false, disk_cost);
                                    }
                                    else if (FileSizeFirst_IgnoreSizeLimit)
                                    {
                                        Image image_processed = ProcessingExtensions.Clone(originImage, x => x.Resize(new ResizeOptions()
                                        {
                                            Size = new SixLabors.ImageSharp.Size(true_width_limit, true_height_limit),
                                            Mode = SelectedResizeModeModel.Mode,
                                            Sampler = SelectedResamplerModel.Resampler,
                                        }));
                                        memoryStream.SetLength(0);
                                        if (token.IsCancellationRequested) throw new OperationCanceledException(token);
                                        st1 = DateTime.Now;
                                        await image_processed.SaveAsync(memoryStream, SelectedFormatModel.Encoder, token);
                                        // 重新读取一下实际处理后的边长
                                        memoryStream.Position = 0;
                                        imageInfo = await Image.IdentifyAsync(memoryStream);
                                        resized_width = imageInfo.Width;
                                        resized_height = imageInfo.Height;
                                        fileSizeInBytes = memoryStream.Length;
                                        width_percentage = ((resized_width / (double)originImage.Width) * 100).ToString("f2");
                                        height_percentage = ((resized_height / (double)originImage.Height) * 100).ToString("f2");
                                        pixel_percentage = ((resized_width * resized_height) / (double)(originImage.Width * originImage.Height) * 100).ToString("f2");
                                        Append2FullConsole($"SaveAsync 耗时 {(DateTime.Now - st1).TotalSeconds} 秒");
                                        
                                        Append2FullConsole($"{item.Name} 的分辨率重新设置为 {resized_width}×{resized_height}, {width_percentage}% 宽 {height_percentage}% 高, {pixel_percentage}% 像素, {Math.Round((double)fileSizeInBytes / 1024, 3)} KiB");
                                        //disk_cost += file_size;
                                        //total_disk_cost += fileSizeInBytes;
                                    }
                                    // 不做处理
                                }
                            }
                            var final_filename = Path.Combine(OutputPath, $"{RenameResult(Path.GetFileNameWithoutExtension(item.Name))}{SelectedFormatModel.Name}");
                            var existence = File.Exists(final_filename);
                            if (File.Exists(final_filename) && IsCheckDontCoverExist)
                            {
                                item.ResizedPath = $"文件已存在：{final_filename}";
                                Append2FullConsole($"{final_filename} 已存在未覆盖，{resized_width}×{resized_height}, {width_percentage}% 宽 {height_percentage}% 高, {pixel_percentage}% 像素, 耗时：{(DateTime.Now - start_time).TotalSeconds} 秒，磁盘写入 {Math.Round((double)disk_cost / 1024, 3)} KiB", true);
                                ProcessedCount++;
                                return (false, false, disk_cost);
                            }
                            else
                            {
                                await File.WriteAllBytesAsync(final_filename, memoryStream.ToArray(), token);
                                if (token.IsCancellationRequested) throw new OperationCanceledException(token);
                                disk_cost += memoryStream.Length;
                                item.ResizedPath = final_filename;
                                Append2FullConsole($"{item.Name} 转换的结果已{(existence ? "覆盖" : "保存")}至 {final_filename}，{resized_width}×{resized_height}, {width_percentage}% 宽 {height_percentage}% 高, {pixel_percentage}% 像素, 耗时：{(DateTime.Now - start_time).TotalSeconds} 秒，磁盘写入 {Math.Round((double)disk_cost / 1024, 3)} KiB", true);
                                item.Processed = true;
                                ProcessedCount++;
                                return (true, true, disk_cost);
                            }
                        }
                    }
                    else
                    {
                        //todo
                        throw new NotImplementedException();
                    }
                }

                //return (true, true, disk_cost);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="originImage"></param>
        /// <param name="encoder"></param>
        /// <param name="memoryStream"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="OperationCanceledException"></exception>
        private async Task<(int, int, long)> BinarySearchShrink(string name, Image originImage, ImageEncoder encoder, MemoryStream memoryStream, CancellationToken token = default)
        {
            double multiple_upper = 1.00;
            double multiple_lower = 0;
            double current_multiple = 1;
            long disk_cost = 0;
            int tried_count = 1;
            int resized_width = 0;
            int resized_height = 0;
            double last_multiple = 0;
            while (tried_count <= BinarySearchTimesLimit)
            {
                if (token.IsCancellationRequested) throw new OperationCanceledException(token);
                last_multiple = current_multiple;
                current_multiple = (multiple_upper + multiple_lower) / 2;
                if (current_multiple == last_multiple) 
                {
                    MessageBox.Show("相同！");
                }
                resized_width = (int)(originImage.Width * current_multiple);
                resized_height = (int)(originImage.Height * current_multiple);
                Image image_processed = ProcessingExtensions.Clone(originImage, x => x.Resize(new ResizeOptions()
                {
                    Size = new SixLabors.ImageSharp.Size(resized_width, resized_height),
                    Mode = SelectedResizeModeModel.Mode,
                    Sampler = SelectedResamplerModel.Resampler,
                }));
                memoryStream.SetLength(0);
                var st1 = DateTime.Now;
                await image_processed.SaveAsync(memoryStream, encoder, token);
                if (token.IsCancellationRequested == true) throw new OperationCanceledException(token);
                Append2FullConsole($"SaveAsync 耗时 {(DateTime.Now - st1).TotalSeconds} 秒");
                long fileSizeInBytes = memoryStream.Length;
                disk_cost += fileSizeInBytes;
                // 重新读取一下实际处理后的边长
                memoryStream.Position = 0;
                ImageInfo imageInfo = await Image.IdentifyAsync(memoryStream);
                resized_width = imageInfo.Width;
                resized_height = imageInfo.Height;
                var width_percentage = ((resized_width / (double)originImage.Width) * 100).ToString("f2");
                var height_percentage = ((resized_height / (double)originImage.Height) * 100).ToString("f2");
                var pixel_percentage = ((resized_width * resized_height) / (double)(originImage.Width * originImage.Height) * 100).ToString("f2");

                Append2FullConsole($"{name} 第 {tried_count} 次尝试：{resized_width}×{resized_height}, {width_percentage}% 宽 {height_percentage}% 高, {pixel_percentage}% 像素, {Math.Round((double)fileSizeInBytes/1024, 3)} KiB");

                if (fileSizeInBytes > TargetSizeUpperLimit * 1024)
                {
                    multiple_upper = current_multiple;
                }
                else if (fileSizeInBytes < TargetSizeLowerLimit * 1024)
                {
                    multiple_lower = current_multiple;
                }
                else 
                {
                    break;
                }
                tried_count++;
            }
            return (resized_width, resized_height, disk_cost);
        }

        private (bool, int, int) IsUnderResolutionLimit(int width, int height)
        {
            if (IsOutputResLimitationAdaptPortraitImage == false)
            {
                if (width < TargetResolutionWidthLowerLimit || height < TargetResolutionHeightLowerLimit)
                {
                    return (true, TargetResolutionWidthLowerLimit, TargetResolutionHeightLowerLimit);
                }
                else
                {
                    return (false, 0, 0);
                }
            }
            else 
            {
                // non vertical image
                if (width >= height)
                {
                    if (width < TargetResolutionWidthLowerLimit || height < TargetResolutionHeightLowerLimit)
                    {
                        return (true, TargetResolutionWidthLowerLimit, TargetResolutionHeightLowerLimit);
                    }
                    else
                    {
                        return (false, 0, 0);
                    }
                }
                // vertical image
                else
                {
                    if (width < TargetResolutionHeightLowerLimit || height < TargetResolutionWidthLowerLimit)
                    {
                        return (true, TargetResolutionHeightLowerLimit, TargetResolutionWidthLowerLimit);
                    }
                    else
                    {
                        return (false, 0, 0);
                    }
                }
            }
        }

        private string RenameResult(string originName)
        {
            string finalName = OutputFileNamePattern;
            finalName = finalName.Replace("{time}", DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss"));
            finalName = finalName.Replace("{name}", originName);
            finalName = finalName.Replace("{quality}", Quality.ToString());

            return finalName;
        }

        private void SetupSelectedEncoder()
        {
            if (SelectedFormatModel.Name == ".webp")
            {
                SelectedFormatModel.Encoder = new WebpEncoder()
                {
                    FileFormat = (IsWebpLossyMode) ? WebpFileFormatType.Lossy : WebpFileFormatType.Lossless,
                    Quality = Quality,
                    Method = (WebpEncodingMethod)EncodingMethod
                    
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
            Append2FullConsole($"试图添加 {paths.Length} 个文件，实际添加 {added} 个");
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
    }
}
