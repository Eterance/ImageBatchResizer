using SixLabors.ImageSharp.Formats;

namespace ImageBatchResizer.Models
{
    public class FormatModel: IModel
    {
        public string DisplayName { get; private set; }
        public ImageEncoder Encoder { get; set; }
        public FormatModel(string name, ImageEncoder encoder)
        {
            DisplayName = name;
            Encoder = encoder;
        }
    }
}
