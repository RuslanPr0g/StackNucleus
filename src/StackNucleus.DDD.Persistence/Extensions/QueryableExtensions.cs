using StackNucleus.DDD.Domain.ClientModels;
using System.Linq.Expressions;
using System.Reflection;

namespace StackNucleus.DDD.Persistence.Extensions;

/// <summary>
/// Extensions for queryable models
/// </summary>
public static class QueryableExtensions
{
    /// <summary>
    /// Applies pagination to a queryable based on the provided query model.
    /// </summary>
    /// <param name="query">The source IQueryable to apply pagination on.</param>
    /// <param name="queryModel">The model containing pagination info (start index, items count).</param>
    /// <returns>The paginated IQueryable.</returns>
    public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, ClientQueryableModel queryModel)
    {
        if (queryModel is null || queryModel.ItemsCount <= 0)
        {
            return query;
        }

        return query
            .Skip(queryModel.StartIndex)
            .Take(queryModel.ItemsCount);
    }

    /// <summary>
    /// Orders a queryable by a specified property.
    /// </summary>
    /// <param name="source">The source IQueryable to apply ordering on.</param>
    /// <param name="propertyName">The property name to order by.</param>
    /// <param name="ascending">Determines whether the ordering is ascending (default is true).</param>
    /// <returns>The ordered IQueryable.</returns>
    public static IQueryable<T> OrderByProperty<T>(
        this IQueryable<T> source,
        string propertyName,
        bool ascending = true)
    {
        if (string.IsNullOrWhiteSpace(propertyName))
        {
            return source;
        }

        var type = typeof(T);
        var property = type.GetProperty(propertyName,
            BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

        if (property == null || !property.CanRead)
        {
            return source;
        }

        try
        {
            var parameter = Expression.Parameter(type, "x");
            var propertyAccess = Expression.Property(parameter, property);
            var lambda = Expression.Lambda(propertyAccess, parameter);

            var methodName = ascending ? "OrderBy" : "OrderByDescending";

            var method = typeof(Queryable)
                .GetMethods()
                .First(m => m.Name == methodName
                            && m.GetParameters().Length == 2)
                .MakeGenericMethod(type, property.PropertyType);

            return (IQueryable<T>)method.Invoke(null, new object[] { source, lambda })!;
        }
        catch
        {
            return source;
        }
    }
}
