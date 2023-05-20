using NiuX.DependencyInjection.Attributes;
using System.Collections.Concurrent;
using System.Reflection;

namespace NiuX.DependencyInjection;

/// <summary>
/// Ioc Factory
/// </summary>
public class IocFactory
{
    /// <summary>
    /// The container
    /// </summary>
    private readonly ConcurrentDictionary<string, object> _container = new();

    /// <summary>
    /// The type container
    /// </summary>
    private readonly ConcurrentDictionary<string, Type> _typeContainer = new();

    /// <summary>
    /// Loads the specified assembly.
    /// </summary>
    /// <param name="assembly">The assembly.</param>
    public void Load(Assembly assembly)
    {
        foreach (var type in assembly.GetTypes().Where(x => x.GetCustomAttribute<InjectionAttribute>() != null))
            _typeContainer.TryAdd(type.FullName!, type);
    }

    /// <summary>
    /// Gets the specified full name.
    /// </summary>
    /// <param name="fullName">The full name.</param>
    /// <returns></returns>
    /// <exception cref="System.Exception">对象尚未注册</exception>
    public object Get(string fullName)
    {
        if (_container.TryGetValue(fullName, out var obj)) return obj;

        if (!_typeContainer.TryGetValue(fullName, out var type)) throw new Exception("对象尚未注册");

        var instance = Activator.CreateInstance(type);

        foreach (var property in type.GetProperties())
        {
            var injectionProperty = property.GetCustomAttribute<InjectionAttribute>();

            if (injectionProperty == null) continue;

            var propertyInstance = Get(property.PropertyType.FullName);
            property.SetValue(instance, propertyInstance);
        }

        _container.TryAdd(type.FullName!, instance);
        return instance;
    }

    /// <summary>
    /// Gets the specified full name.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fullName">The full name.</param>
    /// <returns></returns>
    public T Get<T>(string fullName)
    {
        return (T)Get(fullName);
    }
}