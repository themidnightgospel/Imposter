using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.ClassImpersonation;

public class GenericOverrideConstraintTests
{
    [Fact]
    public async Task GivenVirtualMethodWithInterfaceConstraint_WhenImposterIsGenerated_ShouldCompile()
    {
        await AssertClassImposterCompiles(
            "public virtual T Echo<T>(T value) where T : System.IDisposable => value;"
        );
    }

    [Fact]
    public async Task GivenVirtualMethodWithBaseClassConstraint_WhenImposterIsGenerated_ShouldCompile()
    {
        await AssertClassImposterCompiles(
            "public virtual T Echo<T>(T value) where T : System.IO.Stream => value;"
        );
    }

    [Fact]
    public async Task GivenVirtualMethodWithConstructorConstraint_WhenImposterIsGenerated_ShouldCompile()
    {
        await AssertClassImposterCompiles(
            "public virtual T Echo<T>(T value) where T : new() => value;"
        );
    }

    [Fact]
    public async Task GivenAbstractMethodWithInterfaceConstraint_WhenImposterIsGenerated_ShouldCompile()
    {
        await AssertClassImposterCompiles(
            "public abstract T Echo<T>(T value) where T : System.IDisposable;",
            isAbstract: true
        );
    }

    [Fact]
    public async Task GivenVirtualMethodWithUnmanagedConstraint_WhenImposterIsGenerated_ShouldCompile()
    {
        await AssertClassImposterCompiles(
            "public virtual T Echo<T>(T value) where T : unmanaged => value;"
        );
    }

    [Fact]
    public async Task GivenVirtualMethodWithNotNullConstraint_WhenImposterIsGenerated_ShouldCompile()
    {
        await AssertClassImposterCompiles(
            "public virtual T Echo<T>(T value) where T : notnull => value;"
        );
    }

    [Fact]
    public async Task GivenVirtualMethodWithClassAndInterfaceConstraintsAndNullableSignature_WhenImposterIsGenerated_ShouldCompile()
    {
        await AssertClassImposterCompiles(
            "public virtual T? Echo<T>(T? value) where T : class, System.IDisposable => value;"
        );
    }

    [Fact]
    public async Task GivenVirtualMethodWithStructAndInterfaceConstraintsAndNullableSignature_WhenImposterIsGenerated_ShouldCompile()
    {
        await AssertClassImposterCompiles(
            "public virtual T? Echo<T>(T? value) where T : struct, System.IComparable<T> => value;"
        );
    }

    [Fact]
    public async Task GivenVirtualMethodWithNullableClassConstraint_WhenImposterIsGenerated_ShouldCompile()
    {
        await AssertClassImposterCompiles(
            "public virtual T Echo<T>(T value) where T : class?, System.IDisposable? => value;"
        );
    }

    [Fact]
    public async Task GivenVirtualMethodWithUnconstrainedNullableSignature_WhenImposterIsGenerated_ShouldCompile()
    {
        await AssertClassImposterCompiles("public virtual T? Echo<T>(T? value) => value;");
    }

    [Fact]
    public async Task GivenVirtualMethodWithBaseClassConstraintAndNullableSignature_WhenImposterIsGenerated_ShouldCompile()
    {
        await AssertClassImposterCompiles(
            "public virtual T? Echo<T>(T? value) where T : System.IO.Stream => value;"
        );
    }

    [Fact]
    public async Task GivenVirtualMethodWithInterfaceConstraintAndNullableSignature_WhenImposterIsGenerated_ShouldCompile()
    {
        await AssertClassImposterCompiles(
            "public virtual T? Echo<T>(T? value) where T : System.IDisposable => value;"
        );
    }

    [Fact]
    public async Task GivenVirtualMethodWithNotNullConstraintAndNullableSignature_WhenImposterIsGenerated_ShouldCompile()
    {
        await AssertClassImposterCompiles(
            "public virtual T? Echo<T>(T? value) where T : notnull => value;"
        );
    }

    [Fact]
    public async Task GivenExplicitlyImplementedInterfaceMethodWithConstraint_WhenImposterIsGenerated_ShouldCompile()
    {
        var context = await GeneratorTestHelper.CreateContext(
            /*lang=csharp*/
            """
            #nullable enable
            using Imposter.Abstractions;

            [assembly: GenerateImposter(typeof(Sample.IService))]

            namespace Sample
            {
                public interface IFirst { T Echo<T>(T value) where T : System.IDisposable; }

                public interface ISecond { T Echo<T>(T value) where T : System.IDisposable; }

                public interface IService : IFirst, ISecond { }
            }
            """,
            baseSourceFileName: "GenericOverrideConstraint.cs",
            snippetFileName: "Snippet.cs",
            assemblyName: nameof(GenericOverrideConstraintTests)
        );

        GeneratorTestHelper.AssertNoDiagnostics(context.CompileSnippet(string.Empty));
    }

    private static async Task AssertClassImposterCompiles(
        string methodDeclaration,
        bool isAbstract = false
    )
    {
        var classModifier = isAbstract ? "abstract " : "";
        var context = await GeneratorTestHelper.CreateContext(
            /*lang=csharp*/
            $$"""
            #nullable enable
            using Imposter.Abstractions;

            [assembly: GenerateImposter(typeof(Sample.Service))]

            namespace Sample
            {
                public {{classModifier}}class Service
                {
                    {{methodDeclaration}}
                }
            }
            """,
            baseSourceFileName: "GenericOverrideConstraint.cs",
            snippetFileName: "Snippet.cs",
            assemblyName: nameof(GenericOverrideConstraintTests)
        );

        GeneratorTestHelper.AssertNoDiagnostics(context.CompileSnippet(string.Empty));
    }
}
