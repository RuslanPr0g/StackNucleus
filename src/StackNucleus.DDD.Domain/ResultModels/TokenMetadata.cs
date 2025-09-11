namespace StackNucleus.DDD.Domain.ResultModels;

/// <summary>
/// Represents the metadata for an authentication token, including the token and refresh token.
/// </summary>
public sealed record TokenMetadata
{
    /// <summary>
    /// Gets or sets the authentication token.
    /// </summary>
    public string Token { get; set; }

    /// <summary>
    /// Gets or sets the refresh token used to obtain a new authentication token.
    /// </summary>
    public string RefreshToken { get; set; }
}
