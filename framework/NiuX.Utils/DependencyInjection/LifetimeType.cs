namespace NiuX.DependencyInjection;

/// <summary>
/// Lifetime Type
/// </summary>
public enum LifetimeType
{
    /// <summary>
    /// The singlelton
    /// </summary>
    Singlelton,
    /// <summary>
    /// The scope
    /// </summary>
    Scope,
    /// <summary>
    /// The transient
    /// </summary>
    Transient
}