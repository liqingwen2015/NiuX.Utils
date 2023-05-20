namespace NiuX.Csv.Attributes;

/// <summary>
/// Csv 列名
/// </summary>
/// <seealso cref="System.Attribute" />
public sealed class CsvColumnNameAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CsvColumnNameAttribute"/> class.
    /// </summary>
    /// <param name="name">The name.</param>
    public CsvColumnNameAttribute(string name)
    {
        Name = name;
    }

    /// <summary>
    /// 名称
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    public string Name { get; set; }
}