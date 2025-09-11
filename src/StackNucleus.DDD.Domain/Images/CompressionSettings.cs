namespace StackNucleus.DDD.Domain.Images;

/// <summary>
/// Represents the settings for compression, including the format dimensions and maximum width.
/// </summary>
public sealed class CompressionSettings
{
    /// <summary>
    /// Gets or sets the width of the format. Defaults to 16.
    /// </summary>
    public int FormatWidth { get; set; } = 16;

    /// <summary>
    /// Gets or sets the height of the format. Defaults to 9.
    /// </summary>
    public int FormatHeight { get; set; } = 9;

    /// <summary>
    /// Gets or sets the maximum allowed width for compression. Defaults to 1080.
    /// </summary>
    public int MaxWidth { get; set; } = 1080;

    /// <summary>
    /// Provides the default compression settings.
    /// </summary>
    public static CompressionSettings Default = new();
}
