namespace StackNucleus.DDD.Domain.Images;

/// <summary>
/// Maps a generated image URL to its corresponding size.
/// </summary>
public sealed record ImageUrlToSize
{
    /// <summary>
    /// Direct URL of the uploaded image in blob storage or CDN.
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// The size category of the image (e.g., small, medium, large).
    /// </summary>
    public ImageSize Size { get; set; }
}
