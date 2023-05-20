using NiuX.DependencyInjection.Attributes;

namespace NiuX.DependencyInjection;

/// <summary>
/// Service Collection Extensions
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Gets the services.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="services">The services.</param>
    /// <returns></returns>
    public static IEnumerable<T> GetServices<T>(this ServiceContainer services)
    {
        return services.GetService<IEnumerable<T>>();
    }

    /// <summary>
    /// Gets the service.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="services">The services.</param>
    /// <returns></returns>
    public static T GetService<T>(this ServiceContainer services)
    {
        return (T)services.GetService(typeof(T));
    }

    /// <summary>
    /// Determines whether this instance has registry.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="services">The services.</param>
    /// <returns>
    ///   <c>true</c> if the specified services has registry; otherwise, <c>false</c>.
    /// </returns>
    public static bool HasRegistry<T>(this ServiceContainer services)
    {
        return services.HasRegistry(typeof(T));
    }

    /// <summary>
    /// Determines whether the specified service type has registry.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="serviceType">Type of the service.</param>
    /// <returns>
    ///   <c>true</c> if the specified service type has registry; otherwise, <c>false</c>.
    /// </returns>
    public static bool HasRegistry(this ServiceContainer services, Type serviceType)
    {
        return services.Root.Registries.ContainsKey(serviceType);
    }

    /// <summary>
    /// Registers the specified from.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="from">From.</param>
    /// <param name="to">To.</param>
    /// <param name="lifetimeType">Type of the lifetime.</param>
    /// <returns></returns>
    public static ServiceContainer Register(this ServiceContainer services, Type from, Type to,
        LifetimeType lifetimeType)
    {
        services.Register(new ServiceRegistry(from, lifetimeType, (_, arguments) => Create(_, to, arguments)));
        return services;
    }

    /// <summary>
    /// Registers the specified lifetime type.
    /// </summary>
    /// <typeparam name="TFrom">The type of from.</typeparam>
    /// <typeparam name="TTo">The type of to.</typeparam>
    /// <param name="services">The services.</param>
    /// <param name="lifetimeType">Type of the lifetime.</param>
    /// <returns></returns>
    public static ServiceContainer Register<TFrom, TTo>(this ServiceContainer services, LifetimeType lifetimeType)
        where TTo : TFrom
    {
        services.Register(new ServiceRegistry(typeof(TFrom), lifetimeType,
            (_, arguments) => Create(_, typeof(TTo), arguments)));
        return services;
    }

    /// <summary>
    /// Registers the specified factory.
    /// </summary>
    /// <typeparam name="TServiceType">The type of the service type.</typeparam>
    /// <param name="services">The services.</param>
    /// <param name="factory">The factory.</param>
    /// <param name="lifetimeType">Type of the lifetime.</param>
    /// <returns></returns>
    public static ServiceContainer Register<TServiceType>(this ServiceContainer services,
        Func<ServiceContainer, TServiceType> factory, LifetimeType lifetimeType = LifetimeType.Singlelton)
    {
        services.Register(new ServiceRegistry(typeof(TServiceType), lifetimeType, (_, __) => factory(_)));
        return services;
    }

    /// <summary>
    /// Registers the instance.
    /// </summary>
    /// <typeparam name="TServiceInstace">The type of the service instace.</typeparam>
    /// <param name="services">The services.</param>
    /// <param name="lifetimeType">Type of the lifetime.</param>
    /// <returns></returns>
    public static ServiceContainer RegisterInstance<TServiceInstace>(this ServiceContainer services,
        LifetimeType lifetimeType) where TServiceInstace : new()
    {
        services.Register(new ServiceRegistry(typeof(TServiceInstace), lifetimeType, (_, __) => new TServiceInstace()));
        return services;
    }

    /// <summary>
    /// Registers the singlelton.
    /// </summary>
    /// <typeparam name="TServiceInstace">The type of the service instace.</typeparam>
    /// <param name="services">The services.</param>
    /// <param name="instance">The instance.</param>
    /// <returns></returns>
    public static ServiceContainer RegisterSinglelton<TServiceInstace>(this ServiceContainer services,
        TServiceInstace instance)
    {
        services.Register(new ServiceRegistry(typeof(TServiceInstace), LifetimeType.Singlelton, (_, __) => instance!));
        return services;
    }

    /// <summary>
    /// Creates the child.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns></returns>
    public static ServiceContainer CreateChild(this ServiceContainer services)
    {
        return new ServiceContainer(services);
    }

    /// <summary>
    /// Creates the specified services.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="type">The type.</param>
    /// <param name="genericArgumnents">The generic argumnents.</param>
    /// <returns></returns>
    /// <exception cref="System.InvalidOperationException">
    /// Cannot create the instance of {type} which does not have an public constructor.
    /// or
    /// Cannot create the instance of {type} whose constructor has non-registered parameter type(s)
    /// </exception>
    private static object Create(ServiceContainer services, Type type, Type[] genericArgumnents)
    {
        if (genericArgumnents.Length > 0) type = type.MakeGenericType(genericArgumnents);

        var constructors = type.GetConstructors();

        if (constructors.Length == 0)
            throw new InvalidOperationException(
                $"Cannot create the instance of {type} which does not have an public constructor.");

        var constructor =
            constructors.FirstOrDefault(x => x.GetCustomAttributes(false).OfType<InjectionAttribute>().Any());
        constructor = constructor ?? constructors.First();

        var parameters = constructor.GetParameters();

        if (parameters.Length == 0) return Activator.CreateInstance(type);

        var arguments = new object[parameters.Length];
        for (var index = 0; index < arguments.Length; index++)
        {
            var parameter = parameters[index];
            var parameterType = parameter.ParameterType;

            if (services.HasRegistry(parameterType))
                arguments[index] = services.GetService(parameterType);
            else if (parameter.HasDefaultValue)
                arguments[index] = parameter.DefaultValue;
            else
                throw new InvalidOperationException(
                    $"Cannot create the instance of {type} whose constructor has non-registered parameter type(s)");
        }

        return Activator.CreateInstance(type, arguments);
    }
}