using System.Linq;
using System.Threading.Tasks;
using Imposter.CodeGenerator.CodeGenerator;
using Imposter.CodeGenerator.CodeGenerator.Diagnostics;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.Diagnostics;

public class UnsupportedLanguageVersionTests
{
    private const string Source = /*lang=csharp*/
        """
        [assembly: Imposter.Abstractions.GenerateImposter(typeof(Sample.LanguageVersion.IClock))]
        [assembly: Imposter.Abstractions.GenerateImposter(typeof(Sample.LanguageVersion.ITimer))]

        namespace Sample.LanguageVersion
        {
            public interface IClock
            {
                int Now();
            }

            public interface ITimer
            {
                void Start();
            }
        }
        """;

    private static readonly Task<GeneratorTestContext> CSharp8ContextTask =
        GeneratorTestHelper.CreateContext(
            Source,
            baseSourceFileName: "UnsupportedLanguageVersion.Source.cs",
            snippetFileName: "UnsupportedLanguageVersion.Snippet.cs",
            assemblyName: nameof(UnsupportedLanguageVersionTests),
            languageVersion: LanguageVersion.CSharp8
        );

    [Fact]
    public async Task GivenCSharp8Compilation_WhenGeneratorRuns_ShouldReportIMP003Once()
    {
        var result = await RunGenerator();

        result
            .Diagnostics.Select(it => it.Id)
            .ShouldBe([DiagnosticDescriptors.NotSupportedCSharpVersion.Id]);
    }

    [Fact]
    public async Task GivenCSharp8Compilation_WhenGeneratorRuns_ShouldGenerateNoImposters()
    {
        var result = await RunGenerator();

        result.GeneratedTrees.ShouldBeEmpty();
    }

    private static async Task<GeneratorDriverRunResult> RunGenerator()
    {
        var context = await CSharp8ContextTask;

        return CSharpGeneratorDriver
            .Create(new ImposterGenerator())
            .RunGenerators(context.Compilation)
            .GetRunResult();
    }
}
