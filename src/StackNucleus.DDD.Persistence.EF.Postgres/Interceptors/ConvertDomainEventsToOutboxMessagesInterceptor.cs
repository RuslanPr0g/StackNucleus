using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using StackNucleus.DDD.Domain;
using StackNucleus.DDD.Domain.Outbox;

namespace StackNucleus.DDD.Persistence.EF.Postgres.Interceptors;

/// <summary>
/// Intercepts the `SaveChanges` operation of the DbContext to convert domain events into outbox messages.
/// This is typically used in the context of event-driven architectures, where domain events are captured
/// and stored for eventual processing or communication (e.g., event streaming, message queues).
/// </summary>
public sealed class ConvertDomainEventsToOutboxMessagesInterceptor : SaveChangesInterceptor
{
    /// <inheritdoc cref="ConvertDomainEventsToOutboxMessagesInterceptor" />
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        DbContext? context = eventData.Context;

        if (context is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        var messages = context.ChangeTracker
            .Entries<IAggregateRoot>()
            .Select(x => x.Entity)
            .SelectMany(root =>
            {
                var domainEvents = root.DomainEvents;
                root.ClearEvents();

                foreach (var @event in domainEvents)
                {
                    @event.CorrelationId = @event.CorrelationId == Guid.Empty ?
                        Guid.NewGuid() : @event.CorrelationId;
                }

                return domainEvents;
            })
            .Select(x => new OutboxMessage()
            {
                Id = Guid.NewGuid(),
                OccuredOnUtc = DateTime.UtcNow,
                Type = x.GetType().FullName ?? x.GetType().Name,
                Content = JsonConvert.SerializeObject(x, new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All
                })
            })
            .ToList();

        context.Set<OutboxMessage>().AddRange(messages);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}