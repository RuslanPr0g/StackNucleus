using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using StackNucleus.DDD.Domain.Images;
using StackNucleus.DDD.Domain.Images.Compressors;

namespace StackNucleus.DDD.Images.Compressors;

/// <summary>
/// Provides the default implementation of the image compression process.
/// </summary>
public sealed class DefaultImageCompressor : IImageCompressor
{
    /// <summary>
    /// Compresses the given image preview asynchronously according to the specified compression settings.
    /// </summary>
    /// <param name="imagePreview">The image preview in byte array format.</param>
    /// <param name="settings">The compression settings to apply.</param>
    /// <returns>A byte array representing the compressed image.</returns>
    public ValueTask<byte[]> CompressAsync(byte[] imagePreview, CompressionSettings settings)
    {
        using var image = Image.Load(imagePreview);
        int maxWidth = settings.MaxWidth;
        int maxHeight = maxWidth * settings.FormatHeight / settings.FormatWidth;

        if (image.Width > maxWidth || image.Height > maxHeight)
        {
            image.Mutate(x => x.Resize(new ResizeOptions
            {
                Mode = ResizeMode.Max,
                Size = new Size(maxWidth, maxHeight)
            }));
        }

        using var ms = new MemoryStream();
        image.Save(ms, new JpegEncoder { Quality = 75 });
        return ValueTask.FromResult(ms.ToArray());
    }
}
