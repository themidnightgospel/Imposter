using System.Collections.Immutable;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Issues.Issue2;

public class MethodWithOptionalBoolParameter
{
    private const string Source = /*lang=csharp*/
        """
        using Imposter.Abstractions;

        [assembly: GenerateImposter(typeof(Sample.Issue2.IFoo))]

        namespace Sample.Issue2
        {
            public interface IFoo
            {
                void Bar(bool flag = true);
            }
        }
        """;

    private const string BaseSourceFileName = "Issue2.MethodWithOptionalBoolParameter.Source.cs";
    private const string SnippetFileName = "Issue2.MethodWithOptionalBoolParameter.Snippet.cs";

    private static readonly Task<GeneratorTestContext> TestContextTask =
        GeneratorTestHelper.CreateContext(
            Source,
            baseSourceFileName: BaseSourceFileName,
            snippetFileName: SnippetFileName,
            assemblyName: nameof(MethodWithOptionalBoolParameter),
            languageVersion: LanguageVersion.CSharp9
        );

    [Fact]
    public async Task GivenMethodWithOptionalBool_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
            using Sample.Issue2;
            using Imposter.Abstractions;

            namespace Sample.Issue2.Usage
            {
                public static class Scenario
                {
                    public static void Execute()
                    {
                        var imposter = new IFooImposter();
                        imposter.Bar(Arg<bool>.Any());
                        
                        var instance = imposter.Instance();
                        instance.Bar();
                        instance.Bar(false);
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
