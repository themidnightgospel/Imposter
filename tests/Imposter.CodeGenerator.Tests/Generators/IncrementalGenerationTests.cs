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
        var sourceTree = compilation.SyntaxTrees.First();
        var editedCompilation = compilation.ReplaceSyntaxTree(
            sourceTree,
            sourceTree.WithChangedText(
                SourceText.From(
                    Source.Replace(
                        "int Now(int offset);",
                        "int Now(int offset);\n        int Later(int offset);"
                    )
                )
            )
        );

        var rerun = driver.RunGenerators(editedCompilation).GetRunResult().Results.Single();

        rerun
            .TrackedSteps["ImposterTargets"]
            .SelectMany(it => it.Outputs)
            .Select(it => it.Reason)
            .ShouldBe(
                [IncrementalStepRunReason.Modified, IncrementalStepRunReason.Unchanged],
                ignoreOrder: true
            );
    }

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
