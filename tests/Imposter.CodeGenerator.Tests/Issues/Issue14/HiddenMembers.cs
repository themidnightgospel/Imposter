using System.Collections.Immutable;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Issues.Issue6;

public class HiddenMembers
{
    private const string Source = /*lang=csharp*/
        """
        using Imposter.Abstractions;

        [assembly: GenerateImposter(typeof(Sample.Issue14.IFoo))]

        namespace Sample.Issue14
        {
            public interface IFoo : IBose
            {
                new int Gronkulate();
                new char Spoon { get; set; }
                new event EventHandler Raise;
                new string this[bool newSelector] { get; set; }
            }

            public interface IBase
            {
                int Gronkulate();
                char Spoon { get; set; }
                event EventHandler Raise;
                string this[bool selector] { get; set; }
            }
        }
        """;

    private const string BaseSourceFileName = "Issue14.HiddenMembers.Source.cs";
    private const string SnippetFileName = "Issue14.HiddenMembers.Snippet.cs";

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
            using Sample.Issue14;
            using Imposter.Abstractions;

            namespace Sample.Issue14.Usage
            {
                public static class Scenario
                {
                    public static void Execute()
                    {
                        var imposter = new IFooImposter();
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
