using Microsoft.Extensions.DependencyInjection;
using StackNucleus.DDD.Domain.EventPublishers;

namespace StackNucleus.DDD.Events.WolverineFx;

/// <summary>
/// Extension methods for registering event publishers in the dependency injection container.
/// </summary>
public static class DIModule
{
    /// <summary>
    /// Registers the <see cref="IEventPublisher"/> service in the dependency injection container.
    /// The <see cref="BaseEventPublisher"/> is used as the implementation.
    /// </summary>
    /// <param name="services">
    /// The <see cref="IServiceCollection"/> to which the event publisher is added.
    /// </param>
    /// <returns>
    /// The updated <see cref="IServiceCollection"/> with the event publisher registered.
    /// </returns>
    public static IServiceCollection AddEnterpriseEventPublishers(this IServiceCollection services)
    {
        services.AddScoped<IEventPublisher, BaseEventPublisher>();
        return services;
    }
}