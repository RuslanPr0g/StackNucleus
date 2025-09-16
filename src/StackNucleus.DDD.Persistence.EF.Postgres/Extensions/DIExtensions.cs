using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackNucleus.DDD.Persistence.EF.Postgres.Interceptors;

namespace StackNucleus.DDD.Persistence.EF.Postgres.Extensions;

/// <summary>
/// Provides extension methods for setting up and configuring the persistence context (DbContext) with dependency injection.
/// These methods configure Entity Framework Core to work with a PostgreSQL database and handle migrations automatically.
/// </summary>
public static class DIExtensions
{
    /// <summary>
    /// Adds and configures a <see cref="DbContext"/> for persistence in a PostgreSQL database with automatic migrations.
    /// This method also sets up an interceptor to convert domain events into outbox messages.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the DbContext being configured. This should be a subclass of <see cref="DbContext"/>.
    /// </typeparam>
    /// <param name="services">
    /// The <see cref="IServiceCollection"/> to add the DbContext and related services to.
    /// </param>
    /// <param name="configuration">
    /// The application's configuration that holds the connection string and other settings.
    /// </param>
    /// <param name="migrationsAssemblyName">
    /// The assembly containing the migrations for this DbContext. This is required for applying migrations.
    /// </param>
    /// <param name="connectionStringName">
    /// The name of the connection string to retrieve from the configuration. Defaults to "postgres".
    /// </param>
    /// <param name="migrationsHistoryTableSchemaName">
    /// The schema name for the migrations history table. Defaults to "public".
    /// </param>
    /// <param name="migrateDatabase">
    /// Whether to migrate the database at startup. Default is true.
    /// </param>
    /// <returns>
    /// The <see cref="IServiceCollection"/> to allow for method chaining.
    /// </returns>
    /// <remarks>
    /// This method will register the <see cref="DbContext"/> with the DI container, configure it to use PostgreSQL,
    /// and automatically apply any pending migrations on application startup. It also adds the <see cref="ConvertDomainEventsToOutboxMessagesInterceptor"/>
    /// for handling domain events.
    /// </remarks>
    public static IServiceCollection AddConfigurablePersistenceContext<TContext>(
        this IServiceCollection services,
        IConfiguration configuration,
        string migrationsAssemblyName,
        string connectionStringName = "postgres",
        string migrationsHistoryTableSchemaName = "public",
        bool migrateDatabase = true)
        where TContext : DbContext
    {
        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();

        var mainConnectionString = configuration.GetConnectionString(connectionStringName);

        services.AddDbContext<TContext>((sp, builder) =>
        {
            var interceptor = sp.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>();

            builder.UseNpgsql(
                    mainConnectionString,
                    b =>
                    {
                        b.MigrationsAssembly(migrationsAssemblyName)
                            .MigrationsHistoryTable("__EFMigrationsHistory", migrationsHistoryTableSchemaName);
                    })
                .AddInterceptors(interceptor!);
        });

        if (migrateDatabase)
        {
            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<TContext>();
                db.Database.Migrate();
            }
        }

        return services;
    }
}
