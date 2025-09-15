using StackNucleus.DDD.Domain.ResultModels;

namespace StackNucleus.DDD.Domain.Images.Uploaders;

/// <summary>
/// Defines the contract for uploading images to a storage system with optional compression.
/// </summary>
public interface IImageUploader
{
    /// <summary>
    /// Uploads an image to a specified folder with optional compression applied.
    /// </summary>
    /// <param name="request">The request for the image upload process.</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The response consisting of result and image urls.</returns>
    Task<ValueOrNull<UploadImageResponse>> UploadImageAsync(
        UploadImageRequest request,
        CancellationToken cancellationToken = default);
}