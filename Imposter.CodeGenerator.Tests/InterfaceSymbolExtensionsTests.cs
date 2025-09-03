using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace Imposter.CodeGenerator.Tests;

public class InterfaceSymbolExtensionsTests
{
    private static Compilation CreateCompilation(string source)
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(source);
        var references = new MetadataReference[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
        };

        return CSharpCompilation.Create("test", new[] { syntaxTree }, references);
    }

    private static INamedTypeSymbol GetInterfaceSymbol(Compilation compilation, string interfaceName)
    {
        return compilation.GetTypeByMetadataName(interfaceName);
    }

    [Fact]
    public void GetAllInterfaceMethods_SimpleInterface_ReturnsAllMethods()
    {
        var source = @"
public interface ISimple
{
    void Method1();
    int Method2(string param);
}
";

        var compilation = CreateCompilation(source);
        var interfaceSymbol = GetInterfaceSymbol(compilation, "ISimple");

        var methods = interfaceSymbol.GetAllInterfaceMethods();

        Assert.Equal(2, methods.Count);
        Assert.Contains(methods, m => m.Name == "Method1");
        Assert.Contains(methods, m => m.Name == "Method2");
    }

    [Fact]
    public void GetAllInterfaceMethods_InterfaceWithProperties_ExcludesAccessors()
    {
        var source = @"
public interface IWithProperties
{
    void RegularMethod();
    int Property { get; set; }
    event System.EventHandler Event;
}
";

        var compilation = CreateCompilation(source);
        var interfaceSymbol = GetInterfaceSymbol(compilation, "IWithProperties");

        var methods = interfaceSymbol.GetAllInterfaceMethods();

        Assert.Single(methods);
        Assert.Equal("RegularMethod", methods[0].Name);
        Assert.DoesNotContain(methods, m => m.Name.Contains("get_") || m.Name.Contains("set_"));
        Assert.DoesNotContain(methods, m => m.Name.Contains("add_") || m.Name.Contains("remove_"));
    }

    [Fact]
    public void GetAllInterfaceMethods_InheritanceChain_ReturnsAllMethodsFromHierarchy()
    {
        var source = @"
public interface IParent
{
    void ParentMethod();
}

public interface IChild : IParent
{
    void ChildMethod();
}

public interface IGrandChild : IChild
{
    void GrandChildMethod();
}
";

        var compilation = CreateCompilation(source);
        var grandChildSymbol = GetInterfaceSymbol(compilation, "IGrandChild");

        var methods = grandChildSymbol.GetAllInterfaceMethods();

        Assert.Equal(3, methods.Count);
        Assert.Contains(methods, m => m.Name == "ParentMethod");
        Assert.Contains(methods, m => m.Name == "ChildMethod");
        Assert.Contains(methods, m => m.Name == "GrandChildMethod");
    }

    [Fact]
    public void GetAllInterfaceMethods_MultipleInheritance_ReturnsAllMethods()
    {
        var source = @"
public interface IFirst
{
    void FirstMethod();
}

public interface ISecond
{
    void SecondMethod();
}

public interface ICombined : IFirst, ISecond
{
    void CombinedMethod();
}
";

        var compilation = CreateCompilation(source);
        var combinedSymbol = GetInterfaceSymbol(compilation, "ICombined");

        var methods = combinedSymbol.GetAllInterfaceMethods();

        Assert.Equal(3, methods.Count);
        Assert.Contains(methods, m => m.Name == "FirstMethod");
        Assert.Contains(methods, m => m.Name == "SecondMethod");
        Assert.Contains(methods, m => m.Name == "CombinedMethod");
    }

    [Fact]
    public void GetAllInterfaceMethods_CircularInheritance_PreventsInfiniteRecursion()
    {
        var source = @"
public interface ICircularA : ICircularB
{
    void MethodA();
}

public interface ICircularB : ICircularA
{
    void MethodB();
}
";

        var compilation = CreateCompilation(source);
        var interfaceSymbol = GetInterfaceSymbol(compilation, "ICircularA");

        var methods = interfaceSymbol.GetAllInterfaceMethods();

        // Should get both methods without infinite recursion
        Assert.Equal(2, methods.Count);
        Assert.Contains(methods, m => m.Name == "MethodA");
        Assert.Contains(methods, m => m.Name == "MethodB");
    }

    [Fact]
    public void GetAllInterfaceMethods_EmptyInterface_ReturnsEmptyList()
    {
        var source = @"
public interface IEmpty
{
}
";

        var compilation = CreateCompilation(source);
        var interfaceSymbol = GetInterfaceSymbol(compilation, "IEmpty");

        var methods = interfaceSymbol.GetAllInterfaceMethods();

        Assert.Empty(methods);
    }

    [Fact]
    public void GetAllInterfaceMethods_InterfaceWithMethodsAndOperators_ReturnsOnlyOrdinaryMethods()
    {
        var source = @"
public interface IWithOperators
{
    void RegularMethod();
    int RegularMethodWithParams(string param);
    
    // These would be compiler-generated and should be excluded
    int this[int index] { get; set; }
    event System.EventHandler SomeEvent;
}
";

        var compilation = CreateCompilation(source);
        var interfaceSymbol = GetInterfaceSymbol(compilation, "IWithOperators");

        var methods = interfaceSymbol.GetAllInterfaceMethods();

        Assert.Equal(2, methods.Count);
        Assert.Contains(methods, m => m.Name == "RegularMethod");
        Assert.Contains(methods, m => m.Name == "RegularMethodWithParams");
        Assert.DoesNotContain(methods, m => m.MethodKind != MethodKind.Ordinary);
    }
}