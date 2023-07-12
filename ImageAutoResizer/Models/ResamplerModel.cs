using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing.Processors.Transforms;

namespace ImageBatchResizer.Models
{
    public class ResamplerModel
    {
        public string Name { get; private set; }
        public IResampler Resampler { get; private set; }
        public ResamplerModel(string name, IResampler resampler)
        {
            Name = name;
            Resampler = resampler;
        }
    }
}
