using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;
using Wolverine;
using Wolverine.ErrorHandling;
using Wolverine.RabbitMQ;

namespace StackNucleus.DDD.Events.WolverineFx;

/// <summary>
/// Extension methods for configuring event handlers and message queues in the application.
/// </summary>
public static class DiExtensions
{
    /// <summary>
    /// Configures event handlers for message-driven communication via RabbitMQ. This method configures 
    /// the application to use Wolverine for handling events, automatically provisions the message queue,
    /// and applies specific policies and settings for handling messages.
    /// </summary>
    /// <param name="builder">
    /// The <see cref="IHostApplicationBuilder"/> to which the event handlers are added.
    /// </param>
    /// <param name="assemblies">
    /// An array of <see cref="Assembly"/> instances to scan for event handler types.
    /// </param>
    /// <param name="rabbitMqConnectionString">
    /// The connection string for RabbitMQ.
    /// </param>
    /// <param name="queueName">
    /// The name of the queue to bind with the exchange.
    /// </param>
    public static void AddConfigurableEventHandlers(
        this IHostApplicationBuilder builder,
        Assembly[] assemblies,
        string rabbitMqConnectionString,
        string queueName)
    {
        builder.UseWolverine(opts =>
        {
            opts.UseRabbitMq(rabbitMqConnectionString)
                .AutoProvision()
                .ConfigureListeners(
                    listener =>
                    {
                        listener.UseDurableInbox();
                    }
                )
                .DeclareExchange("h-common-exchange",
                    ex =>
                    {
                        ex.BindQueue(queueName, queueName);
                    });

            foreach (var assembly in assemblies)
            {
                opts.Discovery.IncludeAssembly(assembly);
            }

            opts.PublishAllMessages()
                .ToRabbitExchange("h-common-exchange")
                .UseDurableOutbox();

            opts.ListenToRabbitQueue(queueName, cfg =>
            {
                cfg.BindExchange("h-common-exchange", queueName);
            })
            .PreFetchCount(100)
            .ListenerCount(5)
            .UseDurableInbox();

            opts.Policies
                .LogMessageStarting(LogLevel.Information);

            opts.Policies.OnException<Exception>()
                .RetryTimes(3)
                .Then.MoveToErrorQueue();
        });
    }
}