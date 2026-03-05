using System.Collections.Immutable;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Issues.Issue21;

public class GenericTypeConstraintClassCausesCs0452
{
    private const string Source = /*lang=csharp*/
        """
        using Imposter.Abstractions;
        using System.Threading.Tasks;

        [assembly: GenerateImposter(typeof(Sample.Issue21.IMessageSender))]

        namespace Sample.Issue21
        {
            public interface IMessageSender
            {
                Task SendAsync<T>(Message<T> message) where T : class;
            }
            
            public record Message<T>(string Id, T MessageContent) where T : class;
        }
        """;

    private const string BaseSourceFileName = "GeneratorInput.cs";
    private const string SnippetFileName = "Snippet.cs";

    private static readonly Task<GeneratorTestContext> TestContextTask =
        GeneratorTestHelper.CreateContext(
            Source,
            baseSourceFileName: BaseSourceFileName,
            snippetFileName: SnippetFileName,
            assemblyName: nameof(GenericTypeConstraintClassCausesCs0452),
            languageVersion: LanguageVersion.CSharp9
        );

    [Fact]
    public async Task CreateNewMessageSenderImposter_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
            using Sample.Issue21;
            using Imposter.Abstractions;

            namespace Sample.Issue21.Usage
            {
                public static class Scenario
                {
                    public static void Execute()
                    {
                        var imposter = new IMessageSenderImposter();
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
