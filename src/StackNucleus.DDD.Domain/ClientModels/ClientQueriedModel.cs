namespace StackNucleus.DDD.Domain.ClientModels;

/// <summary>
/// Represents a paged query result for a specific type <typeparamref name="T"/>.
/// Contains the items returned by a query and metadata about the total available items.
/// </summary>
/// <typeparam name="T">The type of the items contained in the query result. Must be a reference type.</typeparam>
public sealed class ClientQueriedModel<T> where T : class
{
    /// <summary>
    /// An empty instance of <see cref="ClientQueriedModel{T}"/> with no items and a total count of zero.
    /// </summary>
    public static ClientQueriedModel<T> Empty = ClientQueriedModel<T>.Create(Array.Empty<T>(), 0);

    /// <summary>
    /// The collection of items retrieved by the query.
    /// </summary>
    public required IEnumerable<T> Items { get; set; }

    /// <summary>
    /// The total number of items available in the data source for the query.
    /// </summary>
    public required int TotalItemsCount { get; set; }

    /// <summary>
    /// The number of items returned in the current page. Optional.
    /// </summary>
    public int? ItemsPerCurrentPageCount { get; set; } = null;

    /// <summary>
    /// Creates a new instance of <see cref="ClientQueriedModel{T}"/> with the provided items and total count.
    /// </summary>
    /// <param name="items">The collection of items retrieved by the query.</param>
    /// <param name="totalCount">The total number of items available.</param>
    /// <returns>A new <see cref="ClientQueriedModel{T}"/> instance.</returns>
    public static ClientQueriedModel<T> Create(IEnumerable<T> items, int totalCount)
    {
        return new ClientQueriedModel<T>()
        {
            Items = items,
            TotalItemsCount = totalCount
        };
    }

    /// <summary>
    /// Creates a new instance of <see cref="ClientQueriedModel{T}"/> with the provided items, total count, 
    /// and the number of items per current page.
    /// </summary>
    /// <param name="items">The collection of items retrieved by the query.</param>
    /// <param name="totalCount">The total number of items available.</param>
    /// <param name="itemsPerPage">The number of items returned in the current page.</param>
    /// <returns>A new <see cref="ClientQueriedModel{T}"/> instance.</returns>
    public static ClientQueriedModel<T> Create(IEnumerable<T> items, int totalCount, int itemsPerPage)
    {
        return new ClientQueriedModel<T>()
        {
            Items = items,
            TotalItemsCount = totalCount,
            ItemsPerCurrentPageCount = itemsPerPage
        };
    }
}