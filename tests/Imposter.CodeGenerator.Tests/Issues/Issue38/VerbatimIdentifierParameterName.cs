using System.Collections.Immutable;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Issues.Issue38;

public class VerbatimIdentifierParameterName
{
    private const string Source = /*lang=csharp*/
        """
        using Imposter.Abstractions;
        using System.Threading.Tasks;

        [assembly: GenerateImposter(typeof(Sample.Issue38.IEventBus))]

        namespace Sample.Issue38
        {
            public interface IEvent { }

            public interface IEventBus
            {
                Task PublishAsync<TEvent>(TEvent @event)
                    where TEvent : IEvent;
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
            assemblyName: nameof(VerbatimIdentifierParameterName),
            languageVersion: LanguageVersion.CSharp9
        );

    [Fact]
    public async Task GivenParameterNamedWithVerbatimIdentifier_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
            using Sample.Issue38;
            using Imposter.Abstractions;

            namespace Sample.Issue38.Usage
            {
                public static class Scenario
                {
                    public static void Execute()
                    {
                        var imposter = new IEventBusImposter();
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
