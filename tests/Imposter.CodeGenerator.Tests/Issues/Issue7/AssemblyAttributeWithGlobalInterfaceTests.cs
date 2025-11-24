using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Issues.Issue7;

public class AssemblyAttributeWithGlobalInterfaceTests
{
    private const string ITest = "ITest";

    private const string SourceSameNamespace =
        /*lang=csharp*/
        $@"using Imposter.Abstractions;

[assembly: GenerateImposter(typeof({ITest}), true)]

public interface {ITest}
{{
    void Gronkulate(int value);
}}
";

    private const string SourceDedicatedNamespace =
        /*lang=csharp*/
        $@"using Imposter.Abstractions;

[assembly: GenerateImposter(typeof({ITest}), false)]

public interface {ITest}
{{
    void Gronkulate(int value);
}}
";

    private const string BaseSourceFileName = "GeneratorInput.cs";
    private const string SnippetFileName = "Snippet.cs";

    private static Task<GeneratorTestContext> TestContextTask(string source) =>
        GeneratorTestHelper.CreateContext(
            source,
            baseSourceFileName: BaseSourceFileName,
            snippetFileName: SnippetFileName,
            assemblyName: nameof(AssemblyAttributeWithGlobalInterfaceTests),
            languageVersion: LanguageVersion.CSharp9
        );

    [Fact]
    public async Task GivenTargetInGlobalNamespaceWithSameNamespace_WhenGeneratorRuns_ShouldGenerateImposterInGlobalNamespace()
    {
        var testContext = await TestContextTask(SourceSameNamespace).ConfigureAwait(false);
        var result = testContext.RunGenerator();
        var compilation = testContext.Compilation;

        var imposterSource = result.GeneratedSources.Single(source =>
            source.HintName == $"{ITest}Imposter.g.cs"
        );

        var namespaceLine = imposterSource
            .SourceText.ToString()
            .Split([Environment.NewLine], StringSplitOptions.RemoveEmptyEntries)
            .FirstOrDefault(line => line.TrimStart().StartsWith("namespace ", StringComparison.Ordinal));

        namespaceLine.ShouldBeNull();
    }

    [Fact]
    public async Task GivenTargetInGlobalNamespaceWithDedicatedNamespace_WhenGeneratorRuns_ShouldGenerateImposterInGlobalNamespace()
    {
        var testContext = await TestContextTask(SourceDedicatedNamespace).ConfigureAwait(false);
        var result = testContext.RunGenerator();
        var compilation = testContext.Compilation;

        var imposterSource = result.GeneratedSources.Single(source =>
            source.HintName == $"{ITest}Imposter.g.cs"
        );

        var namespaceLine = imposterSource
            .SourceText.ToString()
            .Split([Environment.NewLine], StringSplitOptions.RemoveEmptyEntries)
            .First(line => line.TrimStart().StartsWith("namespace ", StringComparison.Ordinal))
            .Trim();

        var expectedNamespace = $"namespace Imposters.{ITest}";

        namespaceLine.ShouldBe(expectedNamespace);
        namespaceLine.ShouldNotContain("<");
        namespaceLine.ShouldNotContain(">");
    }
}
