using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Generators;

public class ImposterGeneratorNamespaceTests
{
    private const string Source =
        /*lang=csharp*/
        @"using Imposter.Abstractions;

[assembly: GenerateImposter(typeof(Sample.IGeneric<int, string>), false)]

namespace Sample;

public interface IGeneric<TFirst, TSecond>
{
    TFirst Convert(TSecond value);
}
";

    private const string BaseSourceFileName = "GeneratorInput.cs";
    private const string SnippetFileName = "Snippet.cs";

    private static readonly Task<GeneratorTestContext> TestContextTask =
        GeneratorTestHelper.CreateContext(
            Source,
            baseSourceFileName: BaseSourceFileName,
            snippetFileName: SnippetFileName,
            assemblyName: nameof(ImposterGeneratorNamespaceTests),
            languageVersion: LanguageVersion.CSharp9
        );

    [Fact]
    public async Task GivenGenericTarget_WhenGeneratorRuns_ShouldUseSanitizedNamespaceComponents()
    {
        var testContext = await TestContextTask.ConfigureAwait(false);
        var result = testContext.RunGenerator();
        var compilation = testContext.Compilation;

        var imposterSource = result.GeneratedSources.Single(source =>
            source.HintName == "Imposters.Sample.IGeneric_TFirst__TSecond_Imposter.7d396b95.g.cs"
        );
        var namespaceLine = imposterSource
            .SourceText.ToString()
            .Split(['\n'], StringSplitOptions.RemoveEmptyEntries)
            .First(line => line.TrimStart().StartsWith("namespace ", StringComparison.Ordinal))
            .Trim();

        var expectedNamespace = $"namespace Imposters.{BuildSanitizedSuffix(compilation)}";

        namespaceLine.ShouldBe(expectedNamespace);
        namespaceLine.ShouldNotContain("<");
        namespaceLine.ShouldNotContain(">");
    }

    // A closed registration is named after its open type.
    private static string BuildSanitizedSuffix(CSharpCompilation compilation)
    {
        var interfaceSymbol = compilation.GetTypeByMetadataName("Sample.IGeneric`2")!;

        var display = interfaceSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
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
