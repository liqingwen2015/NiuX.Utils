namespace NiuX.DependencyInjection;

/// <summary>
/// Service Registry
/// </summary>
public class ServiceRegistry
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceRegistry"/> class.
    /// </summary>
    /// <param name="serviceType">Type of the service.</param>
    /// <param name="lifetimeType">Type of the lifetime.</param>
    /// <param name="factory">The factory.</param>
    public ServiceRegistry(Type serviceType, LifetimeType lifetimeType, Func<ServiceContainer, Type[], object> factory)
    {
        ServiceType = serviceType;
        LifetimeType = lifetimeType;
        Factory = factory;
    }

    /// <summary>
    /// Gets or sets the type of the service.
    /// </summary>
    /// <value>
    /// The type of the service.
    /// </value>
    public Type ServiceType { get; set; }

    /// <summary>
    /// Gets or sets the type of the lifetime.
    /// </summary>
    /// <value>
    /// The type of the lifetime.
    /// </value>
    public LifetimeType LifetimeType { get; set; }

    /// <summary>
    /// Gets or sets the factory.
    /// </summary>
    /// <value>
    /// The factory.
    /// </value>
    public Func<ServiceContainer, Type[], object> Factory { get; set; }

    /// <summary>
    /// Gets or sets the next.
    /// </summary>
    /// <value>
    /// The next.
    /// </value>
    internal ServiceRegistry Next { get; set; }

    /// <summary>
    /// Ases the enumerable.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<ServiceRegistry> AsEnumerable()
    {
        for (var self = this; self != null; self = self.Next) yield return self;
    }

    /// <summary>
    /// Ases the list.
    /// </summary>
    /// <returns></returns>
    public List<ServiceRegistry> AsList()
    {
        var list = new List<ServiceRegistry>();
        for (var self = this; self != null; self = self.Next) list.Add(self);
        return list;
    }
}