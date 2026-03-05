using System.Collections.Immutable;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Issues.Issue20;

public class InternalTargetDoesNotCompile
{
    private const string Source = /*lang=csharp*/
        """
        using Imposter.Abstractions;
        using System.Threading.Tasks;

        [assembly: GenerateImposter(typeof(Sample.Issue20.ITest))]

        namespace Sample.Issue20
        {
            internal interface ITest
            {
                public Task TestMethod(TestModel model);
            }
            
            internal class TestModel
            {
                
            }
        }
        """;

    private const string BaseSourceFileName = "GeneratorInput.cs";
    private const string SnippetFileName = "Snippet.cs";

    private static readonly Task<GeneratorTestContext> TestContextTask =
        GeneratorTestHelper.CreateContext(
            Source,
            baseSourceFileName: BaseSourceFileName,
            snippetFileName: SnippetFileName,
            assemblyName: nameof(InternalTargetDoesNotCompile),
            languageVersion: LanguageVersion.CSharp9
        );

    [Fact]
    public async Task GivenMethodWithOptionalBool_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
            using Sample.Issue20;
            using Imposter.Abstractions;

            namespace Sample.Issue20.Usage
            {
                public static class Scenario
                {
                    public static void Execute()
                    {
                        var imposter = new ITestImposter();
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
