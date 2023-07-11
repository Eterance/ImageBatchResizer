using ImageBatchResizer.ViewModels;
using System;
using System.Collections.Generic;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ImageBatchResizer.ViewModels
{
    public class CoreViewModel : ViewModelBase
    {

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
