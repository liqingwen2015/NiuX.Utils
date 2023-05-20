using Microsoft.Extensions.Configuration;
using System.Data;

namespace NiuX.AnyDbConfigProvider;

/// <summary>
/// Db 配置提供器扩展
/// </summary>
public static class DbConfigurationProviderExtensions
{
    /// <summary>
    /// Adds the database configuration.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="setup">The setup.</param>
    /// <returns></returns>
    public static IConfigurationBuilder AddDbConfiguration(this IConfigurationBuilder builder,
        DbConfigOptions setup)
    {
        return builder.Add(new DbConfigurationSource(setup));
    }

    /// <summary>
    /// Adds the database configuration.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="createDbConnection">The create database connection.</param>
    /// <param name="tableName">Name of the table.</param>
    /// <param name="reloadOnChange">if set to <c>true</c> [reload on change].</param>
    /// <param name="reloadInterval">The reload interval.</param>
    /// <returns></returns>
    public static IConfigurationBuilder AddDbConfiguration(this IConfigurationBuilder builder,
        Func<IDbConnection> createDbConnection, string tableName = "T_Configs", bool reloadOnChange = false,
        TimeSpan? reloadInterval = null)
    {
        return builder.AddDbConfiguration(new DbConfigOptions
        {
            CreateDbConnection = createDbConnection,
            TableName = tableName,
            ReloadOnChange = reloadOnChange,
            ReloadInterval = reloadInterval
        });
    }
}