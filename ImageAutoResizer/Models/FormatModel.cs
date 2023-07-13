using SixLabors.ImageSharp.Formats;

namespace ImageBatchResizer.Models
{
    public class FormatModel
    {
        public string Name { get; private set; }
        public ImageEncoder Encoder { get; set; }
        public FormatModel(string name, ImageEncoder encoder)
        {
            Name = name;
            Encoder = encoder;
        }
    }
}
