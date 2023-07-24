using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing.Processors.Transforms;

namespace ImageBatchResizer.Models
{
    public class ResamplerModel: IModel
    {
        public string DisplayName { get; private set; }
        public IResampler Resampler { get; private set; }
        public ResamplerModel(string name, IResampler resampler)
        {
            DisplayName = name;
            Resampler = resampler;
        }
    }
}
