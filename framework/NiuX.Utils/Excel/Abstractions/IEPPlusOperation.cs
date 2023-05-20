namespace NiuX.Excel.Abstractions;

/// <summary>
/// IEPPlusOperation
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IEPPlusOperation<T>
{
    /// <summary>
    /// Gets or sets the data.
    /// </summary>
    /// <value>
    /// The data.
    /// </value>
    public List<T> Data { get; set; }
}

/// <summary>
/// EPPlusOperation
/// </summary>
/// <typeparam name="T"></typeparam>
/// <seealso cref="NiuX.Excel.Abstractions.IEPPlusOperation&lt;T&gt;" />
public class EPPlusOperation<T> : IEPPlusOperation<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EPPlusOperation{T}"/> class.
    /// </summary>
    /// <param name="data">The data.</param>
    public EPPlusOperation(List<T> data)
    {
        Data = data;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EPPlusOperation{T}"/> class.
    /// </summary>
    public EPPlusOperation()
    {
    }

    /// <summary>
    /// Gets or sets the data.
    /// </summary>
    /// <value>
    /// The data.
    /// </value>
    public List<T> Data { get; set; }
}