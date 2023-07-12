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
        private bool _isResLimitationAdaptPortraitImage = true;
        private bool _isAcceptBmp = true;
        private bool _isAcceptJpeg = true;
        private bool _isAcceptTga = true;
        private bool _isAcceptPng = true;
        private bool _isAcceptTiff = true;
        private bool _isAcceptWebP = true;

        public bool IsEnableInputResUpperLimit
        {
            get => _isEnableInputResUpperLimit;
            set
            {
                SetValue(ref _isEnableInputResUpperLimit, value);
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

        public bool IsResLimitationAdaptPortraitImage
        {
            get => _isResLimitationAdaptPortraitImage;
            set
            {
                SetValue(ref _isResLimitationAdaptPortraitImage, value);
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

        public bool IsEnableParametersPanel
        {
            get => _isEnableParametersPanel;
            set
            {
                SetValue(ref _isEnableParametersPanel, value);
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
                new("Crop", ResizeMode.Crop),
                new("Max", ResizeMode.Max),
            };
            SelectedResizeModeModel = new("Min", ResizeMode.Min);
            ResizeModeList.Add(SelectedResizeModeModel);
            ResizeModeList.Add(new("Pad", ResizeMode.Pad));
            ResizeModeList.Add(new("Stretch", ResizeMode.Stretch));
        }

        public CoreViewModel()
        {
            InitFormatList();
            InitResamplerList();
            InitResizeModeList();
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
