using SixLabors.ImageSharp.Processing;

namespace ImageBatchResizer.Models
{
    public class ResizeModeModel: IModel
    {
        public string DisplayName { get; private set; }
        public ResizeMode Mode { get; private set; }
        public ResizeModeModel(string name, ResizeMode mode)
        {
            DisplayName = name;
            Mode = mode;
        }
    }
}
