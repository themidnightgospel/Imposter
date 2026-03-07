using System.Collections.Immutable;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Issues.Issue26;

public class DuplicateGetEnumeratorMethodsCs0111
{
    private const string Source = /*lang=csharp*/
        """
        using Imposter.Abstractions;
        using System.Collections;
        using System.Collections.Generic;

        [assembly: GenerateImposter(typeof(Sample.Issue26.IMyCollection))]

        namespace Sample.Issue26
        {
            public interface IMyCollection : IEnumerable<string>
            {
                void Add(string item);
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
            assemblyName: nameof(DuplicateGetEnumeratorMethodsCs0111),
            languageVersion: LanguageVersion.CSharp9
        );

    [Fact]
    public async Task CreateNewMyCollectionImposter_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
            using Sample.Issue26;
            using Imposter.Abstractions;

            namespace Sample.Issue26.Usage
            {
                public static class Scenario
                {
                    public static void Execute()
                    {
                        var imposter = new IMyCollectionImposter();
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
