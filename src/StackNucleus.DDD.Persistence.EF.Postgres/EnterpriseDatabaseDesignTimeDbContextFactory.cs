using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace StackNucleus.DDD.Persistence.EF.Postgres;

/// <summary>
/// A base class for creating a DbContext in a design-time environment, such as during migrations.
/// This class is intended to be inherited by concrete factories that provide specific logic for creating a DbContext.
/// </summary>
/// <typeparam name="TContext">
/// The type of the DbContext to be created. This should be a subclass of <see cref="DbContext"/>.
/// </typeparam>
public abstract class EnterpriseDatabaseDesignTimeDbContextFactory<TContext>
    : IDesignTimeDbContextFactory<TContext>
    where TContext : DbContext
{
    /// <summary>
    /// Gets or sets the name of the connection string to use when creating the DbContext.
    /// Default is "PostgresEF".
    /// </summary>
    public virtual string ConnectionStringName { get; set; } = "PostgresEF";

    /// <summary>
    /// Gets or sets the name of the appsettings file to load the connection string from.
    /// Default is "appsettings.json".
    /// </summary>
    public virtual string AppSettingsName { get; set; } = "appsettings.json";

    /// <summary>
    /// Creates a DbContext based on the provided <see cref="DbContextOptions{TContext}"/>.
    /// This method must be implemented by the subclass to provide specific configuration for the DbContext.
    /// </summary>
    /// <param name="options">
    /// The options to be used when creating the DbContext.
    /// </param>
    /// <returns>
    /// An instance of <typeparamref name="TContext"/>.
    /// </returns>
    public abstract TContext CreateDbContextBasedOnOptions(DbContextOptions<TContext> options);

    /// <summary>
    /// Creates a DbContext based on configuration settings, such as connection string and appsettings file.
    /// This is typically used during design-time tasks like migrations.
    /// </summary>
    /// <param name="args">
    /// Command-line arguments (not used in this implementation).
    /// </param>
    /// <returns>
    /// An instance of <typeparamref name="TContext"/>.
    /// </returns>
    public TContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(AppSettingsName)
                .Build();

        var connectionString = configuration.GetConnectionString(ConnectionStringName);

        var builder = new DbContextOptionsBuilder<TContext>();
        builder.UseNpgsql(connectionString);
        return CreateDbContextBasedOnOptions(builder.Options);
    }
}
