using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace StackNucleus.DDD.Jobs.Quartz.Extensions;

/// <summary>
/// Provides extension methods for configuring Quartz jobs within the dependency injection container.
/// </summary>
public static class DIExtensions
{
    /// <summary>
    /// Adds configurable Quartz jobs to the service collection.
    /// </summary>
    /// <param name="services">The service collection to which the jobs will be added.</param>
    /// <param name="jobConfigurations">The collection of job configurations to be registered.</param>
    /// <returns>The updated service collection with the Quartz jobs added.</returns>
    public static IServiceCollection AddConfigurableJobs(
        this IServiceCollection services,
        IEnumerable<JobConfiguration> jobConfigurations)
    {
        services.AddQuartz(conf =>
        {
            foreach (var job in jobConfigurations)
            {
                conf.AddJob(job.Type, new JobKey(job.Key)).AddTrigger(trigger =>
                {
                    var configurator = trigger.ForJob(job.Key);

                    if (job.RepeatInterval > 0)
                    {
                        configurator.WithSimpleSchedule(
                            schedule =>
                            {
                                var scheduleBuilder = schedule
                                    .WithIntervalInSeconds(job.RepeatInterval);

                                if (job.RepeatForever)
                                {
                                    scheduleBuilder.RepeatForever();
                                }
                            });
                    }
                });
            }
        });

        services.AddQuartzHostedService();

        return services;
    }
}
