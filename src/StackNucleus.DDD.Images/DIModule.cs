using Microsoft.Extensions.DependencyInjection;
using StackNucleus.DDD.Domain.Images;
using StackNucleus.DDD.Images.Compressors;

namespace StackNucleus.DDD.Images;

/// <summary>
/// Provides extension methods for adding shared image services to the dependency injection container.
/// </summary>
public static class DIModule
{
    /// <summary>
    /// Registers the image compressor service in the service collection.
    /// </summary>
    /// <param name="services">The service collection to which the image compressor will be added.</param>
    /// <returns>The updated service collection with the image compressor service registered.</returns>
    public static IServiceCollection AddNucleusImages(this IServiceCollection services)
    {
        services.AddScoped<IImageCompressor, DefaultImageCompressor>();
        return services;
    }
}
