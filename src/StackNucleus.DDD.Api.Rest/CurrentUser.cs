using System.Security.Claims;

namespace StackNucleus.DDD.Api.Rest;

/// <summary>
/// Represents the current user with an ID and username extracted from claims.
/// </summary>
public sealed record CurrentUser
{
    /// <summary>
    /// Gets the ID of the current user.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Gets the username of the current user.
    /// </summary>
    public string Username { get; }

    private CurrentUser(Guid id, string username)
    {
        Id = id;
        Username = username;
    }

    /// <summary>
    /// Creates a <see cref="CurrentUser"/> instance from the claims of the provided <see cref="ClaimsPrincipal"/>.
    /// </summary>
    /// <param name="user">
    /// The <see cref="ClaimsPrincipal"/> representing the current authenticated user.
    /// </param>
    /// <returns>
    /// A <see cref="CurrentUser"/> instance if valid user data is found in the claims; otherwise, <c>null</c>.
    /// </returns>
    public static CurrentUser? FromClaims(ClaimsPrincipal user)
    {
        var id = user.GetUserId();
        var username = user.GetUsername();

        if (!id.HasValue || string.IsNullOrWhiteSpace(username))
        {
            return null;
        }

        return new CurrentUser(id.Value, username);
    }
}