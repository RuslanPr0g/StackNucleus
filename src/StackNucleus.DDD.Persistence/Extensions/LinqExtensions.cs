namespace StackNucleus.DDD.Persistence;

/// <summary>
/// Provides extension methods for LINQ queries to enhance the manipulation of collections.
/// These methods are designed to handle null values gracefully and provide more expressive querying capabilities.
/// </summary>
public static class LinqExtensions
{
    /// <summary>
    /// Selects elements from the source collection while skipping <see langword="null"/> values.
    /// The selector function is applied to each non-null element of the source sequence,
    /// and only non-null results from the selector are returned.
    /// </summary>
    /// <typeparam name="TSource">
    /// The type of the elements in the source collection. This type must be a reference type.
    /// </typeparam>
    /// <typeparam name="TResult">
    /// The type of the elements returned by the selector. This type must also be a reference type.
    /// </typeparam>
    /// <param name="source">
    /// The collection to query. This collection is expected to contain reference types, where null values are allowed.
    /// </param>
    /// <param name="selector">
    /// A function to transform each element of the source collection. This function is applied only to non-null elements.
    /// The result of the selector function is expected to be a reference type.
    /// </param>
    /// <returns>
    /// A sequence of results from the selector function, where both the source elements and the selector results are non-null.
    /// </returns>
    /// <remarks>
    /// This method first filters out any <see langword="null"/> values from the source collection,
    /// applies the selector to the remaining elements, and then filters out any <see langword="null"/> values
    /// from the selector's output.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the <paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.
    /// </exception>
    public static IEnumerable<TResult> SelectSkipNulls<TSource, TResult>(
        this IEnumerable<TSource?> source,
        Func<TSource, TResult?> selector)
        where TSource : class
        where TResult : class
    {
        return source
            .Where(x => x is not null)
            .Select(x => selector(x!))
            .Where(result => result is not null)!;
    }
}