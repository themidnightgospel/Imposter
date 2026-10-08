// `ref readonly` parameters need C# 12, which the Roslyn 4.0 and 4.4 builds of these tests don't know.
#if ROSLYN4_14_OR_GREATER
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.MethodImpersonation;

public class RefReadOnlyParameterTests
{
    [Fact]
    public async Task GivenInterfaceMethodWithRefReadOnlyParameter_WhenImposterIsGenerated_ShouldCompile()
    {
        await AssertImposterCompiles(
            "public interface IService { int Read(ref readonly int key); }",
            "Sample.IService"
        );
    }

    [Fact]
    public async Task GivenGenericInterfaceMethodWithRefReadOnlyParameter_WhenImposterIsGenerated_ShouldCompile()
    {
        await AssertImposterCompiles(
            "public interface IService { T Echo<T>(ref readonly T value); }",
            "Sample.IService"
        );
    }

    [Fact]
    public async Task GivenVirtualMethodWithRefReadOnlyParameter_WhenImposterIsGenerated_ShouldCompile()
    {
        await AssertImposterCompiles(
            "public class Service { public virtual int Read(ref readonly int key) => key; }",
            "Sample.Service"
        );
    }

    [Fact]
    public async Task GivenAbstractMethodWithRefReadOnlyParameter_WhenImposterIsGenerated_ShouldCompile()
    {
        await AssertImposterCompiles(
            "public abstract class Service { public abstract int Read(ref readonly int key); }",
            "Sample.Service"
        );
    }

    [Fact]
    public async Task GivenVoidMethodWithRefReadOnlyAndOtherRefKinds_WhenImposterIsGenerated_ShouldCompile()
    {
        await AssertImposterCompiles(
            "public interface IService { void Mix(ref readonly int key, in int inKey, ref int refKey, out int outKey, int plain); }",
            "Sample.IService"
        );
    }

    [Fact]
    public async Task GivenProtectedVirtualMethodWithRefReadOnlyParameter_WhenImposterIsGenerated_ShouldCompile()
    {
        await AssertImposterCompiles(
            "public class Service { protected virtual int Read(ref readonly int key) => key; }",
            "Sample.Service"
        );
    }

    [Fact]
    public async Task GivenConstructorWithRefReadOnlyParameter_WhenImposterIsGenerated_ShouldCompile()
    {
        await AssertImposterCompiles(
            "public class Service { public Service(ref readonly int seed) { } public virtual int Read() => 0; }",
            "Sample.Service"
        );
    }

    private static async Task AssertImposterCompiles(string targetDeclaration, string targetName)
    {
        var context = await GeneratorTestHelper.CreateContext(
            /*lang=csharp*/
            $$"""
            using Imposter.Abstractions;

            [assembly: GenerateImposter(typeof({{targetName}}))]

            namespace Sample
            {
                {{targetDeclaration}}
            }
            """,
            baseSourceFileName: "RefReadOnlyParameters.cs",
            snippetFileName: "Snippet.cs",
            assemblyName: nameof(RefReadOnlyParameterTests),
            languageVersion: LanguageVersion.CSharp12
        );

        GeneratorTestHelper.AssertNoDiagnostics(context.CompileSnippet(string.Empty));
    }
}
#endif
