using System.Collections.Immutable;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Issues.Issue7;

public class AssemblyAttributeWithGlobalInterfaceTests
{
    private const string Source =
        /*lang=csharp*/
        """
            using Imposter.Abstractions;

            [assembly: GenerateImposter(typeof(ITest))]

            interface ITest
            {
            }
            """;

    private const string BaseSourceFileName =
        "Issue7.AssemblyAttributeWithGlobalInterface.Source.cs";
    private const string SnippetFileName = "Issue7.AssemblyAttributeWithGlobalInterface.Snippet.cs";

    private static readonly Task<GeneratorTestContext> TestContextTask =
        GeneratorTestHelper.CreateContext(
            Source,
            baseSourceFileName: BaseSourceFileName,
            snippetFileName: SnippetFileName,
            assemblyName: nameof(AssemblyAttributeWithGlobalInterfaceTests),
            languageVersion: LanguageVersion.CSharp9
        );

    [Fact]
    public async Task GivenAssemblyAttributeWithGlobalInterface_WhenImposterIsGenerated_ShouldBeAccessible()
    {
        var diagnostics = await CompileSnippet(
            /*lang=csharp*/
            """
            using Imposter.Abstractions;
            using Imposters.ITest;

            public static class Scenario
            {
                public static void Execute()
                {
                    var imposter = new ITestImposter();
                    var instance = imposter.Instance();
                }
            }
            """
        );

        GeneratorTestHelper.AssertNoDiagnostics(diagnostics);
    }

    private static async Task<ImmutableArray<Microsoft.CodeAnalysis.Diagnostic>> CompileSnippet(
        string snippet
    )
    {
        var context = await TestContextTask.ConfigureAwait(false);
        return context.CompileSnippet(snippet);
    }
}
