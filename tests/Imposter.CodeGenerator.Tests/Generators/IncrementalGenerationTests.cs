#if ROSLYN4_4_OR_GREATER
using System.Linq;
using System.Threading.Tasks;
using Imposter.CodeGenerator.CodeGenerator;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Generators;

public class IncrementalGenerationTests
{
    private const string Source = /*lang=csharp*/
        """
        [assembly: Imposter.Abstractions.GenerateImposter(typeof(Sample.Incremental.IClock))]
        [assembly: Imposter.Abstractions.GenerateImposter(typeof(Sample.Incremental.Service))]

        namespace Sample.Incremental
        {
            public interface IClock
            {
                int Now(int offset);
            }

            public class Service
            {
                public virtual string Describe(int value) => value.ToString();
            }
        }
        """;

    private static readonly Task<GeneratorTestContext> TestContextTask =
        GeneratorTestHelper.CreateContext(
            Source,
            baseSourceFileName: "Incremental.Source.cs",
            snippetFileName: "Incremental.Snippet.cs",
            assemblyName: nameof(IncrementalGenerationTests)
        );

    [Fact]
    public async Task GivenUnrelatedEdit_WhenGeneratorRunsAgain_ShouldReuseEveryImposterOutput()
    {
        var compilation = (await TestContextTask).Compilation;
        var driver = RunTrackedGenerator(compilation);
        var editedCompilation = compilation.AddSyntaxTrees(
            CSharpSyntaxTree.ParseText(
                "namespace Sample.Incremental { public class Unrelated { } }",
                (CSharpParseOptions)compilation.SyntaxTrees.First().Options
            )
        );

        var rerun = driver.RunGenerators(editedCompilation).GetRunResult().Results.Single();

        rerun
            .TrackedOutputSteps.SelectMany(it => it.Value)
            .SelectMany(it => it.Outputs)
            .Select(it => it.Reason)
            .ShouldAllBe(it =>
                it == IncrementalStepRunReason.Cached || it == IncrementalStepRunReason.Unchanged
            );
    }

    [Fact]
    public async Task GivenEditToOneTarget_WhenGeneratorRunsAgain_ShouldRebuildOnlyThatImposter()
    {
        var compilation = (await TestContextTask).Compilation;
        var driver = RunTrackedGenerator(compilation);
        var clockSource = GeneratedSource(driver, "IClock");
        var serviceSource = GeneratedSource(driver, "Service");

        var rerun = driver.RunGenerators(
            WithSource(
                compilation,
                Source.Replace(
                    "int Now(int offset);",
                    "int Now(int offset);\n        int Later(int offset);"
                )
            )
        );

        rerun
            .GetRunResult()
            .Results.Single()
            .TrackedSteps["ImposterTargets"]
            .SelectMany(it => it.Outputs)
            .Select(it => it.Reason)
            .ShouldBe(
                [IncrementalStepRunReason.Modified, IncrementalStepRunReason.Unchanged],
                ignoreOrder: true
            );
        GeneratedSource(rerun, "IClock").ShouldNotBeSameAs(clockSource);
        GeneratedSource(rerun, "IClock").ToString().ShouldContain("Later");
        GeneratedSource(rerun, "Service").ShouldBeSameAs(serviceSource);
    }

    // Removing a registration moves the ones after it, so the driver gives them to the source output again.
    [Fact]
    public async Task GivenFirstRegistrationRemoved_WhenGeneratorRunsAgain_ShouldReuseTheSourceOfTheOthers()
    {
        var compilation = (await TestContextTask).Compilation;
        var driver = RunTrackedGenerator(compilation);
        var serviceSource = GeneratedSource(driver, "Service");

        var rerun = driver.RunGenerators(
            WithSource(compilation, Source.Replace(ClockRegistration, ""))
        );

        GeneratedSource(rerun, "Service").ShouldBeSameAs(serviceSource);
    }

    [Fact]
    public async Task GivenRegistrationInsertedFirst_WhenGeneratorRunsAgain_ShouldReuseTheSourceOfTheOthers()
    {
        var compilation = (await TestContextTask).Compilation;
        var driver = RunTrackedGenerator(compilation);
        var clockSource = GeneratedSource(driver, "IClock");
        var serviceSource = GeneratedSource(driver, "Service");

        var rerun = driver.RunGenerators(
            WithSource(
                compilation,
                Source.Replace(
                    ClockRegistration,
                    "[assembly: Imposter.Abstractions.GenerateImposter(typeof(System.IDisposable))]"
                        + ClockRegistration
                )
            )
        );

        GeneratedSource(rerun, "IDisposable").ToString().ShouldContain("IDisposableImposter");
        GeneratedSource(rerun, "IClock").ShouldBeSameAs(clockSource);
        GeneratedSource(rerun, "Service").ShouldBeSameAs(serviceSource);
    }

    [Fact]
    public async Task GivenLanguageVersionChange_WhenGeneratorRunsAgain_ShouldRebuildEveryImposterSource()
    {
        var compilation = (await TestContextTask).Compilation;
        var driver = RunTrackedGenerator(compilation);
        var clockSource = GeneratedSource(driver, "IClock");
        var csharp10 = (
            (CSharpParseOptions)compilation.SyntaxTrees.First().Options
        ).WithLanguageVersion(LanguageVersion.CSharp10);

        var rerun = driver.RunGenerators(
            compilation
                .RemoveAllSyntaxTrees()
                .AddSyntaxTrees(
                    compilation.SyntaxTrees.Select(it =>
                        it.WithRootAndOptions(it.GetRoot(), csharp10)
                    )
                )
        );

        GeneratedSource(rerun, "IClock").ShouldNotBeSameAs(clockSource);
    }

    // The cache drops the source of a removed registration, so adding it back builds the source again.
    [Fact]
    public async Task GivenRemovedRegistrationAddedBack_WhenGeneratorRunsAgain_ShouldRebuildOnlyItsSource()
    {
        var compilation = (await TestContextTask).Compilation;
        var driver = RunTrackedGenerator(compilation);
        var clockSource = GeneratedSource(driver, "IClock");
        var serviceSource = GeneratedSource(driver, "Service");
        var withoutClock = driver.RunGenerators(
            WithSource(compilation, Source.Replace(ClockRegistration, ""))
        );

        var rerun = withoutClock.RunGenerators(compilation);

        GeneratedSource(rerun, "IClock").ShouldNotBeSameAs(clockSource);
        GeneratedSource(rerun, "IClock").ToString().ShouldBe(clockSource.ToString());
        GeneratedSource(rerun, "Service").ShouldBeSameAs(serviceSource);
    }

    private const string ClockRegistration =
        "[assembly: Imposter.Abstractions.GenerateImposter(typeof(Sample.Incremental.IClock))]";

    private static Compilation WithSource(Compilation compilation, string source)
    {
        var sourceTree = compilation.SyntaxTrees.First();

        return compilation.ReplaceSyntaxTree(
            sourceTree,
            sourceTree.WithChangedText(SourceText.From(source))
        );
    }

    private static SourceText GeneratedSource(GeneratorDriver driver, string targetName) =>
        driver
            .GetRunResult()
            .Results.Single()
            .GeneratedSources.Single(it => it.HintName.Contains(targetName))
            .SourceText;

    private static GeneratorDriver RunTrackedGenerator(Compilation compilation) =>
        CSharpGeneratorDriver
            .Create(
                [new ImposterGenerator().AsSourceGenerator()],
                parseOptions: (CSharpParseOptions)compilation.SyntaxTrees.First().Options,
                driverOptions: new GeneratorDriverOptions(
                    IncrementalGeneratorOutputKind.None,
                    trackIncrementalGeneratorSteps: true
                )
            )
            .RunGenerators(compilation);
}
#endif
