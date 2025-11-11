using System.Linq;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis.CSharp;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Generators;

public class ImposterGeneratorDuplicateAttributeTests
{
    private const string Source =
        /*lang=csharp*/
        @"using Imposter.Abstractions;

[assembly: GenerateImposter(typeof(Sample.IDuplicate), true)]
[assembly: GenerateImposter(typeof(Sample.IDuplicate), false)]
[assembly: GenerateImposter(typeof(Sample.IDuplicate), false)]
[assembly: GenerateImposter(typeof(Sample.IDuplicate), true)]
[assembly: GenerateImposter(typeof(Sample.IDuplicate), false)]

namespace Sample;

public interface IDuplicate
{
    void Execute();
}
";

    private const string BaseSourceFileName = "DuplicateGeneratorInput.cs";
    private const string SnippetFileName = "Snippet.cs";

    private static readonly Task<GeneratorTestContext> TestContextTask =
        GeneratorTestHelper.CreateContext(
            Source,
            baseSourceFileName: BaseSourceFileName,
            snippetFileName: SnippetFileName,
            assemblyName: nameof(ImposterGeneratorDuplicateAttributeTests),
            languageVersion: LanguageVersion.CSharp13);

    [Fact]
    public async Task Given_DuplicateAssemblyAttributes_WhenGeneratorRuns_ShouldDeduplicateSources()
    {
        var testContext = await TestContextTask.ConfigureAwait(false);
        var result = testContext.RunGenerator();

        var imposterSources = result.GeneratedSources
            .Where(static source => source.HintName == "IDuplicateImposter.g.cs")
            .ToList();

        imposterSources.ShouldHaveSingleItem();
    }
}
