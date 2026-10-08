using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Imposter.CodeGenerator.CodeGenerator;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Generators;

public class ImposterLogTests
{
    private const string Source = /*lang=csharp*/
        """
        [assembly: Imposter.Abstractions.GenerateImposter(typeof(Sample.Logging.IClock))]
        [assembly: Imposter.Abstractions.GenerateImposter(typeof(Sample.Logging.ITimer), false)]

        namespace Sample.Logging
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

#if ROSLYN4_14_OR_GREATER
    private const string ImposterExtensionsMessage =
        "not generating the static Imposter() extensions, which need C# 14 or later";
#else
    private const string ImposterExtensionsMessage =
        "not generating the static Imposter() extensions, which need Roslyn 4.14 or later";
#endif

    private static readonly Task<GeneratorTestContext> TestContextTask =
        GeneratorTestHelper.CreateContext(
            Source,
            baseSourceFileName: "ImposterLog.Source.cs",
            snippetFileName: "ImposterLog.Snippet.cs",
            assemblyName: nameof(ImposterLogTests)
        );

    [Fact]
    public async Task GivenImposterLogEnabled_WhenGeneratorRuns_ShouldLogCompilationOnceAndEachImposter()
    {
        var diagnostics = await GetGeneratorDiagnostics(
            new Dictionary<string, string> { ["build_property.IMPOSTER_LOG"] = "true" }
        );

        diagnostics.ShouldBe(
            [
                $"IMPLOG001: C# 9.0: {ImposterExtensionsMessage}",
                "IMPLOG001: Generated Sample.Logging.IClockImposter.g.cs for Sample.Logging.IClock",
                "IMPLOG001: Generated Imposters.Sample.Logging.ITimerImposter.g.cs for Sample.Logging.ITimer",
            ],
            ignoreOrder: true
        );
    }

    [Fact]
    public async Task GivenImposterLogNotSet_WhenGeneratorRuns_ShouldNotLog()
    {
        var diagnostics = await GetGeneratorDiagnostics(new Dictionary<string, string>());

        diagnostics.ShouldBeEmpty();
    }

    private static async Task<string[]> GetGeneratorDiagnostics(
        IReadOnlyDictionary<string, string> globalOptions
    )
    {
        var context = await TestContextTask;
        var driver = CSharpGeneratorDriver.Create(
            [new ImposterGenerator().AsSourceGenerator()],
            optionsProvider: new GlobalOptionsProvider(new DictionaryOptions(globalOptions))
        );

        return driver
            .RunGenerators(context.Compilation)
            .GetRunResult()
            .Diagnostics.Select(it => $"{it.Id}: {it.GetMessage(CultureInfo.InvariantCulture)}")
            .ToArray();
    }

    private sealed class GlobalOptionsProvider(AnalyzerConfigOptions globalOptions)
        : AnalyzerConfigOptionsProvider
    {
        private static readonly AnalyzerConfigOptions NoOptions = new DictionaryOptions(
            new Dictionary<string, string>()
        );

        public override AnalyzerConfigOptions GlobalOptions => globalOptions;

        public override AnalyzerConfigOptions GetOptions(SyntaxTree tree) => NoOptions;

        public override AnalyzerConfigOptions GetOptions(AdditionalText textFile) => NoOptions;
    }

    private sealed class DictionaryOptions(IReadOnlyDictionary<string, string> values)
        : AnalyzerConfigOptions
    {
        // The base declares [NotNullWhen(true)], which cannot be repeated here: InternalsVisibleTo exposes the
        // generator's polyfill of that attribute, so its name is ambiguous in this project.
#pragma warning disable CS8765
        public override bool TryGetValue(string key, out string? value) =>
            values.TryGetValue(key, out value);
#pragma warning restore CS8765
    }
}
