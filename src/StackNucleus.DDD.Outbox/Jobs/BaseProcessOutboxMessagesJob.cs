using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;
using StackNucleus.DDD.Domain;
using StackNucleus.DDD.Domain.EventPublishers;
using StackNucleus.DDD.Domain.Outbox;
using System.Reflection;

namespace StackNucleus.DDD.Outbox.Jobs;

/// <summary>
/// Base class for processing outbox messages and publishing domain events.
/// </summary>
/// <typeparam name="TContext">The type of the DbContext used for data access.</typeparam>
/// <typeparam name="TAssembly">The type of the assembly that contains the event types.</typeparam>
public abstract class BaseProcessOutboxMessagesJob<TContext, TAssembly> : IJob
    where TContext : DbContext
    where TAssembly : Assembly
{
    /// <summary>
    /// Gets the DbContext instance used for querying the database.
    /// </summary>
    protected abstract TContext Context { get; init; }

    /// <summary>
    /// Gets the list of assemblies containing event types.
    /// </summary>
    protected abstract IReadOnlyList<TAssembly> MessagesAssembly { get; init; }

    private readonly IEventPublisher _publisher;

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseProcessOutboxMessagesJob{TContext, TAssembly}"/> class.
    /// </summary>
    /// <param name="publisher">The event publisher used to publish domain events.</param>
    public BaseProcessOutboxMessagesJob(IEventPublisher publisher)
    {
        _publisher = publisher;
    }

    /// <summary>
    /// Executes the job of processing and publishing outbox messages.
    /// </summary>
    /// <param name="context">The job execution context.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task Execute(IJobExecutionContext context)
    {
        var messages = await Context
            .Set<OutboxMessage>()
            .Where(m => m.ProcessedOnUtc == null)
            .Take(20)
            .ToListAsync();

        foreach (var message in messages)
        {
            try
            {
                var messageType = MessagesAssembly
                    .Select(asm => asm.GetType(message.Type))
                    .FirstOrDefault(t => t != null);

                if (messageType is null)
                {
                    message.Error = $"Type '{message.Type}' could not be resolved.";
                    continue;
                }

                var @event = JsonConvert.DeserializeObject(
                    message.Content,
                    messageType,
                    new JsonSerializerSettings()
                    {
                        TypeNameHandling = TypeNameHandling.All,
                    });

                if (@event is null || @event is not IDomainEvent domainEvent)
                {
                    continue;
                }

                await _publisher.Publish(domainEvent);

                message.ProcessedOnUtc = DateTime.UtcNow;
                message.Error = string.Empty;
            }
            catch (Exception ex)
            {
                message.Error = JsonConvert.SerializeObject(ex);
                throw;
            }
        }

        if (messages.Count > 0)
        {
            await Context.SaveChangesAsync();
        }
    }
}
