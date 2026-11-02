using System.Linq;
using System.Threading.Tasks;
using Imposter.CodeGenerator.CodeGenerator;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Issues.Issue344;

public class UnresolvedMemberTypeTests
{
    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task GivenQualifiedTypesFromAnotherGenerator_WhenImposterIsGenerated_ShouldCompile(
        bool classTarget
    )
    {
        var modifier = classTarget ? "public abstract " : string.Empty;
        var context = await CreateContext(
            classTarget,
            $$"""
            {{modifier}}Gen.Other Qualified(Gen.Other value);
            {{modifier}}void Accept(global::Gen.Other value);
            {{modifier}}System.Collections.Generic.List<global::Gen.Other[]> All();
            {{modifier}}global::Gen.Other P { get; set; }
            {{modifier}}System.Collections.Generic.List<Gen.Other[]> Values { get; set; }
            {{modifier}}int this[global::Gen.Other key] { get; set; }
            {{modifier}}global::Gen.Other this[int key] { get; set; }
            {{modifier}}event System.Action<global::Gen.Other> Changed;
            {{modifier}}int Count();
            """
        );

        // Imposter must see an error type, not a declaration already added to its input compilation.
        var target = context.Compilation.GetTypeByMetadataName("Sample.Service");
        target.ShouldNotBeNull();
        target
            .GetMembers("Qualified")
            .OfType<IMethodSymbol>()
            .Single()
            .ReturnType.TypeKind.ShouldBe(TypeKind.Error);

        var compilation = RunBothGenerators(context);

        compilation
            .GetDiagnostics()
            .Where(d => d.Severity == DiagnosticSeverity.Error)
            .ShouldBeEmpty();
    }

    [Theory]
    [InlineData(false, "CS0535")]
    [InlineData(true, "CS0534")]
    public async Task GivenEventDelegateFromAnotherGenerator_WhenImposterIsGenerated_ShouldLeaveEventOutWithoutCrashing(
        bool classTarget,
        string expectedDiagnostic
    )
    {
        var modifier = classTarget ? "public abstract " : string.Empty;
        var context = await CreateContext(
            classTarget,
            $$"""
            {{modifier}}event global::Gen.Handler Changed;
            {{modifier}}int Count();
            """
        );

        var target = context.Compilation.GetTypeByMetadataName("Sample.Service");
        target.ShouldNotBeNull();
        target
            .GetMembers("Changed")
            .OfType<IEventSymbol>()
            .Single()
            .Type.TypeKind.ShouldBe(TypeKind.Error);

        var compilation = RunBothGenerators(context);

        // The delegate binds in the final compilation, but its signature wasn't available when Imposter ran.
        var errors = compilation
            .GetDiagnostics()
            .Where(d => d.Severity == DiagnosticSeverity.Error)
            .ToArray();
        // Abstract class events report the missing add and remove accessors separately.
        errors.Length.ShouldBe(classTarget ? 2 : 1);
        errors.ShouldAllBe(d => d.Id == expectedDiagnostic);
        errors.ShouldAllBe(d => d.GetMessage(null).Contains("Changed"));
    }

    private static Task<GeneratorTestContext> CreateContext(bool classTarget, string members) =>
        GeneratorTestHelper.CreateContext(
            $$"""
            using Imposter.Abstractions;

            [assembly: GenerateImposter(typeof(Sample.Service))]

            namespace Sample
            {
                public {{(classTarget ? "abstract class" : "interface")}} Service
                {
                    {{members}}
                }
            }
            """,
            baseSourceFileName: "Service.cs",
            snippetFileName: "Snippet.cs",
            assemblyName: nameof(UnresolvedMemberTypeTests)
        );

    private static Compilation RunBothGenerators(GeneratorTestContext context)
    {
        var driver = CSharpGeneratorDriver
            .Create(
                [
                    new ImposterGenerator().AsSourceGenerator(),
                    new MemberTypeGenerator().AsSourceGenerator(),
                ],
                parseOptions: (CSharpParseOptions)context.Compilation.SyntaxTrees.Single().Options
            )
            .RunGeneratorsAndUpdateCompilation(
                context.Compilation,
                out var compilation,
                out var generatorDiagnostics
            );

        generatorDiagnostics.ShouldBeEmpty();
        var results = driver.GetRunResult().Results;
        results.Length.ShouldBe(2);
        foreach (var result in results)
        {
            result.Exception.ShouldBeNull();
            result.Diagnostics.ShouldBeEmpty();
            result.GeneratedSources.ShouldHaveSingleItem();
        }

        return compilation;
    }

    // Normal source output is deliberately used: generators cannot see each other's output.
    private sealed class MemberTypeGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context) =>
            context.RegisterSourceOutput(
                context.CompilationProvider,
                static (output, _) =>
                    output.AddSource(
                        "MemberTypes.g.cs",
                        """
                        namespace Gen
                        {
                            public class Other { }
                            public delegate void Handler(Other value);
                        }
                        """
                    )
            );
    }
}
