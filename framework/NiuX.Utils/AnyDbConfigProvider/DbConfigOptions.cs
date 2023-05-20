using System.Data;

namespace NiuX.AnyDbConfigProvider;

/// <summary>
/// 数据库配置
/// </summary>
public class DbConfigOptions
{
    /// <summary>
    /// Gets or sets the create database connection.
    /// </summary>
    /// <value>
    /// The create database connection.
    /// </value>
    public Func<IDbConnection>? CreateDbConnection { get; set; }

    /// <summary>
    /// Gets or sets the name of the table.
    /// </summary>
    /// <value>
    /// The name of the table.
    /// </value>
    public string? TableName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether [reload on change].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [reload on change]; otherwise, <c>false</c>.
    /// </value>
    public bool ReloadOnChange { get; set; }

    /// <summary>
    /// Gets or sets the reload interval.
    /// </summary>
    /// <value>
    /// The reload interval.
    /// </value>
    public TimeSpan? ReloadInterval { get; set; }
}