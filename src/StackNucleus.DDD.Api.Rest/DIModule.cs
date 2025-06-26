using Microsoft.Extensions.DependencyInjection;
using StackNucleus.DDD.Events.WolverineFx;
using StackNucleus.DDD.Generators;

namespace StackNucleus.DDD.Api.Rest;

/// <summary>
/// Extension methods for configuring the REST API services in the dependency injection container.
/// </summary>
public static class DIModule
{
    /// <summary>
    /// Registers the necessary services for the REST API, including generators, event publishers, 
    /// and the authorized endpoint handler.
    /// </summary>
    /// <param name="services">
    /// The <see cref="IServiceCollection"/> to which the REST API services are added.
    /// </param>
    /// <returns>
    /// The updated <see cref="IServiceCollection"/> with the necessary services for the REST API.
    /// </returns>
    public static IServiceCollection AddNucleusRestApi(this IServiceCollection services)
    {
        services.AddNucleusGenerators();
        services.AddNucleusEventPublishers();

        services.AddSingleton<IAuthorizedEndpointHandler, AuthorizedEndpointHandler>();

        return services;
    }
}