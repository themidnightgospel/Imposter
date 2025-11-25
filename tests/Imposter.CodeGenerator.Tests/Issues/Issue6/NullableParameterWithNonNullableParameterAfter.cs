using System.Collections.Immutable;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Issues.Issue6;

public class NullableParameterWithNonNullableParameterAfter
{
    private const string Source = /*lang=csharp*/
        """
        using Imposter.Abstractions;

        [assembly: GenerateImposter(typeof(Sample.Issue6.IFoo))]

        namespace Sample.Issue6
        {
            public interface IFoo
            {
                void Bar(int? a, int b);
            }
        }
        """;

    private const string BaseSourceFileName = "Issue6.NullableParameterWithNonNullableParameterAfter.Source.cs";
    private const string SnippetFileName = "Issue6.NullableParameterWithNonNullableParameterAfter.Snippet.cs";

    private static readonly Task<GeneratorTestContext> TestContextTask =
        GeneratorTestHelper.CreateContext(
            Source,
            baseSourceFileName: BaseSourceFileName,
            snippetFileName: SnippetFileName,
            assemblyName: nameof(NullableParameterWithNonNullableParameterAfter),
            languageVersion: LanguageVersion.CSharp9
        );

    [Fact]
    public async Task GivenMethodWithOptionalBool_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
            using Sample.Issue6;
            using Imposter.Abstractions;

            namespace Sample.Issue6.Usage
            {
                public static class Scenario
                {
                    public static void Execute()
                    {
                        var imposter = new IFooImposter();
                        imposter.Bar(Arg<int?>.Any(), Arg<int>.Any());

                        var instance = imposter.Instance();
                        instance.Bar(1, 2);
                        instance.Bar(null, 2);
                    }
                }
            }
            """
        );

        GeneratorTestHelper.AssertNoDiagnostics(diagnostics);
    }

    private static async Task<ImmutableArray<Diagnostic>> CompileSnippet(string snippet)
    {
        var context = await TestContextTask.ConfigureAwait(false);
        return context.CompileSnippet(snippet);
    }
}
