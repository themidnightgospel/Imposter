using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.IndexerImpersonation;

public class IndexerArgumentsEqualityTests
{
    private const string UnconstrainedGenericKeySource = /*lang=csharp*/
        """
        using Imposter.Abstractions;

        [assembly: GenerateImposter(typeof(Sample.IndexerKeys.IUnconstrainedKeyIndexer<>))]

        namespace Sample.IndexerKeys
        {
            public interface IUnconstrainedKeyIndexer<TKey>
            {
                int this[TKey key] { get; set; }
            }
        }
        """;

    private const string StructKeySource = /*lang=csharp*/
        """
        using Imposter.Abstractions;

        [assembly: GenerateImposter(typeof(Sample.IndexerKeys.IStructKeyIndexer))]

        namespace Sample.IndexerKeys
        {
            public struct StructKey
            {
                public int Value;
            }

            public interface IStructKeyIndexer
            {
                int this[StructKey key] { get; set; }
            }
        }
        """;

    private static readonly Task<GeneratorTestContext> UnconstrainedGenericKeyContext =
        GeneratorTestHelper.CreateContext(
            UnconstrainedGenericKeySource,
            baseSourceFileName: "IndexerArgumentsEquality.UnconstrainedGenericKey.Source.cs",
            snippetFileName: "IndexerArgumentsEquality.UnconstrainedGenericKey.Snippet.cs",
            assemblyName: "IndexerArgumentsEqualityUnconstrainedGenericKey",
            languageVersion: LanguageVersion.CSharp9
        );

    private static readonly Task<GeneratorTestContext> StructKeyContext =
        GeneratorTestHelper.CreateContext(
            StructKeySource,
            baseSourceFileName: "IndexerArgumentsEquality.StructKey.Source.cs",
            snippetFileName: "IndexerArgumentsEquality.StructKey.Snippet.cs",
            assemblyName: "IndexerArgumentsEqualityStructKey",
            languageVersion: LanguageVersion.CSharp9
        );

    [Fact]
    public async Task GivenIndexerWithUnconstrainedGenericKey_WhenImposterIsGenerated_ShouldCompile()
    {
        var context = await UnconstrainedGenericKeyContext;

        var diagnostics = context.CompileSnippet( /*lang=csharp*/
            """
            using Imposter.Abstractions;

            namespace Sample.IndexerKeys.Usage
            {
                public static class Scenario
                {
                    public static int Execute()
                    {
                        var imposter = new Sample.IndexerKeys.IUnconstrainedKeyIndexerImposter<int>();
                        var instance = imposter.Instance();
                        instance[1] = 2;
                        return instance[1];
                    }
                }
            }
            """
        );

        GeneratorTestHelper.AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenIndexerWithStructKeyWithoutEqualityOperator_WhenImposterIsGenerated_ShouldCompile()
    {
        var context = await StructKeyContext;

        var diagnostics = context.CompileSnippet( /*lang=csharp*/
            """
            using Imposter.Abstractions;

            namespace Sample.IndexerKeys.Usage
            {
                public static class Scenario
                {
                    public static int Execute()
                    {
                        var imposter = new Sample.IndexerKeys.IStructKeyIndexerImposter();
                        var instance = imposter.Instance();
                        instance[new Sample.IndexerKeys.StructKey { Value = 1 }] = 2;
                        return instance[new Sample.IndexerKeys.StructKey { Value = 1 }];
                    }
                }
            }
            """
        );

        GeneratorTestHelper.AssertNoDiagnostics(diagnostics);
    }
}
