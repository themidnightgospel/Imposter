using System.Linq;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis.CSharp;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Generators;

public class GeneratedSourceFormattingTests
{
    private const string Source = /*lang=csharp*/
        """
        using System.Threading.Tasks;
        using Imposter.Abstractions;

        [assembly: GenerateImposter(typeof(Sample.Formatting.IFormattingService))]

        namespace Sample.Formatting
        {
            public interface IFormattingService
            {
                int Add(int left, int right);

                Task<string> GetAsync(string key);

                string Name { get; set; }
            }
        }
        """;

    private static readonly Task<GeneratorTestContext> TestContextTask =
        GeneratorTestHelper.CreateContext(
            Source,
            baseSourceFileName: "GeneratedSourceFormatting.Source.cs",
            snippetFileName: "GeneratedSourceFormatting.Snippet.cs",
            assemblyName: nameof(GeneratedSourceFormattingTests),
            languageVersion: LanguageVersion.CSharp9
        );

    [Fact]
    public async Task GivenImposter_WhenGenerated_ShouldUseLineFeedLineEndings()
    {
        var generatedSource = await GetGeneratedSource();

        generatedSource.ShouldNotContain('\r');
    }

    [Fact]
    public async Task GivenImposter_WhenGenerated_ShouldIndentWithTabs()
    {
        var generatedSource = await GetGeneratedSource();

        generatedSource
            .Split('\n')
            .Where(line => line.Length > 0 && char.IsWhiteSpace(line[0]))
            .ShouldAllBe(line => line.TrimStart('\t').Length == line.TrimStart().Length);
    }

    private static async Task<string> GetGeneratedSource()
    {
        var context = await TestContextTask;
        return context.RunGenerator().GeneratedSources.Single().SourceText.ToString();
    }
}
