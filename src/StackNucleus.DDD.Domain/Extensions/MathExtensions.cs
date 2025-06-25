namespace StackNucleus.DDD.Domain.Extensions;

/// <summary>
/// Provides extension methods for mathematical operations.
/// This static class includes a method to calculate the percentage of a given number relative to a total count.
/// </summary>
public static class MathExtensions
{
    /// <summary>
    /// Calculates the percentage of the current count relative to the total count.
    /// </summary>
    /// <param name="currentCount">The current count or part of the total.</param>
    /// <param name="totalCount">The total count, used as the denominator for percentage calculation.</param>
    /// <returns>The percentage as a <see cref="decimal"/>. If <paramref name="currentCount"/> is greater than 
    /// <paramref name="totalCount"/>, the method returns 100. If either <paramref name="currentCount"/> or <paramref name="totalCount"/> is 0, it returns 0.</returns>
    public static decimal PercentageOf(this int currentCount, int totalCount)
    {
        if (currentCount > totalCount)
        {
            return 100;
        }

        if (totalCount == 0 || currentCount == 0)
        {
            return 0;
        }

        return currentCount * 1m / (totalCount * 1m) * 100;
    }
}
