using ImageBatchResizer.Enums;
using ImageBatchResizer.Models;
using ImageBatchResizer.Services;
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
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace ImageBatchResizer.ViewModels
{
    public class CoreViewModel : ViewModelBase
    {
        private readonly bool initReady = false;

        private WindowsSettingsService windowsSettingsService = new(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ImageBatchResizer"));
        private bool _isEnableParametersPanel = true;
        private FormatModel _selectedFormatModel;
        private ResamplerModel _selectedResamplerModel;
        private ResizeModeModel _selectedResizeModeModel;
        private bool _isEnableInputResUpperLimit = false;
        private bool _isEnableInputResLowerLimit = false;
        private bool _isInputResLimitationAdaptPortraitImage = false;
        private bool _isAcceptBmp = true;
        private bool _isAcceptJpeg = true;
        private bool _isAcceptTga = true;
        private bool _isAcceptPng = true;
        private bool _isAcceptTiff = true;
        private bool _isAcceptWebP = true;
        private bool _isFileSizeFirstMode = true;
        private bool _isResFirstMode = false;
        private double _targetSizeLowerLimit = 300;
        private double _targetSizeUpperLimit = 350;
        private double _binarySearchTimesLimit = 20;
        private bool _isDeleteSmallerThanTarget = false;
        private double _targetResolutionWidthLowerLimit = 1920;
        private double _targetResolutionHeightLowerLimit = 1200;
        private bool _isEnableResLowerLimit = false;
        private bool _isOutputResLimitationAdaptPortraitImage = false;
        private bool _fileSizeFirst_DoNothing = true;
        private bool _fileSizeFirst_Delete = false;
        private bool _fileSizeFirst_IgnoreSizeLimit = false;
        private bool _fileSizeFirst_ReduceQuality = false;
        private bool _isEnableReduceQuality = true;
        private bool _isEnableTargetSizeLimit = false;
        private bool _resFirst_DoNothing = true;
        private bool _resFirst_Delete = false;
        private bool _resFirst_IgnoreSizeLimit = false;
        private int _quality = 90;
        private string _outputPath = "";
        private string _outputFileNamePattern = "";
        private double _inputResUpperLimitWidth = 10000;
        private double _InputResUpperLimitHeight = 8000;
        private double _InputResLowerLimitWidth = 1920;
        private double _InputResLowerLimitHeight = 1200;
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
        private int _SliderMinimum = 1;
        private int _SliderMaximum = 100;
        private string _CompressDescrption = "有损压缩质量";
        private bool _IsEnableCompressAdjust = true;
        private bool _IsWebpLossyMode = true;
        private bool _IsWebpEncoder = true;
        private CancellationTokenSource? conncetingTokenSource;
        private bool _IsEnableStartButton = true;
        private string _StartButtonContent = "开始转换";
        private bool _IsEnableWebpUniqueArgs = false;
        private int _EncodingMethod = 4;
        private int _ParallelCount = 1;
        private bool _IsCheckDontCoverExist = true;
        private FlowDocument _fullConsoleContentFlowDocument = new();
        private FlowDocument _liteConsoleContentFlowDocument = new();

        public bool IsEnableParametersPanel
        {
            get => _isEnableParametersPanel;
            set
            {
                windowsSettingsService.Set("IsEnableInputResUpperLimit", value);
                SetValue(ref _isEnableParametersPanel, value);
            }
        }
        public bool IsEnableStartButton
        {
            get => _IsEnableStartButton;
            set
            {
                windowsSettingsService.Set("IsEnableStartButton", value);
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
                windowsSettingsService.Set("IsEnableInputResUpperLimit", value);
                SetValue(ref _isEnableInputResUpperLimit, value);
            }
        }
        public double InputResUpperLimitWidth
        {
            get => _inputResUpperLimitWidth;
            set
            {
                windowsSettingsService.Set("InputResUpperLimitWidth", value);
                SetValue(ref _inputResUpperLimitWidth, value);
            }
        }
        public double InputResUpperLimitHeight
        {
            get => _InputResUpperLimitHeight;
            set
            {
                windowsSettingsService.Set("InputResUpperLimitHeight", value);
                SetValue(ref _InputResUpperLimitHeight, value);
            }
        }

        public bool IsEnableInputResLowerLimit
        {
            get => _isEnableInputResLowerLimit;
            set
            {
                windowsSettingsService.Set("IsEnableInputResLowerLimit", value);
                SetValue(ref _isEnableInputResLowerLimit, value);
            }
        }
        public double InputResLowerLimitWidth
        {
            get => _InputResLowerLimitWidth;
            set
            {
                windowsSettingsService.Set("InputResLowerLimitWidth", value);
                SetValue(ref _InputResLowerLimitWidth, value);
            }
        }
        public double InputResLowerLimitHeight
        {
            get => _InputResLowerLimitHeight;
            set
            {
                windowsSettingsService.Set("InputResLowerLimitHeight", value);
                SetValue(ref _InputResLowerLimitHeight, value);
            }
        }

        public bool IsInputResLimitationAdaptPortraitImage
        {
            get => _isInputResLimitationAdaptPortraitImage;
            set
            {
                windowsSettingsService.Set("IsInputResLimitationAdaptPortraitImage", value);
                SetValue(ref _isInputResLimitationAdaptPortraitImage, value);
            }
        }

        public bool IsAcceptBmp
        {
            get => _isAcceptBmp;
            set
            {
                windowsSettingsService.Set("IsAcceptBmp", value);
                SetValue(ref _isAcceptBmp, value);
            }
        }
        public bool IsAcceptJpeg
        {
            get => _isAcceptJpeg;
            set
            {
                windowsSettingsService.Set("IsAcceptJpeg", value);
                SetValue(ref _isAcceptJpeg, value);
            }
        }
        public bool IsAcceptPng
        {
            get => _isAcceptPng;
            set
            {
                windowsSettingsService.Set("IsAcceptPng", value);
                SetValue(ref _isAcceptPng, value);
            }
        }
        public bool IsAcceptTga
        {
            get => _isAcceptTga;
            set
            {
                windowsSettingsService.Set("IsAcceptTga", value);
                SetValue(ref _isAcceptTga, value);
            }
        }
        public bool IsAcceptTiff
        {
            get => _isAcceptTiff;
            set
            {
                windowsSettingsService.Set("IsAcceptTiff", value);
                SetValue(ref _isAcceptTiff, value);
            }
        }
        public bool IsAcceptWebP
        {
            get => _isAcceptWebP;
            set
            {
                windowsSettingsService.Set("IsAcceptWebP", value);
                SetValue(ref _isAcceptWebP, value);
            }
        }
        #endregion

        public bool IsFileSizeFirstMode
        {
            get => _isFileSizeFirstMode;
            set
            {
                windowsSettingsService.Set("IsFileSizeFirstMode", value);
                SetValue(ref _isFileSizeFirstMode, value);
            }
        }
        public bool IsResFirstMode
        {
            get => _isResFirstMode;
            set
            {
                windowsSettingsService.Set("IsResFirstMode", value);
                SetValue(ref _isResFirstMode, value);
            }
        }

        #region 输出参数
        public double TargetSizeLowerLimit
        {
            get => _targetSizeLowerLimit;
            set
            {
                windowsSettingsService.Set("TargetSizeLowerLimit", value);
                SetValue(ref _targetSizeLowerLimit, value);
            }
        }
        public double TargetSizeUpperLimit
        {
            get => _targetSizeUpperLimit;
            set
            {
                windowsSettingsService.Set("TargetSizeUpperLimit", value);
                SetValue(ref _targetSizeUpperLimit, value);
            }
        }
        public double BinarySearchTimesLimit
        {
            get => _binarySearchTimesLimit;
            set
            {
                windowsSettingsService.Set("BinarySearchTimesLimit", value);
                SetValue(ref _binarySearchTimesLimit, value);
            }
        }
        public bool IsDeleteSmallerThanTarget
        {
            get => _isDeleteSmallerThanTarget;
            set
            {
                windowsSettingsService.Set("IsDeleteSmallerThanTarget", value);
                SetValue(ref _isDeleteSmallerThanTarget, value);
            }
        }
        public bool IsEnableResLowerLimit
        {
            get => _isEnableResLowerLimit;
            set
            {
                windowsSettingsService.Set("IsEnableResLowerLimit", value);
                SetValue(ref _isEnableResLowerLimit, value);
            }
        }

        public double TargetResolutionWidthLowerLimit
        {
            get => _targetResolutionWidthLowerLimit;
            set
            {
                windowsSettingsService.Set("TargetResolutionWidthLowerLimit", value);
                SetValue(ref _targetResolutionWidthLowerLimit, value);
            }
        }
        public double TargetResolutionHeightLowerLimit
        {
            get => _targetResolutionHeightLowerLimit;
            set
            {
                windowsSettingsService.Set("TargetResolutionHeightLowerLimit", value);
                SetValue(ref _targetResolutionHeightLowerLimit, value);
            }
        }
        public bool IsOutputResLimitationAdaptPortraitImage
        {
            get => _isOutputResLimitationAdaptPortraitImage;
            set
            {
                windowsSettingsService.Set("IsOutputResLimitationAdaptPortraitImage", value);
                SetValue(ref _isOutputResLimitationAdaptPortraitImage, value);
            }
        }
        public bool FileSizeFirst_DoNothing
        {
            get => _fileSizeFirst_DoNothing;
            set
            {
                windowsSettingsService.Set("FileSizeFirst_DoNothing", value);
                SetValue(ref _fileSizeFirst_DoNothing, value);
            }
        }
        public bool FileSizeFirst_Delete
        {
            get => _fileSizeFirst_Delete;
            set
            {
                windowsSettingsService.Set("FileSizeFirst_Delete", value);
                SetValue(ref _fileSizeFirst_Delete, value);
            }
        }
        public bool FileSizeFirst_IgnoreSizeLimit
        {
            get => _fileSizeFirst_IgnoreSizeLimit;
            set
            {
                windowsSettingsService.Set("FileSizeFirst_IgnoreSizeLimit", value);
                SetValue(ref _fileSizeFirst_IgnoreSizeLimit, value);
            }
        }
        public bool FileSizeFirst_ReduceQuality
        {
            get => _fileSizeFirst_ReduceQuality;
            set
            {
                windowsSettingsService.Set("FileSizeFirst_ReduceQuality", value);
                SetValue(ref _fileSizeFirst_ReduceQuality, value);
            }
        }
        public bool IsEnableReduceQuality
        {
            get => _isEnableReduceQuality;
            set
            {
                windowsSettingsService.Set("IsEnableReduceQuality", value);
                SetValue(ref _isEnableReduceQuality, value);
            }
        }
        public bool IsEnableTargetSizeLimit
        {
            get => _isEnableTargetSizeLimit;
            set
            {
                windowsSettingsService.Set("IsEnableTargetSizeLimit", value);
                SetValue(ref _isEnableTargetSizeLimit, value);
            }
        }
        public bool ResFirst_DoNothing
        {
            get => _resFirst_DoNothing;
            set
            {
                windowsSettingsService.Set("ResFirst_DoNothing", value);
                SetValue(ref _resFirst_DoNothing, value);
            }
        }
        public bool ResFirst_Delete
        {
            get => _resFirst_Delete;
            set
            {
                windowsSettingsService.Set("ResFirst_Delete", value);
                SetValue(ref _resFirst_Delete, value);
            }
        }
        public bool ResFirst_IgnoreSizeLimit
        {
            get => _resFirst_IgnoreSizeLimit;
            set
            {
                windowsSettingsService.Set("ResFirst_IgnoreSizeLimit", value);
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
                windowsSettingsService.Set("Quality", value);
                SetValue(ref _quality, value);
            }
        }
        public bool IsEnableCompressAdjust
        {
            get => _IsEnableCompressAdjust;
            set
            {
                windowsSettingsService.Set("IsEnableCompressAdjust", value);
                SetValue(ref _IsEnableCompressAdjust, value);
            }
        }
        public bool IsWebpEncoder
        {
            get => _IsWebpEncoder;
            set
            {
                windowsSettingsService.Set("IsWebpEncoder", value);
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
                windowsSettingsService.Set("EncodingMethod", value);
                SetValue(ref _EncodingMethod, value);
            }
        }
        public bool IsWebpLossyMode
        {
            get => _IsWebpLossyMode;
            set
            {
                windowsSettingsService.Set("IsWebpLossyMode", value);
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
        public bool IsNotWebpLossyMode
        {
            get => !_IsWebpLossyMode;
            set
            {
                windowsSettingsService.Set("IsNotWebpLossyMode", value);
                SetValue(ref _IsWebpLossyMode, !value);
            }
        }
        public int SliderMinimum
        {
            get => _SliderMinimum;
            set
            {
                windowsSettingsService.Set("SliderMinimum", value);
                SetValue(ref _SliderMinimum, value);
            }
        }
        public int SliderMaximum
        {
            get => _SliderMaximum;
            set
            {
                windowsSettingsService.Set("SliderMaximum", value);
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
                windowsSettingsService.Set("IsEnableWebpUniqueArgs", value);
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
                windowsSettingsService.Set("IsCheckDontCoverExist", value);
                SetValue(ref _IsCheckDontCoverExist, value);
            }
        }
        public bool IsCheckCoverExist
        {
            get => !_IsCheckDontCoverExist;
            set
            {
                windowsSettingsService.Set("IsCheckCoverExist", value);
                SetValue(ref _IsCheckDontCoverExist, !value);
            }
        }

        public ObservableCollection<FormatModel> FormatList { get; set; }

        public FormatModel SelectedFormatModel
        {
            get => _selectedFormatModel;
            set
            {
                SetValue(ref _selectedFormatModel, value);
                if (value.DisplayName == ".webp")
                {
                    IsWebpEncoder = true;
                }
                else 
                {
                    IsWebpEncoder = false;
                }
                if (value.DisplayName == ".jpg" || value.DisplayName == ".webp")
                {
                    IsEnableReduceQuality = true;
                }
                else 
                {
                    IsEnableReduceQuality = false;
                    if (FileSizeFirst_ReduceQuality)
                    {
                        FileSizeFirst_ReduceQuality = false;
                        FileSizeFirst_DoNothing = true;
                    }
                }
                // 根据编码器的不同，改变压缩的描述
                if (value.DisplayName == ".jpg" || (value.DisplayName == ".webp" && IsWebpLossyMode))
                {
                    LossyCompressUI();
                }
                else if(value.DisplayName == ".png" || value.DisplayName == ".tif")
                {
                    LosslessCompressUI();
                }
                else if (value.DisplayName == ".webp" && !IsWebpLossyMode)
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
                new("裁切", SixLabors.ImageSharp.Processing.ResizeMode.Crop),
                new("严格小于边长限制", SixLabors.ImageSharp.Processing.ResizeMode.Max),
            };
            SelectedResizeModeModel = new("稍大于边长限制", SixLabors.ImageSharp.Processing.ResizeMode.Manual);
            ResizeModeList.Add(SelectedResizeModeModel);
            ResizeModeList.Add(new("短边对齐", SixLabors.ImageSharp.Processing.ResizeMode.Min));
            ResizeModeList.Add(new("填充", SixLabors.ImageSharp.Processing.ResizeMode.Pad));
            ResizeModeList.Add(new("拉伸", SixLabors.ImageSharp.Processing.ResizeMode.Stretch));
        }

        private void InitParameters()
        {
            IsEnableInputResUpperLimit = windowsSettingsService.Get("IsEnableInputResUpperLimit", false);
            InputResUpperLimitWidth = windowsSettingsService.Get("InputResUpperLimitWidth", 10000.0);
            InputResUpperLimitHeight = windowsSettingsService.Get("InputResUpperLimitHeight", 8000.0);
            IsEnableInputResLowerLimit = windowsSettingsService.Get("IsEnableInputResLowerLimit", false);
            InputResLowerLimitWidth = windowsSettingsService.Get("InputResLowerLimitWidth", 1920.0);
            InputResLowerLimitHeight = windowsSettingsService.Get("InputResLowerLimitHeight", 1200.0);
            IsInputResLimitationAdaptPortraitImage = windowsSettingsService.Get("IsInputResLimitationAdaptPortraitImage", false);
            IsAcceptBmp = windowsSettingsService.Get("IsAcceptBmp", true);
            IsAcceptJpeg = windowsSettingsService.Get("IsAcceptJpeg", true);
            IsAcceptPng = windowsSettingsService.Get("IsAcceptPng", true);
            IsAcceptTga = windowsSettingsService.Get("IsAcceptTga", true);
            IsAcceptTiff = windowsSettingsService.Get("IsAcceptTiff", true);
            IsAcceptWebP = windowsSettingsService.Get("IsAcceptWebP", true);

            IsFileSizeFirstMode = windowsSettingsService.Get("IsFileSizeFirstMode", true);
            IsResFirstMode = windowsSettingsService.Get("IsResFirstMode", false);
            TargetSizeLowerLimit = windowsSettingsService.Get("TargetSizeLowerLimit", 300.0);
            TargetSizeUpperLimit = windowsSettingsService.Get("TargetSizeUpperLimit", 350.0);
            BinarySearchTimesLimit = windowsSettingsService.Get("BinarySearchTimesLimit", 20.0);
            IsDeleteSmallerThanTarget = windowsSettingsService.Get("IsDeleteSmallerThanTarget", false);
            IsEnableResLowerLimit = windowsSettingsService.Get("IsEnableResLowerLimit", false);
            TargetResolutionWidthLowerLimit = windowsSettingsService.Get("TargetResolutionWidthLowerLimit", 1920.0);
            TargetResolutionHeightLowerLimit = windowsSettingsService.Get("TargetResolutionHeightLowerLimit", 1200.0);
            IsOutputResLimitationAdaptPortraitImage = windowsSettingsService.Get("IsOutputResLimitationAdaptPortraitImage", false);
            FileSizeFirst_DoNothing = windowsSettingsService.Get("FileSizeFirst_DoNothing", true);
            FileSizeFirst_Delete = windowsSettingsService.Get("FileSizeFirst_Delete", false);
            FileSizeFirst_IgnoreSizeLimit = windowsSettingsService.Get("FileSizeFirst_IgnoreSizeLimit", false);
            FileSizeFirst_ReduceQuality = windowsSettingsService.Get("FileSizeFirst_ReduceQuality", false);
            IsEnableReduceQuality = windowsSettingsService.Get("IsEnableReduceQuality", false);
            IsEnableTargetSizeLimit = windowsSettingsService.Get("IsEnableTargetSizeLimit", false);
            ResFirst_DoNothing = windowsSettingsService.Get("ResFirst_DoNothing", true);
            ResFirst_Delete = windowsSettingsService.Get("ResFirst_Delete", false);
            ResFirst_IgnoreSizeLimit = windowsSettingsService.Get("ResFirst_IgnoreSizeLimit", false);

            Quality = windowsSettingsService.Get("Quality", 90);
            IsEnableCompressAdjust = windowsSettingsService.Get("IsEnableCompressAdjust", true);
            IsWebpEncoder = windowsSettingsService.Get("IsWebpEncoder", true);
            EncodingMethod = windowsSettingsService.Get("EncodingMethod", 4);
            IsWebpLossyMode = windowsSettingsService.Get("IsWebpLossyMode", true);
            IsNotWebpLossyMode = windowsSettingsService.Get("IsNotWebpLossyMode", false);
            SliderMinimum = windowsSettingsService.Get("SliderMinimum", 1);
            SliderMaximum = windowsSettingsService.Get("SliderMaximum", 100);
            IsEnableWebpUniqueArgs = windowsSettingsService.Get("IsEnableWebpUniqueArgs", false);
            ParallelCount = windowsSettingsService.Get("ParallelCount", 1);
            IsCheckDontCoverExist = windowsSettingsService.Get("IsCheckDontCoverExist", true);
            IsCheckCoverExist = windowsSettingsService.Get("IsCheckCoverExist", false);

            OutputPath = windowsSettingsService.Get("OutputPath", "");
            OutputFileNamePattern = windowsSettingsService.Get("OutputFileNamePattern", "{name}");
            IsEnableOriginFileName = windowsSettingsService.Get("IsEnableOriginFileName", true);
            IsEnableIndex = windowsSettingsService.Get("IsEnableIndex", false);
            IsEnableTime = windowsSettingsService.Get("IsEnableTime", false);
            IsEnableQuality = windowsSettingsService.Get("IsEnableQuality", false);
        }

        public string OutputPath
        {
            get => _outputPath;
            set
            {
                windowsSettingsService.Set("OutputPath", value);
                SetValue(ref _outputPath, value);
            }
        }
        public ICommand SelectOutputFolderCommand { get; private set; }

        public string OutputFileNamePattern
        {
            get => _outputFileNamePattern;
            set
            {
                windowsSettingsService.Set("OutputFileNamePattern", value);
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
            // 如果还没初始化完成就不处理
            // 因为初始化的时候会先读取pattern再读取多选框状态
            // 读取多选框状态会触发修改 pattern
            if (!initReady) { return; }
            // 增加
            if (value)
            {
                OutputFileNamePattern = @$"{OutputFileNamePattern}{GetDash()}{PlaceHolder}";
            }
            // 减少
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
                windowsSettingsService.Set("IsEnableOriginFileName", value);
                ProcessOutputFileNamePattern(value, "{name}");
                SetValue(ref _isEnableOriginFileName, value);
            }
        }
        public bool IsEnableIndex
        {
            get => _isEnableIndex;
            set
            {
                windowsSettingsService.Set("IsEnableIndex", value);
                ProcessOutputFileNamePattern(value, "{index}");
                SetValue(ref _isEnableIndex, value);
            }
        }
        public bool IsEnableTime
        {
            get => _isEnableTime;
            set
            {
                windowsSettingsService.Set("IsEnableTime", value);
                ProcessOutputFileNamePattern(value, "{time}");
                SetValue(ref _isEnableTime, value);
            }
        }
        public bool IsEnableQuality
        {
            get => _isEnableQuality;
            set
            {
                windowsSettingsService.Set("IsEnableQuality", value);
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

        public FlowDocument FullConsoleFlowDocument
        {
            get => _fullConsoleContentFlowDocument;
            private set
            {
                SetValue(ref _fullConsoleContentFlowDocument, value);
            }
        }
        public FlowDocument LiteConsoleFlowDocument
        {
            get => _liteConsoleContentFlowDocument;
            private set
            {
                SetValue(ref _liteConsoleContentFlowDocument, value);
            }
        }

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

        private void Append2FullConsoleDocument(string message, ConsoleMessageTypeEnum type,  bool liteAlso = false)
        {
            lock (FullConsoleFlowDocument)
            {
                FullConsoleContent = $"{FullConsoleContent}\n\n{DateTime.Now} {message}";
            }
            if (liteAlso)
            {
                Append2LiteConsole(message);
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
        public DelegateCommand RevertSelectedCommand { get; }
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
            InitParameters();
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
            RevertSelectedCommand = new DelegateCommand(() =>
            {
                for (int i = 0; i < InputFileList.Count; i++)
                {
                    InputFileList[i].Chosen = !InputFileList[i].Chosen;
                }
            });
            DeleteChosenCommand = new DelegateCommand(() => 
            {
                for (int i = InputFileList.Count - 1; i >= 0; i--)
                {
                    if (InputFileList[i].Chosen == true)
                    {
                        InputFileList.Remove(InputFileList[i]);
                    }
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

            initReady = true;
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
            // 设置编码器参数
            SelectedFormatModel.Encoder = SetupEncoder(Quality);

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
                    Append2FullConsole($"{item.DisplayName} 此前已被处理，跳过", true);
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
        /// <param name="fileItem"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="OperationCanceledException"></exception>
        private async Task<(bool, bool, long)> SingleProcessAsync(List<string> list, FileModel fileItem, CancellationToken token = default)
        {
            var current_quality = Quality;
            long disk_cost = 0;
            var originalExt = Path.GetExtension(fileItem.DisplayName);
            if (originalExt != null && list.Contains(originalExt) == false)
            {
                Append2FullConsole($"{fileItem.DisplayName} 扩展名不在允许读取的格式中，跳过");
                fileItem.Processed = true;
                ProcessedCount++;
                return (true, false, disk_cost);
            }
            var imageInfo = await Image.IdentifyAsync(fileItem.Path);
            var originWidth = imageInfo.Width;
            var originHeigth = imageInfo.Height;
            // 限制适应竖图
            int inputResUpperLimitWidth = (int)InputResUpperLimitWidth;
            int inputResUpperLimitHeight = (int)InputResUpperLimitHeight;
            int inputResLowerLimitWidth = (int)InputResLowerLimitWidth;
            int inputResLowerLimitHeight = (int)InputResLowerLimitHeight;
            if (IsInputResLimitationAdaptPortraitImage && (originHeigth > originWidth))
            {
                inputResUpperLimitWidth = (int)InputResUpperLimitHeight;
                inputResUpperLimitHeight = (int)InputResUpperLimitWidth;
                inputResLowerLimitWidth = (int)InputResLowerLimitHeight;
                inputResLowerLimitHeight = (int)InputResLowerLimitWidth;
            }
            // 跳过不在分辨率限制内的
            if (IsEnableInputResUpperLimit && (originWidth > inputResUpperLimitWidth || originHeigth > inputResUpperLimitHeight))
            {
                Append2FullConsole($"{fileItem} 大小（{originWidth} × {originHeigth}）超出上限，跳过");
                fileItem.Processed = true;
                ProcessedCount++;
                return (true, false, disk_cost);
            }
            if (IsEnableInputResLowerLimit && (originWidth < inputResLowerLimitWidth || originHeigth < inputResLowerLimitHeight))
            {
                Append2FullConsole($"{fileItem} 大小（{originWidth} × {originHeigth}）低于下限，跳过");
                fileItem.Processed = true;
                ProcessedCount++;
                return (true, false, disk_cost);
            }
            var start_time = DateTime.Now;
            var temp_filename = Path.Combine(OutputPath, $"temp{SelectedFormatModel.DisplayName}");
            var final_filename = Path.Combine(OutputPath, $"{RenameResult(Path.GetFileNameWithoutExtension(fileItem.DisplayName), current_quality)}{SelectedFormatModel.DisplayName}");

            var existence = File.Exists(final_filename);
            if (existence && IsCheckDontCoverExist)
            {
                fileItem.ResizedPath = $"文件已存在：{final_filename}";
                Append2FullConsole($"{final_filename} 已存在未覆盖，跳过处理", true);
                ProcessedCount++;
                fileItem.Processed = true;
                return (true, false, disk_cost);
            }

            using (Image originImage = await Image.LoadAsync(fileItem.Path))
            {
                Append2FullConsole($"{fileItem.DisplayName} 开始处理");
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    if (IsFileSizeFirstMode)
                    {
                        try
                        {

                            // 先尝试原尺寸压缩，如果满足要求就不用缩小了
                            if (token.IsCancellationRequested) throw new OperationCanceledException(token);
                            var st1 = DateTime.Now;
                            await originImage.SaveAsync(memoryStream, SelectedFormatModel.Encoder, token);
                            Append2FullConsole($"SaveAsync 耗时 {(DateTime.Now - st1).TotalSeconds} 秒");

                            long fileSizeInBytes = memoryStream.Length;
                            //disk_cost += fileSizeInBytes;
                            //total_disk_cost += disk_cost;
                            Append2FullConsole($"{fileItem.DisplayName} 分辨率未改变 ({originWidth}×{originHeigth})，直接保存的大小：{Math.Round((double)fileSizeInBytes / 1024, 3)} KiB");
                            // 直接转换的文件大小小于下限
                            if (fileSizeInBytes < TargetSizeUpperLimit * 1024)
                            {
                                fileItem.Processed = true;
                                if (fileSizeInBytes < TargetSizeLowerLimit * 1024 && IsDeleteSmallerThanTarget)
                                {
                                    //File.Delete(temp_filename);
                                    Append2FullConsole($"{fileItem.DisplayName} 直接转换的结果被删除，因为小于文件大小区间。耗时：{(DateTime.Now - start_time).TotalSeconds} 秒，磁盘写入 {Math.Round((double)disk_cost / 1024, 3)} KiB");
                                    fileItem.Processed = true;
                                    ProcessedCount++;
                                    return (true, false, disk_cost);
                                }
                                else
                                {
                                    final_filename = Path.Combine(OutputPath, $"{RenameResult(Path.GetFileNameWithoutExtension(fileItem.DisplayName), current_quality)}{SelectedFormatModel.DisplayName}");
                                    existence = File.Exists(final_filename);
                                    if (File.Exists(final_filename) && IsCheckDontCoverExist)
                                    {
                                        fileItem.ResizedPath = $"文件已存在：{final_filename}";
                                        Append2FullConsole($"{final_filename} 已存在未覆盖，耗时：{(DateTime.Now - start_time).TotalSeconds} 秒，磁盘写入 {Math.Round((double)disk_cost / 1024, 3)} KiB", true);
                                        ProcessedCount++;
                                        return (false, false, disk_cost);
                                    }
                                    else
                                    {
                                        await File.WriteAllBytesAsync(final_filename, memoryStream.ToArray(), token);
                                        disk_cost += memoryStream.Length;
                                        fileItem.ResizedPath = final_filename;
                                        Append2FullConsole($"{fileItem.DisplayName} 的结果已{(existence ? "覆盖" : "保存")}至 {final_filename}，耗时：{(DateTime.Now - start_time).TotalSeconds} 秒，磁盘写入 {Math.Round((double)disk_cost / 1024, 3)} KiB", true);
                                        fileItem.Processed = true;
                                        ProcessedCount++;
                                        return (true, true, disk_cost);
                                    }
                                }
                            }
                            else
                            {
                                var temp_encoder = SelectedFormatModel.Encoder;
                                int resized_width, resized_height = 0;
                                string width_percentage, height_percentage, pixel_percentage = "";
                                // 循环减小质量
                                while (true)
                                {
                                    (resized_width, resized_height, _) = await BinarySearchShrink(fileItem.DisplayName, originImage, temp_encoder, memoryStream, token);
                                    // 重新读取一下实际处理后的边长
                                    memoryStream.Position = 0;
                                    imageInfo = await Image.IdentifyAsync(memoryStream);
                                    resized_width = imageInfo.Width;
                                    resized_height = imageInfo.Height;
                                    fileSizeInBytes = memoryStream.Length;
                                    width_percentage = ((resized_width / (double)originImage.Width) * 100).ToString("f2");
                                    height_percentage = ((resized_height / (double)originImage.Height) * 100).ToString("f2");
                                    pixel_percentage = ((resized_width * resized_height) / (double)(originImage.Width * originImage.Height) * 100).ToString("f2");


                                    if (!IsEnableResLowerLimit)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        // 开启了分辨率最低限制
                                        (bool isUnder, int true_width_limit, int true_height_limit) = IsUnderResolutionLimit(resized_width, resized_height);
                                        if (isUnder)
                                        {
                                            if (FileSizeFirst_Delete)
                                            {
                                                //File.Delete(temp_filename);
                                                Append2FullConsole($"{fileItem.DisplayName} 转换的结果被删除，{resized_width}×{resized_height}, {width_percentage}% 宽 {height_percentage}% 高, {pixel_percentage}% 像素, 耗时：{(DateTime.Now - start_time).TotalSeconds} 秒，磁盘写入 {Math.Round((double)disk_cost / 1024, 3)} KiB");
                                                fileItem.Processed = true;
                                                ProcessedCount++;
                                                return (true, false, disk_cost);
                                            }
                                            else if (FileSizeFirst_IgnoreSizeLimit)
                                            {
                                                Image? image_processed = default;
                                                // 手动表示要自己计算分辨率
                                                if (SelectedResizeModeModel.Mode == SixLabors.ImageSharp.Processing.ResizeMode.Manual)
                                                {
                                                    // 选取较大的缩放比例
                                                    // 如果宽的缩放比例更大，也就是说宽距离限制更远，也就是宽相对更小
                                                    // 那么就以宽对齐，高传入0
                                                    double width_scale_factor = true_width_limit / (double)resized_width;
                                                    double height_scale_factor = true_height_limit / (double)resized_height;
                                                    if (width_scale_factor > height_scale_factor)
                                                    {
                                                        true_height_limit = 0;
                                                    }
                                                    else
                                                    {
                                                        true_width_limit = 0;
                                                    }
                                                    image_processed = ProcessingExtensions.Clone(originImage, x => x.Resize(
                                                        true_width_limit,
                                                        true_height_limit,
                                                        SelectedResamplerModel.Resampler
                                                        ));
                                                }
                                                else
                                                {
                                                    image_processed = ProcessingExtensions.Clone(originImage, x => x.Resize(new ResizeOptions()
                                                    {
                                                        Size = new SixLabors.ImageSharp.Size(true_width_limit, true_height_limit),
                                                        Mode = SelectedResizeModeModel.Mode,
                                                        Sampler = SelectedResamplerModel.Resampler,
                                                    }));
                                                }
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

                                                Append2FullConsole($"{fileItem.DisplayName} 的分辨率重新设置为 {resized_width}×{resized_height}, {width_percentage}% 宽 {height_percentage}% 高, {pixel_percentage}% 像素, {Math.Round((double)fileSizeInBytes / 1024, 3)} KiB");
                                                //disk_cost += file_size;
                                                //total_disk_cost += fileSizeInBytes;
                                            }
                                            else if (FileSizeFirst_ReduceQuality)
                                            {
                                                current_quality -= 5;
                                                temp_encoder = SetupEncoder(current_quality);
                                                continue;
                                            }
                                            // 不做处理
                                        }
                                        break;
                                    }
                                }
                                final_filename = Path.Combine(OutputPath, $"{RenameResult(Path.GetFileNameWithoutExtension(fileItem.DisplayName), current_quality)}{SelectedFormatModel.DisplayName}");
                                existence = File.Exists(final_filename);
                                if (File.Exists(final_filename) && IsCheckDontCoverExist)
                                {
                                    fileItem.ResizedPath = $"文件已存在：{final_filename}";
                                    Append2FullConsole($"{final_filename} 已存在未覆盖，{resized_width}×{resized_height}, {width_percentage}% 宽 {height_percentage}% 高, {pixel_percentage}% 像素, 耗时：{(DateTime.Now - start_time).TotalSeconds} 秒，磁盘写入 {Math.Round((double)disk_cost / 1024, 3)} KiB", true);
                                    ProcessedCount++;
                                    return (false, false, disk_cost);
                                }
                                else
                                {
                                    // 未开启分辨率最低限制
                                    await File.WriteAllBytesAsync(final_filename, memoryStream.ToArray(), token);
                                    if (token.IsCancellationRequested) throw new OperationCanceledException(token);
                                    disk_cost += memoryStream.Length;
                                    fileItem.ResizedPath = final_filename;
                                    Append2FullConsole($"{fileItem.DisplayName} 转换的结果已{(existence ? "覆盖" : "保存")}至 {final_filename}，{resized_width}×{resized_height}, {width_percentage}% 宽 {height_percentage}% 高, {pixel_percentage}% 像素, 耗时：{(DateTime.Now - start_time).TotalSeconds} 秒，磁盘写入 {Math.Round((double)disk_cost / 1024, 3)} KiB", true);
                                    fileItem.Processed = true;
                                    ProcessedCount++;
                                    return (true, true, disk_cost);
                                }                                
                            }
                        }
                        catch (Exception e)
                        {
                            Append2FullConsole($"发生错误：{e}");
                            return (false, false, 0);

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
                // 转换失败：
                // 77235172.png
                // illust_76954860_20191002_022429.png
                // 75323963.png
                // 84889828.png
                // 73960040.png
                resized_width = (int)(originImage.Width * current_multiple);
                resized_height = (int)(originImage.Height * current_multiple);
                if (resized_width <= 0 || resized_height <= 0)
                { 
                    throw new Exception();
                }
                Image image_processed;
                if (SelectedResizeModeModel.Mode == SixLabors.ImageSharp.Processing.ResizeMode.Manual)
                {
                    image_processed = ProcessingExtensions.Clone(originImage, x => x.Resize(new ResizeOptions()
                    {
                        Size = new SixLabors.ImageSharp.Size(resized_width, resized_height),
                        Mode = SelectedResizeModeModel.Mode,
                        Sampler = SelectedResamplerModel.Resampler,
                        TargetRectangle = new Rectangle(0, 0, resized_width, resized_height)
                    }));
                }
                else
                {
                    image_processed = ProcessingExtensions.Clone(originImage, x => x.Resize(new ResizeOptions()
                    {
                        Size = new SixLabors.ImageSharp.Size(resized_width, resized_height),
                        Mode = SelectedResizeModeModel.Mode,
                        Sampler = SelectedResamplerModel.Resampler,
                    }));
                }
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
                    return (true, (int)TargetResolutionWidthLowerLimit, (int)TargetResolutionHeightLowerLimit);
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
                        return (true, (int)TargetResolutionWidthLowerLimit, (int)TargetResolutionHeightLowerLimit);
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
                        return (true, (int)TargetResolutionHeightLowerLimit, (int)TargetResolutionWidthLowerLimit);
                    }
                    else
                    {
                        return (false, 0, 0);
                    }
                }
            }
        }

        private string RenameResult(string originName, int quality)
        {
            string finalName = OutputFileNamePattern;
            finalName = finalName.Replace("{time}", DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss"));
            finalName = finalName.Replace("{name}", originName);
            finalName = finalName.Replace("{quality}", quality.ToString());

            return finalName;
        }

        private ImageEncoder SetupEncoder(int quality)
        {
            ImageEncoder encoder = null;
            if (SelectedFormatModel.DisplayName == ".webp")
            {
                encoder = new WebpEncoder()
                {
                    FileFormat = (IsWebpLossyMode) ? WebpFileFormatType.Lossy : WebpFileFormatType.Lossless,
                    Quality = quality,
                    Method = (WebpEncodingMethod)EncodingMethod
                };
            }
            else if (SelectedFormatModel.DisplayName == ".jpg")
            {
                encoder = new JpegEncoder()
                {
                    Quality = quality
                };
            }
            else if (SelectedFormatModel.DisplayName == ".png")
            {
                encoder = new PngEncoder()
                {
                    CompressionLevel = (PngCompressionLevel)quality
                };
            }
            else if (SelectedFormatModel.DisplayName == ".tif")
            {
                encoder = new TiffEncoder()
                {
                    CompressionLevel = (DeflateCompressionLevel)quality
                };
            }
            else 
            {
                throw new Exception("no match encoder, this should't happen.");
            }
            return encoder;
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
                else
                {
                    Append2FullConsole($"{path} 未添加。");
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
