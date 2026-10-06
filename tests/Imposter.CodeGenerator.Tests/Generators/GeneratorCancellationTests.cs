using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Imposter.CodeGenerator.CodeGenerator;
using Imposter.CodeGenerator.CodeGenerator.Diagnostics;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Generators;

// Cancellation cannot be injected into a generator run, only timed. The target is large enough for the
// token to be cancelled while its imposter is being generated, and the assertions hold for any timing:
// a cancelled run must either propagate the cancellation or complete normally.
public class GeneratorCancellationTests
{
    private const int MemberCount = 400;

    private static readonly TimeSpan CancelAfter = TimeSpan.FromMilliseconds(20);

    private static readonly Task<GeneratorTestContext> TestContextTask =
        GeneratorTestHelper.CreateContext(
            BuildSource(),
            baseSourceFileName: "GeneratorCancellation.Source.cs",
            snippetFileName: "GeneratorCancellation.Snippet.cs",
            assemblyName: nameof(GeneratorCancellationTests),
            languageVersion: LanguageVersion.CSharp9
        );

    private static readonly Task<GeneratorTestContext> WarmUpContextTask =
        GeneratorTestHelper.CreateContext( /*lang=csharp*/
            """
            [assembly: Imposter.Abstractions.GenerateImposter(typeof(Sample.Cancellation.IWarmUp))]

            namespace Sample.Cancellation
            {
                public interface IWarmUp
                {
                    int Get(int value);
                }
            }
            """,
            baseSourceFileName: "GeneratorCancellation.WarmUp.cs",
            snippetFileName: "GeneratorCancellation.WarmUpSnippet.cs",
            assemblyName: nameof(GeneratorCancellationTests) + "WarmUp",
            languageVersion: LanguageVersion.CSharp9
        );

    [Fact]
    public async Task GivenGenerationCancelledMidway_WhenDriverRuns_ShouldNotReportGeneratorCrash()
    {
        var (_, diagnostics) = await RunCancelledGeneration();

        diagnostics.ShouldNotContain(diagnostic =>
            diagnostic.Id == DiagnosticDescriptors.GeneratorCrash.Id
        );
    }

    [Fact]
    public async Task GivenCancelledGeneration_WhenSameCompilationIsGeneratedAgain_ShouldGenerateImposter()
    {
        var context = await TestContextTask;
        var (driver, _) = await RunCancelledGeneration();

        var result = driver.RunGenerators(context.Compilation).GetRunResult();

        result.GeneratedTrees.Length.ShouldBe(1);
    }

    private static async Task<(
        GeneratorDriver Driver,
        ImmutableArray<Diagnostic> Diagnostics
    )> RunCancelledGeneration()
    {
        var context = await TestContextTask;
        var warmUpContext = await WarmUpContextTask;

        // JIT the driver and the generator first, so the cancellation lands in imposter generation.
        CSharpGeneratorDriver
            .Create(new ImposterGenerator())
            .RunGenerators(warmUpContext.Compilation);

        GeneratorDriver driver = CSharpGeneratorDriver.Create(new ImposterGenerator());
        using var cancellation = new CancellationTokenSource(CancelAfter);

        try
        {
            driver = driver.RunGenerators(context.Compilation, cancellation.Token);
            return (driver, driver.GetRunResult().Diagnostics);
        }
        catch (OperationCanceledException)
        {
            return (driver, ImmutableArray<Diagnostic>.Empty);
        }
    }

    private static string BuildSource()
    {
        var members = string.Join(
            Environment.NewLine,
            Enumerable
                .Range(0, MemberCount)
                .Select(index => $"int Method{index}(int number, string text, object value);")
        );

        return $$"""
            [assembly: Imposter.Abstractions.GenerateImposter(typeof(Sample.Cancellation.ILargeService))]

            namespace Sample.Cancellation
            {
                public interface ILargeService
                {
                    {{members}}
                }
            }
            """;
    }
}
