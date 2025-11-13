/// <summary>
/// Represents the configuration for a job, including its key, type, repeat interval, and repeat behavior.
/// </summary>
public sealed class JobConfiguration
{
    /// <summary>
    /// Gets or sets the key associated with the job configuration.
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the type of the job.
    /// </summary>
    public Type Type { get; set; }

    /// <summary>
    /// Gets or sets the repeat interval for the job in seconds. A value of -1 means no repeat interval, it is a default value.
    /// </summary>
    public int RepeatInterval { get; set; } = -1;

    /// <summary>
    /// Gets or sets a value indicating whether the job should repeat forever.
    /// </summary>
    public bool RepeatForever { get; set; } = false;
}
