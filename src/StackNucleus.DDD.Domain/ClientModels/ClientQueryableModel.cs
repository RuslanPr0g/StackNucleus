using System.Collections.Immutable;

namespace StackNucleus.DDD.Domain.ClientModels;

/// <summary>
/// Represents the parameters for a paged, sortable query.
/// Used to specify which subset of items to retrieve and the sorting behavior.
/// </summary>
public record ClientQueryableModel
{
    /// <summary>
    /// The zero-based index of the first item to retrieve.
    /// </summary>
    public required long StartIndex { get; set; }

    /// <summary>
    /// The number of items to retrieve for the current query.
    /// </summary>
    public required long ItemsCount { get; set; }

    /// <summary>
    /// The property name used to sort the query results.
    /// </summary>
    public required string SortProperty { get; set; }

    /// <summary>
    /// Indicates whether sorting is ascending (<c>true</c>) or descending (<c>false</c>).
    /// </summary>
    public required bool SortAsc { get; set; }
}

/// <summary>
/// Base class for query models that include rules for sortable properties.
/// Ensures only predefined properties are allowed for sorting.
/// </summary>
public abstract record ClientQueryableModelWithSortableRules : ClientQueryableModel
{
    /// <summary>
    /// The set of properties allowed for sorting. Must be defined in derived classes.
    /// </summary>
    protected abstract ImmutableHashSet<string> SortableProperties { get; }
}