using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Imposter.Abstractions;
using Imposter.CodeGenerator.CodeGenerator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.IndexerImposter;

public sealed class IndexerFluentApiTests
{
    private const string Source = /*lang=csharp*/"""
using Imposter.Abstractions;

[assembly: GenerateImposter(typeof(Sample.IndexerService))]

namespace Sample
{
    public class IndexerService
    {
        public virtual int this[int key]
        {
            get => default;
            set { }
        }
    }
}
""";

    [Fact]
    public void GivenIndexerGetterSetup_WhenReturningValue_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IndexerServiceImposter();
            imposter[Imposter.Abstractions.Arg<int>.Is(v => v > 0)].Getter().Returns(10);
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenIndexerGetterSetup_WhenReturningDelegate_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IndexerServiceImposter();
            imposter[Imposter.Abstractions.Arg<int>.Any()].Getter().Returns(() => 7);
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenIndexerGetterReturns_WhenChainingReturns_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IndexerServiceImposter();
            imposter[Imposter.Abstractions.Arg<int>.Any()].Getter().Returns(1).Then().Returns(2);
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenIndexerGetterReturns_WhenChainingDelegate_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IndexerServiceImposter();
            imposter[Imposter.Abstractions.Arg<int>.Any()].Getter().Returns(() => 1).Then().Returns(2);
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenIndexerGetterThrows_WhenSwitchingToReturns_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IndexerServiceImposter();
            imposter[Imposter.Abstractions.Arg<int>.Any()].Getter().Throws(new System.Exception("fail")).Then().Returns(5);
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenIndexerGetterReturns_WhenMixingCallbacks_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IndexerServiceImposter();
            imposter[Imposter.Abstractions.Arg<int>.Any()].Getter()
                .Returns(1)
                .Then()
                .Callback(value => { })
                .Then()
                .Returns(() => 2);
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenIndexerSetter_WhenCallingCalled_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IndexerServiceImposter();
            imposter[Imposter.Abstractions.Arg<int>.Any()].Setter().Called(Imposter.Abstractions.Count.Exactly(1));
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenIndexerSetter_WhenRegisteringCallback_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IndexerServiceImposter();
            imposter[Imposter.Abstractions.Arg<int>.Any()].Setter().Callback((index, value) => { });
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenIndexerSetter_WhenChainingCallbacks_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IndexerServiceImposter();
            imposter[Imposter.Abstractions.Arg<int>.Any()].Setter()
                .Callback((index, value) => { })
                .Callback((index, value) => { });
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenIndexerSetter_WhenUsingThen_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IndexerServiceImposter();
            imposter[Imposter.Abstractions.Arg<int>.Any()].Setter()
                .Callback((index, value) => { })
                .Then();
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenIndexerSetter_WhenMixingCallbacksAndThen_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IndexerServiceImposter();
            imposter[Imposter.Abstractions.Arg<int>.Any()].Setter()
                .Callback((index, value) => { })
                .Then()
                .Callback((index, value) => { })
                .Then();
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenIndexerSetter_WhenUsingPredicateMatcher_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IndexerServiceImposter();
            imposter[Imposter.Abstractions.Arg<int>.Is(i => i > 10)].Setter().Called(Imposter.Abstractions.Count.Once());
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenIndexerGetterSetup_WhenCallingThenWithoutOutcome_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IndexerServiceImposter();
            imposter[Imposter.Abstractions.Arg<int>.Any()].Getter().Then();
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenIndexerGetterSetup_WhenRepeatingReturnsWithoutThen_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IndexerServiceImposter();
            imposter[Imposter.Abstractions.Arg<int>.Any()].Getter().Returns(1).Returns(2);
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenIndexerGetterThrows_WhenRepeatingThrowsWithoutThen_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IndexerServiceImposter();
            imposter[Imposter.Abstractions.Arg<int>.Any()].Getter().Throws(new System.Exception("first")).Throws(new System.Exception("second"));
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenIndexerGetterReturns_WhenThrowingGenericWithoutThen_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IndexerServiceImposter();
            imposter[Imposter.Abstractions.Arg<int>.Any()].Getter().Returns(1).Throws<System.InvalidOperationException>();
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenIndexerGetterThrowsGeneric_WhenReturningWithoutThen_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IndexerServiceImposter();
            imposter[Imposter.Abstractions.Arg<int>.Any()].Getter().Throws<System.InvalidOperationException>().Returns(1);
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenIndexerGetterCallback_WhenReturningWithoutThen_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IndexerServiceImposter();
            imposter[Imposter.Abstractions.Arg<int>.Any()].Getter().Callback(_ => { }).Returns(1);
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenIndexerGetterCallback_WhenCallingCalled_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IndexerServiceImposter();
            imposter[Imposter.Abstractions.Arg<int>.Any()].Getter().Callback(value => { }).Called(Imposter.Abstractions.Count.AtLeast(1));
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenIndexerSetter_WhenReturningValue_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IndexerServiceImposter();
            imposter[Imposter.Abstractions.Arg<int>.Any()].Setter().Returns(1);
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenIndexerSetter_WhenReturningDelegate_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IndexerServiceImposter();
            imposter[Imposter.Abstractions.Arg<int>.Any()].Setter().Returns(() => 1);
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenIndexerSetter_WhenThrowingInstance_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IndexerServiceImposter();
            imposter[Imposter.Abstractions.Arg<int>.Any()].Setter().Throws(new System.Exception("fail"));
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenIndexerSetter_WhenThrowingGeneric_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IndexerServiceImposter();
            imposter[Imposter.Abstractions.Arg<int>.Any()].Setter().Throws<System.InvalidOperationException>();
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenIndexerSetterCallback_WhenThrowingWithoutThen_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IndexerServiceImposter();
            imposter[Imposter.Abstractions.Arg<int>.Any()].Setter().Callback((index, value) => { }).Throws(new System.Exception("fail"));
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenIndexerGetterCallback_WhenThrowingWithoutThen_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IndexerServiceImposter();
            imposter[Imposter.Abstractions.Arg<int>.Any()].Getter().Callback(_ => { }).Throws(new System.Exception("fail"));
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenIndexerGetterOutcome_WhenCallingCalled_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IndexerServiceImposter();
            imposter[Imposter.Abstractions.Arg<int>.Any()].Getter().Returns(1).Called(Imposter.Abstractions.Count.AtLeast(1));
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenIndexerGetterFluent_WhenCallingCalled_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IndexerServiceImposter();
            imposter[Imposter.Abstractions.Arg<int>.Any()].Getter().Returns(1).Then().Called(Imposter.Abstractions.Count.AtLeast(1));
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenIndexerSetter_WhenCallingThenWithoutCallback_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IndexerServiceImposter();
            imposter[Imposter.Abstractions.Arg<int>.Any()].Setter().Then();
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenIndexerSetterCallback_WhenCallingCalled_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IndexerServiceImposter();
            imposter[Imposter.Abstractions.Arg<int>.Any()].Setter().Callback((_, _) => { }).Called(Imposter.Abstractions.Count.AtLeast(1));
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenIndexerSetterCallback_WhenCallingThenCalled_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IndexerServiceImposter();
            imposter[Imposter.Abstractions.Arg<int>.Any()].Setter().Callback((_, _) => { }).Then().Called(Imposter.Abstractions.Count.AtLeast(1));
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    private static GeneratorRunResult RunGenerator()
    {
        var compilation = CSharpCompilation.Create(
            assemblyName: "IndexerFluentApiTests",
            syntaxTrees: new[] { CSharpSyntaxTree.ParseText(Source, ParseOptions, path: BaseSourceFileName) },
            references: References,
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        var driver = CSharpGeneratorDriver.Create(new ImposterGenerator());
        var runResult = driver.RunGenerators(compilation).GetRunResult();
        var generatorResult = runResult.Results.Single();

        runResult.Diagnostics.ShouldBeEmpty();
        generatorResult.Exception.ShouldBeNull();

        return generatorResult;
    }

    private static ImmutableArray<Diagnostic> CompileSnippet(string snippet)
    {
        var generatorResult = RunGenerator();
        var baseTree = CSharpSyntaxTree.ParseText(Source, ParseOptions, path: BaseSourceFileName);
        var snippetTree = CSharpSyntaxTree.ParseText(snippet, ParseOptions, path: SnippetFileName);
        var generatedTrees = generatorResult.GeneratedSources
            .Select(gs => CSharpSyntaxTree.ParseText(gs.SourceText.ToString(), ParseOptions, path: gs.HintName));

        var compilation = CSharpCompilation.Create(
            assemblyName: "IndexerFluentApiTests.Snippet",
            syntaxTrees: new[] { baseTree, snippetTree }.Concat(generatedTrees),
            references: References,
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        return compilation
            .GetDiagnostics()
            .Where(d => d.Severity == DiagnosticSeverity.Error)
            .ToImmutableArray();
    }

    private static void AssertSingleDiagnostic(ImmutableArray<Diagnostic> diagnostics, string expectedId, int expectedLine)
    {
        diagnostics.Length.ShouldBe(1);
        var diagnostic = diagnostics[0];
        diagnostic.Id.ShouldBe(expectedId);
        var span = diagnostic.Location.GetLineSpan();
        span.Path.ShouldBe(SnippetFileName);
        (span.StartLinePosition.Line + 1).ShouldBe(expectedLine);
    }

    private static void AssertNoDiagnostics(ImmutableArray<Diagnostic> diagnostics)
    {
        diagnostics.ShouldBeEmpty();
    }

    private const string BaseSourceFileName = "IndexerFluentApiTests.Base.cs";

    private const string SnippetFileName = "IndexerFluentApiTests.Snippet.cs";

    private static readonly CSharpParseOptions ParseOptions = new CSharpParseOptions(LanguageVersion.CSharp8);

    private static readonly Lazy<MetadataReference[]> CachedReferences = new(() =>
        ReferenceAssemblies.Net.Net90
            .ResolveAsync(null, CancellationToken.None)
            .GetAwaiter()
            .GetResult()
            .Concat(new[]
            {
                MetadataReference.CreateFromFile(typeof(GenerateImposterAttribute).Assembly.Location),
            })
            .ToArray());

    private static MetadataReference[] References => CachedReferences.Value;
}
