namespace NiuX.Object;

public static class ObjectInitializer
{
    public static T Initialize<T>()
    {
        var obj = (T)Activator.CreateInstance(typeof(T));

        foreach (var property in typeof(T).GetProperties())
        {
            if (property.PropertyType.IsValueType) continue;

            if (property.PropertyType == typeof(string))
                property.SetValue(obj, "");
            else if (property.PropertyType.IsArray)
                //Array.CreateInstance(property.PropertyType,0);
                property.SetValue(obj,
                    Array.CreateInstance(Type.GetType(property.PropertyType.FullName.Replace("[]", "")), 0));
            // property.PropertyType.InvokeMember("Set", BindingFlags.CreateInstance,null, array, new object[] { 5 })
            else if (property.PropertyType.IsArray
                     || property.PropertyType is { IsClass: true, IsGenericType: false, IsValueType: false })
                property.SetValue(obj, Initialize(property.PropertyType));
            else
                property.SetValue(obj, Activator.CreateInstance(property.PropertyType));
        }

        return obj;
    }

    public static object Initialize(Type type)
    {
        if (type == typeof(string)) return null;

        if (type.IsArray) return Array.CreateInstance(Type.GetType(type.FullName.Replace("[]", "")), 0);

        var obj = Activator.CreateInstance(type);

        foreach (var property in type.GetProperties())
        {
            if (property.PropertyType == type)
            {
                property.SetValue(obj, Activator.CreateInstance(type));

                continue;
            }

            if (property.PropertyType.IsValueType) continue;

            if (property.PropertyType == typeof(string))
                property.SetValue(obj, "");
            else if (property.PropertyType.IsArray)
                //Array.CreateInstance(property.PropertyType,0);
                property.SetValue(obj,
                    Array.CreateInstance(Type.GetType(property.PropertyType.FullName.Replace("[]", "")), 0));
            // property.PropertyType.InvokeMember("Set", BindingFlags.CreateInstance,null, array, new object[] { 5 })
            else if (property.PropertyType.IsArray
                     || property.PropertyType.IsClass
                         && !property.PropertyType.IsGenericType
                         && !property.PropertyType.IsValueType)
                Initialize(property.PropertyType);
            else
                property.SetValue(obj, Activator.CreateInstance(property.PropertyType));
        }

        return obj;
    }
}