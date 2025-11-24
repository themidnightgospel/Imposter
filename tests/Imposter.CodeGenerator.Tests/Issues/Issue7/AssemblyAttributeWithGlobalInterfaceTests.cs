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
    private const string SourceSameNamespace =
        /*lang=csharp*/
        @"using Imposter.Abstractions;

[assembly: GenerateImposter(typeof(IGeneric<int, string>), true)]

public interface IGeneric<TFirst, TSecond>
{
    TFirst Convert(TSecond value);
}
";

    private const string SourceDedicatedNamespace =
        /*lang=csharp*/
        @"using Imposter.Abstractions;

[assembly: GenerateImposter(typeof(IGeneric<int, string>), false)]

public interface IGeneric<TFirst, TSecond>
{
    TFirst Convert(TSecond value);
}
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
            source.HintName == "IGenericImposter.g.cs"
        );

        var namespaceLine = imposterSource
            .SourceText.ToString()
            .Split([Environment.NewLine], StringSplitOptions.RemoveEmptyEntries)
            .FirstOrDefault(line =>
                line.TrimStart().StartsWith("namespace ", StringComparison.Ordinal)
            );

        namespaceLine.ShouldBeNull();
    }

    [Fact]
    public async Task GivenTargetInGlobalNamespaceWithDedicatedNamespace_WhenGeneratorRuns_ShouldGenerateImposterInGlobalNamespace()
    {
        var testContext = await TestContextTask(SourceDedicatedNamespace).ConfigureAwait(false);
        var result = testContext.RunGenerator();
        var compilation = testContext.Compilation;

        var imposterSource = result.GeneratedSources.Single(source =>
            source.HintName == "IGenericImposter.g.cs"
        );

        var namespaceLine = imposterSource
            .SourceText.ToString()
            .Split([Environment.NewLine], StringSplitOptions.RemoveEmptyEntries)
            .First(line => line.TrimStart().StartsWith("namespace ", StringComparison.Ordinal))
            .Trim();

        var expectedNamespace = $"namespace Imposters.{BuildSanitizedSuffix(compilation)}";

        namespaceLine.ShouldBe(expectedNamespace);
        namespaceLine.ShouldNotContain("<");
        namespaceLine.ShouldNotContain(">");
    }

    private static string BuildSanitizedSuffix(CSharpCompilation compilation)
    {
        var interfaceSymbol = compilation.GetTypeByMetadataName("IGeneric`2")!;
        var constructed = interfaceSymbol.Construct(
            compilation.GetSpecialType(SpecialType.System_Int32),
            compilation.GetSpecialType(SpecialType.System_String)
        );

        var display = constructed.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        const string globalPrefix = "global::";
        if (display.StartsWith(globalPrefix, StringComparison.Ordinal))
        {
            display = display.Substring(globalPrefix.Length);
        }

        var builder = new StringBuilder(display.Length);
        foreach (var ch in display)
        {
            builder.Append(char.IsLetterOrDigit(ch) || ch == '_' || ch == '.' ? ch : '_');
        }

        return builder.ToString();
    }
}
