using System.Security.Claims;

namespace StackNucleus.DDD.Api.Rest;

/// <summary>
/// Extension methods for extracting custom claims from a <see cref="ClaimsPrincipal"/>.
/// </summary>
public static class ClaimsPrincipalExtensions
{
    /// <summary>
    /// Retrieves the "username" claim from the <see cref="ClaimsPrincipal"/>.
    /// </summary>
    /// <param name="user">
    /// The <see cref="ClaimsPrincipal"/> from which to extract the username claim.
    /// </param>
    /// <returns>
    /// The username if found; otherwise, <c>null</c>.
    /// </returns>
    public static string? GetUsername(this ClaimsPrincipal user)
    {
        var claim = user.FindFirst("username");
        if (claim is null)
        {
            return null;
        }

        return claim.Value;
    }

    /// <summary>
    /// Retrieves the "id" claim from the <see cref="ClaimsPrincipal"/> and parses it as a <see cref="Guid"/>.
    /// </summary>
    /// <param name="user">
    /// The <see cref="ClaimsPrincipal"/> from which to extract the user ID claim.
    /// </param>
    /// <returns>
    /// The user ID as a <see cref="Guid"/> if found and valid; otherwise, <c>null</c>.
    /// </returns>
    public static Guid? GetUserId(this ClaimsPrincipal user)
    {
        var claim = user.FindFirst("id");
        if (claim is null || !Guid.TryParse(claim.Value, out Guid value))
        {
            return null;
        }

        return value;
    }
}