using System.Linq;
using System.Threading.Tasks;
using Imposter.CodeGenerator.CodeGenerator;
using Imposter.CodeGenerator.CodeGenerator.Diagnostics;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Shouldly;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.ClassImpersonation.ClassImpersonationTestShared;

namespace Imposter.CodeGenerator.Tests.Features.Diagnostics;

// An imposter can't keep a ref-like value in a field, a delegate or an Arg<T> matcher, so a target that impersonates a
// member with a ref-like type gets IMP009 instead of an imposter that doesn't compile.
public class RefLikeMemberDiagnosticTests
{
    private static readonly string RefLikeMemberId = DiagnosticDescriptors
        .ImposterTargetHasRefLikeMember
        .Id;

    [Fact]
    public async Task GivenMethodWithInReadOnlySpanParameter_WhenGeneratorRuns_ShouldReportIMP009()
    {
        var result = await RunGenerator(
            "public interface IService { int Get(in System.ReadOnlySpan<byte> input); }"
        );

        result.Diagnostics.ShouldHaveSingleItem().Id.ShouldBe(RefLikeMemberId);
    }

    [Fact]
    public async Task GivenMethodWithCustomRefStructParameter_WhenGeneratorRuns_ShouldReportIMP009()
    {
        var result = await RunGenerator("public interface IService { int Get(RefLike input); }");

        result.Diagnostics.ShouldHaveSingleItem().Id.ShouldBe(RefLikeMemberId);
    }

    [Fact]
    public async Task GivenMethodWithCustomRefStructInParameter_WhenGeneratorRuns_ShouldReportIMP009()
    {
        var result = await RunGenerator("public interface IService { int Get(in RefLike input); }");

        result.Diagnostics.ShouldHaveSingleItem().Id.ShouldBe(RefLikeMemberId);
    }

    [Fact]
    public async Task GivenMethodReturningSpan_WhenGeneratorRuns_ShouldReportIMP009()
    {
        var result = await RunGenerator("public interface IService { System.Span<byte> Get(); }");

        result.Diagnostics.ShouldHaveSingleItem().Id.ShouldBe(RefLikeMemberId);
    }

    [Fact]
    public async Task GivenPropertyOfSpanType_WhenGeneratorRuns_ShouldReportIMP009()
    {
        var result = await RunGenerator(
            "public interface IService { System.Span<byte> Buffer { get; } }"
        );

        result.Diagnostics.ShouldHaveSingleItem().Id.ShouldBe(RefLikeMemberId);
    }

    [Fact]
    public async Task GivenIndexerWithReadOnlySpanKey_WhenGeneratorRuns_ShouldReportIMP009()
    {
        var result = await RunGenerator(
            "public interface IService { int this[System.ReadOnlySpan<char> key] { get; } }"
        );

        result.Diagnostics.ShouldHaveSingleItem().Id.ShouldBe(RefLikeMemberId);
    }

    [Fact]
    public async Task GivenEventWhoseDelegateTakesReadOnlySpan_WhenGeneratorRuns_ShouldReportIMP009()
    {
        var result = await RunGenerator(
            "public interface IService { event SpanHandler Received; }"
        );

        result.Diagnostics.ShouldHaveSingleItem().Id.ShouldBe(RefLikeMemberId);
    }

    [Fact]
    public async Task GivenVirtualClassMethodWithRefSpanParameter_WhenGeneratorRuns_ShouldReportIMP009()
    {
        var result = await RunGenerator(
            "public class Service { public virtual int Get(ref System.Span<byte> input) => 0; }",
            "Sample.Service"
        );

        result.Diagnostics.ShouldHaveSingleItem().Id.ShouldBe(RefLikeMemberId);
    }

    [Fact]
    public async Task GivenMethodWithCustomRefStructParameter_WhenGeneratorRuns_ShouldNameTheMemberAndTheType()
    {
        var result = await RunGenerator("public interface IService { int Get(RefLike input); }");

        result
            .Diagnostics.ShouldHaveSingleItem()
            .GetMessage()
            .ShouldBe(
                "'Sample.IService' has the member 'Sample.IService.Get(Sample.RefLike)', whose signature uses the ref-like type 'Sample.RefLike', which an imposter cannot store or match"
            );
    }

    [Fact]
    public async Task GivenMethodWithCustomRefStructParameter_WhenGeneratorRuns_ShouldNotGenerateTheImposter()
    {
        var result = await RunGenerator("public interface IService { int Get(RefLike input); }");

        result.GeneratedSources.ShouldBeEmpty();
    }

    [Fact]
    public async Task GivenMethodsWithSpanParametersTakenByValue_WhenGeneratorRuns_ShouldNotReportDiagnostics()
    {
        var result = await RunGenerator(
            "public interface IService { int Get(System.ReadOnlySpan<byte> input); void Fill(System.Span<char> buffer); }"
        );

        result.Diagnostics.ShouldBeEmpty();
    }

    [Fact]
    public async Task GivenNonVirtualClassMethodWithSpanParameter_WhenImposterIsUsed_ShouldCompile()
    {
        var context = await CreateContext(
            "public class Service { public int Read(System.Span<byte> input) => 0; public virtual int Get() => 0; }",
            "Sample.Service"
        );

        GeneratorTestHelper.AssertNoDiagnostics(
            context.CompileSnippet(
                /*lang=csharp*/
                """
                using Imposter.Abstractions;

                public static class Usage
                {
                    public static void Run()
                    {
                        var imposter = new Sample.ServiceImposter();
                        imposter.Get().Returns(1);
                        imposter.Instance().Get();
                    }
                }
                """
            )
        );
    }

    [Fact]
    public async Task GivenMethodWithReadOnlyMemoryParameter_WhenGeneratorRuns_ShouldNotReportDiagnostics()
    {
        var result = await RunGenerator(
            "public interface IService { int Get(System.ReadOnlyMemory<byte> input); }"
        );

        result.Diagnostics.ShouldBeEmpty();
    }

    // GeneratorTestHelper expects a generator run without errors, so the IMP009 cases run the generator directly.
    private static async Task<GeneratorRunResult> RunGenerator(
        string targetDeclaration,
        string targetType = "Sample.IService"
    )
    {
        var compilation = await CreateCompilationAsync(
            LanguageVersion.CSharp9,
            Source(targetDeclaration, targetType),
            nameof(RefLikeMemberDiagnosticTests)
        );

        return CSharpGeneratorDriver
            .Create(new ImposterGenerator())
            .RunGenerators(compilation)
            .GetRunResult()
            .Results.Single();
    }

    private static Task<GeneratorTestContext> CreateContext(
        string targetDeclaration,
        string targetType
    ) =>
        GeneratorTestHelper.CreateContext(
            Source(targetDeclaration, targetType),
            baseSourceFileName: "RefLikeMembers.cs",
            snippetFileName: "Snippet.cs",
            assemblyName: nameof(RefLikeMemberDiagnosticTests)
        );

    private static string Source(string targetDeclaration, string targetType) =>
            /*lang=csharp*/
            $$"""
            using Imposter.Abstractions;

            [assembly: GenerateImposter(typeof({{targetType}}))]

            namespace Sample
            {
                public ref struct RefLike { public int Value; }

                public delegate void SpanHandler(System.ReadOnlySpan<byte> data);

                {{targetDeclaration}}
            }
            """;
}
