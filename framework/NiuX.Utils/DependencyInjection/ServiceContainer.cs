using System.Collections.Concurrent;

namespace NiuX.DependencyInjection;

/// <summary>
/// 服务容器
/// </summary>
/// <seealso cref="System.IServiceProvider" />
/// <seealso cref="System.IDisposable" />
public class ServiceContainer : IServiceProvider, IDisposable
{
    /// <summary>
    /// The disposables
    /// </summary>
    private readonly ConcurrentBag<IDisposable> _disposables;
    /// <summary>
    /// The services
    /// </summary>
    private readonly ConcurrentDictionary<ServiceRegistry, object> _services;
    /// <summary>
    /// The disposed
    /// </summary>
    private volatile bool _disposed;
    /// <summary>
    /// The registries
    /// </summary>
    internal ConcurrentDictionary<Type, ServiceRegistry> Registries;
    /// <summary>
    /// The root
    /// </summary>
    internal ServiceContainer Root;

    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceContainer"/> class.
    /// </summary>
    public ServiceContainer()
    {
        Registries = new ConcurrentDictionary<Type, ServiceRegistry>();
        Root = this;
        _services = new ConcurrentDictionary<ServiceRegistry, object>();
        _disposables = new ConcurrentBag<IDisposable>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceContainer"/> class.
    /// </summary>
    /// <param name="parent">The parent.</param>
    internal ServiceContainer(ServiceContainer parent)
    {
        Root = parent.Root;
        Registries = Root.Registries;
        _services = new ConcurrentDictionary<ServiceRegistry, object>();
        _disposables = new ConcurrentBag<IDisposable>();
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        _disposed = true;
        foreach (var disposable in _disposables) disposable.Dispose();

        while (!_disposables.IsEmpty) _disposables.TryTake(out _);

        _services.Clear();
    }

    /// <summary>
    /// Gets the service object of the specified type.
    /// </summary>
    /// <param name="serviceType">An object that specifies the type of service object to get.</param>
    /// <returns>
    /// A service object of type <paramref name="serviceType">serviceType</paramref>.   -or-  null if there is no service object of type <paramref name="serviceType">serviceType</paramref>.
    /// </returns>
    public object GetService(Type serviceType)
    {
        EnsureNotDisposed();
        if (serviceType == typeof(ServiceContainer)) return this;

        ServiceRegistry registry;

        switch (serviceType.IsGenericType)
        {
            case true when serviceType.GetGenericTypeDefinition() == typeof(IEnumerable<>):
                {
                    var elementType = serviceType.GetGenericArguments()[0];

                    if (!Registries.TryGetValue(elementType, out var register)) return Array.CreateInstance(elementType, 0);

                    var registries = register.AsEnumerable();
                    var services = registries.Select(it => GetServiceCore(it, Type.EmptyTypes)).ToArray();
                    var array = Array.CreateInstance(elementType, services.Length);
                    services.CopyTo(array, 0);
                    return array;
                }
            case true when !Registries.ContainsKey(serviceType):
                {
                    var definition = serviceType.GetGenericTypeDefinition();
                    return Registries.TryGetValue(definition, out registry)
                        ? GetServiceCore(registry, serviceType.GetGenericArguments())
                        : null;
                }
            default:
                return Registries.TryGetValue(serviceType, out registry)
                    ? GetServiceCore(registry, Type.EmptyTypes)
                    : null;
        }
    }

    /// <summary>
    /// Registers the specified registry.
    /// </summary>
    /// <param name="registry">The registry.</param>
    /// <returns></returns>
    public ServiceContainer Register(ServiceRegistry registry)
    {
        EnsureNotDisposed();
        if (Registries.TryGetValue(registry.ServiceType, out var existing))
        {
            Registries[registry.ServiceType] = registry;
            registry.Next = existing;
        }
        else
        {
            Registries[registry.ServiceType] = registry;
        }

        return this;
    }

    /// <summary>
    /// Gets the service core.
    /// </summary>
    /// <param name="registry">The registry.</param>
    /// <param name="genericArguments">The generic arguments.</param>
    /// <returns></returns>
    private object GetServiceCore(ServiceRegistry registry, Type[] genericArguments)
    {
        var serviceType = registry.ServiceType;

        object GetOrCreate(ConcurrentDictionary<ServiceRegistry, object> services,
            ConcurrentBag<IDisposable> disposables)
        {
            if (services.TryGetValue(registry, out var service)) return service;

            service = registry.Factory(this, genericArguments);
            services[registry] = service;

            if (service is IDisposable disosable) disposables.Add(disosable);

            return service;
        }

        switch (registry.LifetimeType)
        {
            case LifetimeType.Singlelton:
                return GetOrCreate(Root._services, Root._disposables);

            case LifetimeType.Scope:
                return GetOrCreate(_services, _disposables);

            case LifetimeType.Transient:
            default:
                var service = registry.Factory(this, genericArguments);

                if (service is IDisposable disposable) _disposables.Add(disposable);

                return service;
        }
    }

    /// <summary>
    /// Ensures the not disposed.
    /// </summary>
    /// <exception cref="System.ObjectDisposedException">ServiceCollection</exception>
    private void EnsureNotDisposed()
    {
        if (_disposed) throw new ObjectDisposedException("ServiceCollection");
    }
}