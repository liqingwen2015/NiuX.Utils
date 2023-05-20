using Microsoft.Extensions.Configuration;

namespace NiuX.AnyDbConfigProvider;

/// <summary>
/// Db 配置源
/// </summary>
/// <seealso cref="IConfigurationSource" />
public class DbConfigurationSource : IConfigurationSource
{
    /// <summary>
    /// The options
    /// </summary>
    private readonly DbConfigOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="DbConfigurationSource"/> class.
    /// </summary>
    /// <param name="options">The options.</param>
    public DbConfigurationSource(DbConfigOptions options)
    {
        _options = options;
    }

    /// <summary>
    /// Builds the <see cref="T:Microsoft.Extensions.Configuration.IConfigurationProvider" /> for this source.
    /// </summary>
    /// <param name="builder">The <see cref="T:Microsoft.Extensions.Configuration.IConfigurationBuilder" />.</param>
    /// <returns>
    /// An <see cref="T:Microsoft.Extensions.Configuration.IConfigurationProvider" />
    /// </returns>
    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new DbConfigurationProvider(_options);
    }
}