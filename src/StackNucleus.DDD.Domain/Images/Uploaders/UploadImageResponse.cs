namespace StackNucleus.DDD.Domain.Images.Uploaders;

/// <summary>
/// Represents the response returned after a successful image upload.
/// </summary>
public sealed record UploadImageResponse
{
    /// <summary>
    /// Collection of generated image URLs with their corresponding sizes.
    /// </summary>
    public ImageUrlToSize[] Images { get; set; }
}
