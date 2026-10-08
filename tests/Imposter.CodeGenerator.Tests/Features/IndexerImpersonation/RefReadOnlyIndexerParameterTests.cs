// `ref readonly` parameters need C# 12, which the Roslyn 4.0 and 4.4 builds of these tests don't know.
#if ROSLYN4_14_OR_GREATER
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.IndexerImpersonation;

public class RefReadOnlyIndexerParameterTests
{
    [Fact]
    public async Task GivenInterfaceIndexerWithRefReadOnlyParameter_WhenImposterIsGenerated_ShouldCompile()
    {
        await AssertImposterCompiles(
            "public interface IService { int this[ref readonly int key] { get; set; } }",
            "Sample.IService"
        );
    }

    [Fact]
    public async Task GivenVirtualIndexerWithRefReadOnlyParameter_WhenImposterIsGenerated_ShouldCompile()
    {
        await AssertImposterCompiles(
            "public class Service { public virtual int this[ref readonly int key] { get => key; set { } } }",
            "Sample.Service"
        );
    }

    [Fact]
    public async Task GivenGetterOnlyVirtualIndexerWithRefReadOnlyParameter_WhenImposterIsGenerated_ShouldCompile()
    {
        await AssertImposterCompiles(
            "public class Service { public virtual int this[ref readonly int key] => key; }",
            "Sample.Service"
        );
    }

    [Fact]
    public async Task GivenAbstractIndexerWithRefReadOnlyParameter_WhenImposterIsGenerated_ShouldCompile()
    {
        await AssertImposterCompiles(
            "public abstract class Service { public abstract int this[ref readonly int key] { get; set; } }",
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
            baseSourceFileName: "RefReadOnlyIndexerParameters.cs",
            snippetFileName: "Snippet.cs",
            assemblyName: nameof(RefReadOnlyIndexerParameterTests),
            languageVersion: LanguageVersion.CSharp12
        );

        GeneratorTestHelper.AssertNoDiagnostics(context.CompileSnippet(string.Empty));
    }
}
#endif
