namespace task05;

using System.Reflection;

public class ClassAnalyzer
{
    private readonly Type _type;

    public ClassAnalyzer(Type type)
    {
        _type = type ?? throw new ArgumentNullException(nameof(type));
    }

    public IEnumerable<string> GetPublicMethods()
    {
        const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
        return _type.GetMethods(flags)
                    .Where(m => !m.IsSpecialName)
                    .Select(m => m.Name);
    }

    public IEnumerable<string> GetMethodParams(string methodName)
    {
        const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
        var method = _type.GetMethods(flags)
                          .FirstOrDefault(m => m.Name == methodName && !m.IsSpecialName);

        if (method == null)
            return Enumerable.Empty<string>();

        var result = new List<string>
        {
            method.ReturnType.FullName ?? method.ReturnType.Name
        };

        result.AddRange(method.GetParameters()
                              .Select(p => $"{p.ParameterType.FullName ?? p.ParameterType.Name} {p.Name}"));

        return result;
    }

    public IEnumerable<string> GetAllFields()
    {
        const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
        return _type.GetFields(flags).Select(f => f.Name);
    }

    public IEnumerable<string> GetProperties()
    {
        const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
        return _type.GetProperties(flags).Select(p => p.Name);
    }

    public bool HasAttribute<T>() where T : Attribute
    {
        return _type.IsDefined(typeof(T), inherit: true);
    }
}
