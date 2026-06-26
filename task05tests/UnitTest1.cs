namespace task05tests;

using Xunit;
using task05;

public class TestClass
{
    public int PublicField;
    private string _privateField;
    public int Property { get; set; }

    public void Method() { }
    public int Add(int a, int b) => a + b;
}

[Serializable]
public class AttributedClass { }

public class ClassAnalyzerTests
{
    [Fact]
    public void GetPublicMethods_ReturnsCorrectMethods()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var methods = analyzer.GetPublicMethods();

        Assert.Contains("Method", methods);
    }

    [Fact]
    public void GetAllFields_IncludesPrivateFields()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var fields = analyzer.GetAllFields();

        Assert.Contains("_privateField", fields);
    }

    [Fact]
    public void GetProperties_ReturnsPublicProperties()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var properties = analyzer.GetProperties();

        Assert.Contains("Property", properties);
    }

    [Fact]
    public void HasAttribute_ReturnsTrueForAttributedClass()
    {
        var analyzer = new ClassAnalyzer(typeof(AttributedClass));

        Assert.True(analyzer.HasAttribute<SerializableAttribute>());
    }

    [Fact]
    public void GetMethodParams_ReturnsReturnTypeAndParameterInfo()
    {
        var analyzer = new ClassAnalyzer(typeof(TestClass));
        var methodParams = analyzer.GetMethodParams("Add").ToList();

        Assert.Equal(3, methodParams.Count);
        Assert.Equal("System.Int32", methodParams[0]);
        Assert.Equal("System.Int32 a", methodParams[1]);
        Assert.Equal("System.Int32 b", methodParams[2]);
    }
}
