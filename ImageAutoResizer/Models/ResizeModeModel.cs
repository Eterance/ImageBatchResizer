using SixLabors.ImageSharp.Processing;

namespace ImageBatchResizer.Models
{
    public class ResizeModeModel
    {
        public string Name { get; private set; }
        public ResizeMode Mode { get; private set; }
        public ResizeModeModel(string name, ResizeMode mode)
        {
            Name = name;
            Mode = mode;
        }
    }
}
