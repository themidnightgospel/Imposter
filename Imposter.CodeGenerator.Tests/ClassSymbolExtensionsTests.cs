using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace Imposter.CodeGenerator.Tests;

public class ClassSymbolExtensionsTests
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

    private static INamedTypeSymbol GetTypeSymbol(Compilation compilation, string typeName)
    {
        return compilation.GetTypeByMetadataName(typeName);
    }

    [Fact]
    public void GetAllOverridableMethods_ClassWithVirtualMethods_ReturnsVirtualMethods()
    {
        var source = @"
public class BaseClass
{
    public virtual void VirtualMethod() { }
    public void NormalMethod() { }
    public static void StaticMethod() { }
}
";

        var compilation = CreateCompilation(source);
        var classSymbol = GetTypeSymbol(compilation, "BaseClass");

        var methods = classSymbol.GetAllOverridableMethods();

        Assert.Single(methods);
        Assert.Equal("VirtualMethod", methods[0].Name);
        Assert.True(methods[0].IsVirtual);
    }

    [Fact]
    public void GetAllOverridableMethods_ClassWithAbstractMethods_ReturnsAbstractMethods()
    {
        var source = @"
public abstract class AbstractClass
{
    public abstract void AbstractMethod();
    public void NormalMethod() { }
}
";

        var compilation = CreateCompilation(source);
        var classSymbol = GetTypeSymbol(compilation, "AbstractClass");

        var methods = classSymbol.GetAllOverridableMethods();

        Assert.Single(methods);
        Assert.Equal("AbstractMethod", methods[0].Name);
        Assert.True(methods[0].IsAbstract);
    }

    [Fact]
    public void GetAllOverridableMethods_ClassWithOverrideMethods_ReturnsOverrideMethods()
    {
        var source = @"
public class BaseClass
{
    public virtual void VirtualMethod() { }
}

public class DerivedClass : BaseClass
{
    public override void VirtualMethod() { }
    public void NewMethod() { }
}
";

        var compilation = CreateCompilation(source);
        var classSymbol = GetTypeSymbol(compilation, "DerivedClass");

        var methods = classSymbol.GetAllOverridableMethods();

        Assert.Single(methods);
        Assert.Equal("VirtualMethod", methods[0].Name);
        Assert.True(methods[0].IsOverride);
    }

    [Fact]
    public void GetAllOverridableMethods_InheritanceChain_ReturnsMethodsFromBaseClasses()
    {
        var source = @"
public class GrandParent
{
    public virtual void GrandParentMethod() { }
}

public class Parent : GrandParent
{
    public virtual void ParentMethod() { }
}

public class Child : Parent
{
    public virtual void ChildMethod() { }
}
";

        var compilation = CreateCompilation(source);
        var classSymbol = GetTypeSymbol(compilation, "Child");

        var methods = classSymbol.GetAllOverridableMethods();

        Assert.Equal(3, methods.Count);
        Assert.Contains(methods, m => m.Name == "GrandParentMethod");
        Assert.Contains(methods, m => m.Name == "ParentMethod");
        Assert.Contains(methods, m => m.Name == "ChildMethod");
    }

    [Fact]
    public void GetAllOverridableMethods_SealedClass_ReturnsEmptyList()
    {
        var source = @"
public sealed class SealedClass
{
    public virtual void VirtualMethod() { }
}
";

        var compilation = CreateCompilation(source);
        var classSymbol = GetTypeSymbol(compilation, "SealedClass");

        var methods = classSymbol.GetAllOverridableMethods();

        Assert.Empty(methods);
    }

    [Fact]
    public void GetAllOverridableMethods_ClassWithPrivateAndProtectedMethods_ReturnsOnlyAccessibleOnes()
    {
        var source = @"
public class TestClass
{
    public virtual void PublicVirtual() { }
    protected virtual void ProtectedVirtual() { }
    private virtual void PrivateVirtual() { }
    internal virtual void InternalVirtual() { }
}
";

        var compilation = CreateCompilation(source);
        var classSymbol = GetTypeSymbol(compilation, "TestClass");

        var methods = classSymbol.GetAllOverridableMethods();

        // Should include public, protected, and internal (but not private)
        Assert.Equal(3, methods.Count);
        Assert.Contains(methods, m => m.Name == "PublicVirtual");
        Assert.Contains(methods, m => m.Name == "ProtectedVirtual");
        Assert.Contains(methods, m => m.Name == "InternalVirtual");
        Assert.DoesNotContain(methods, m => m.Name == "PrivateVirtual");
    }

    [Fact]
    public void GetAllOverridableMethods_ClassWithSealedMethods_ExcludesSealedMethods()
    {
        var source = @"
public class BaseClass
{
    public virtual void VirtualMethod() { }
}

public class DerivedClass : BaseClass
{
    public sealed override void VirtualMethod() { }
    public virtual void NewVirtualMethod() { }
}
";

        var compilation = CreateCompilation(source);
        var classSymbol = GetTypeSymbol(compilation, "DerivedClass");

        var methods = classSymbol.GetAllOverridableMethods();

        // Should only include the new virtual method, not the sealed override
        Assert.Single(methods);
        Assert.Equal("NewVirtualMethod", methods[0].Name);
        Assert.DoesNotContain(methods, m => m.Name == "VirtualMethod");
    }

    [Fact]
    public void GetAllOverridableMethods_ClassWithInterfaceImplementation_IncludesInterfaceMethods()
    {
        var source = @"
public interface ITest
{
    void InterfaceMethod();
}

public class TestClass : ITest
{
    public virtual void InterfaceMethod() { }
    public virtual void ClassMethod() { }
}
";

        var compilation = CreateCompilation(source);
        var classSymbol = GetTypeSymbol(compilation, "TestClass");

        var methods = classSymbol.GetAllOverridableMethods();

        Assert.Equal(2, methods.Count);
        Assert.Contains(methods, m => m.Name == "InterfaceMethod");
        Assert.Contains(methods, m => m.Name == "ClassMethod");
    }

    [Fact]
    public void GetAllOverridableMethods_NonClassSymbol_ReturnsEmptyList()
    {
        var source = @"
public interface ITest
{
    void Method();
}
";

        var compilation = CreateCompilation(source);
        var interfaceSymbol = GetTypeSymbol(compilation, "ITest");

        var methods = interfaceSymbol.GetAllOverridableMethods();

        Assert.Empty(methods);
    }
}