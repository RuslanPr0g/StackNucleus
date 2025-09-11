using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;
using StackNucleus.DDD.Domain.Outbox;

namespace StackNucleus.DDD.Outbox.Jobs;

/// <summary>
/// Base class for cleaning outbox messages that have been processed.
/// </summary>
/// <typeparam name="TContext">The type of the DbContext used for data access.</typeparam>
public abstract class BaseCleanOutboxJob<TContext> : IJob
    where TContext : DbContext
{
    /// <summary>
    /// Gets the DbContext instance used for querying the database.
    /// </summary>
    protected abstract TContext Context { get; init; }

    /// <summary>
    /// Executes the job of cleaning outbox messages that have been processed.
    /// </summary>
    /// <param name="context">The job execution context.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task Execute(IJobExecutionContext context)
    {
        var messages = await Context
            .Set<OutboxMessage>()
            .Where(m => m.ProcessedOnUtc != null)
            .Take(5)
            .ToListAsync();

        foreach (var message in messages)
        {
            try
            {
                Context.Remove(message);
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
