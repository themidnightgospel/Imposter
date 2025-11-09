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

namespace Imposter.CodeGenerator.Tests.Features.FluentApi;

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
