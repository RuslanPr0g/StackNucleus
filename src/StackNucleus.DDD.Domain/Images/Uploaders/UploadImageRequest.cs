namespace StackNucleus.DDD.Domain.Images.Uploaders;

/// <summary>
/// Represents a request to upload an image to the media service.
/// </summary>
public sealed record UploadImageRequest
{
    /// <summary>
    /// Unique identifier of the file to be associated with the upload.
    /// </summary>
    public required Guid FileId { get; set; }

    /// <summary>
    /// Raw image data in bytes.
    /// </summary>
    public required byte[] ImageAsBytes { get; set; }

    /// <summary>
    /// Target sizes in which the image should be generated (e.g., small, medium, large).
    /// Defaults to <see cref="ImageSize.Large"/>.
    /// </summary>
    public ImageSize[] Sizes { get; set; } = [ImageSize.Large];

    /// <summary>
    /// File extension of the image (e.g., "jpg", "png").
    /// Defaults to "jpg".
    /// </summary>
    public string Extension { get; set; } = "jpg";

    /// <summary>
    /// Compression settings applied during image processing.
    /// Defaults to <see cref="CompressionSettings.Default"/>.
    /// </summary>
    public CompressionSettings CompressionSettings { get; set; } = CompressionSettings.Default;
}
