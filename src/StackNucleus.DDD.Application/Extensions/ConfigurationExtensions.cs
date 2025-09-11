using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace StackNucleus.DDD.Application.Extensions;

/// <summary>
/// Provides extension methods for binding configuration settings to strongly-typed settings classes.
/// </summary>
public static class ConfigurationExtensions
{
    /// <summary>
    /// Binds a configuration section to a strongly-typed settings class and registers it as a singleton service.
    /// </summary>
    /// <typeparam name="TSettings">The type of the settings class.</typeparam>
    /// <param name="services">The service collection to which the settings will be added.</param>
    /// <param name="configuration">The configuration object containing the settings data.</param>
    /// <param name="settings">The output parameter that will contain the bound settings object.</param>
    /// <param name="sectionName">The name of the configuration section. If null, uses the type name of the settings class.</param>
    /// <returns>The updated service collection with the bound settings registered as a singleton.</returns>
    public static IServiceCollection AddBoundSettingsWithSectionAsEntityName<TSettings>(
        this IServiceCollection services,
        IConfiguration configuration,
        out TSettings settings,
        string? sectionName = null)
        where TSettings : class, new()
    {
        if (sectionName is null)
        {
            sectionName = typeof(TSettings).Name;
        }

        settings = new TSettings();
        configuration.Bind(sectionName, settings);
        services.AddSingleton(settings);
        return services;
    }

    /// <summary>
    /// Binds a configuration section to a strongly-typed settings class and registers it as a singleton service.
    /// This overload does not expose the bound settings object.
    /// </summary>
    /// <typeparam name="TSettings">The type of the settings class.</typeparam>
    /// <param name="services">The service collection to which the settings will be added.</param>
    /// <param name="configuration">The configuration object containing the settings data.</param>
    /// <param name="sectionName">The name of the configuration section. If null, uses the type name of the settings class.</param>
    /// <returns>The updated service collection with the bound settings registered as a singleton.</returns>
    public static IServiceCollection AddBoundSettingsWithSectionAsEntityName<TSettings>(
        this IServiceCollection services,
        IConfiguration configuration,
        string? sectionName = null)
        where TSettings : class, new()
    {
        return services.AddBoundSettingsWithSectionAsEntityName<TSettings>(
            configuration,
            out var _,
            sectionName);
    }
}
