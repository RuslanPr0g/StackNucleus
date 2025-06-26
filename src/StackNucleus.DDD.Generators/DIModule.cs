using Microsoft.Extensions.DependencyInjection;
using StackNucleus.DDD.Domain.Generators;

namespace StackNucleus.DDD.Generators;

/// <summary>
/// Registers all the generators defined in the assemly to be used in DI.
/// </summary>
public static class DIModule
{
    /// <inheritdoc cref="DIModule" />
    public static IServiceCollection AddNucleusGenerators(this IServiceCollection services)
    {
        services.AddSingleton<IIdGenerator, IdGenerator>();
        return services;
    }
}