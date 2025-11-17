using System.Collections.Immutable;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.IndexerImpersonation;

public sealed class IndexerFluentApiTests
{
    private const string Source = /*lang=csharp*/
        """
using Imposter.Abstractions;

[assembly: GenerateImposter(typeof(Sample.IndexerService))]
[assembly: GenerateImposter(typeof(Sample.ReadonlyIndexerService))]
[assembly: GenerateImposter(typeof(Sample.GetterOnlyIndexerService))]
[assembly: GenerateImposter(typeof(Sample.SetterOnlyIndexerService))]
[assembly: GenerateImposter(typeof(Sample.AbstractGetterIndexerService))]
[assembly: GenerateImposter(typeof(Sample.AbstractSetterIndexerService))]
[assembly: GenerateImposter(typeof(Sample.IIndexerContract))]

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

    public class ReadonlyIndexerService
    {
        public int this[int key]
        {
            get => default;
            set { }
        }
    }

    public class GetterOnlyIndexerService
    {
        public virtual int this[int key]
        {
            get => key;
        }
    }

    public class SetterOnlyIndexerService
    {
        public virtual int this[int key]
        {
            set { }
        }
    }

    public abstract class AbstractGetterIndexerService
    {
        public abstract int this[int key] { get; }
    }

    public abstract class AbstractSetterIndexerService
    {
        public abstract int this[int key] { set; }
    }

    public interface IIndexerContract
    {
        int this[int key] { get; set; }
    }
}
""";

    [Fact]
    public async Task GivenIndexerGetterSetup_WhenReturningValue_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
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
"""
        );

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenOverrideableIndexer_WhenCallingUseBaseImplementation_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IndexerServiceImposter();
            imposter[Imposter.Abstractions.Arg<int>.Any()].Getter().UseBaseImplementation();
            imposter[Imposter.Abstractions.Arg<int>.Any()].Setter().UseBaseImplementation();
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenIndexerGetterSetup_WhenReturningDelegate_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
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
"""
        );

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenIndexerGetterReturns_WhenChainingReturns_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
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
"""
        );

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenIndexerGetterReturns_WhenChainingDelegate_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
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
"""
        );

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenIndexerGetterThrows_WhenSwitchingToReturns_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
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
"""
        );

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenIndexerGetterReturns_WhenMixingCallbacks_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
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
"""
        );

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenIndexerSetter_WhenCallingCalled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
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
"""
        );

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenIndexerSetter_WhenRegisteringCallback_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
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
"""
        );

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenIndexerSetter_WhenChainingCallbacks_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
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
"""
        );

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenIndexerSetter_WhenUsingThen_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
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
"""
        );

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenIndexerSetter_WhenMixingCallbacksAndThen_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
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
"""
        );

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenIndexerSetter_WhenUsingPredicateMatcher_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
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
"""
        );

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenGetterOnlyOverrideableIndexer_WhenCallingUseBaseImplementationOnGetter_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new GetterOnlyIndexerServiceImposter();
            imposter[Imposter.Abstractions.Arg<int>.Any()].Getter().UseBaseImplementation();
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenSetterOnlyOverrideableIndexer_WhenCallingUseBaseImplementationOnSetter_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SetterOnlyIndexerServiceImposter();
            imposter[Imposter.Abstractions.Arg<int>.Any()].Setter().UseBaseImplementation();
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenGetterOnlyIndexer_WhenCallingSetter_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new GetterOnlyIndexerServiceImposter();
            imposter[Imposter.Abstractions.Arg<int>.Any()].Setter();
        }
    }
}
"""
        );

        AssertSingleDiagnostic(
            diagnostics,
            WellKnownCsCompilerErrorCodes.MemberNotFound,
            expectedLine: 8
        );
    }

    [Fact]
    public async Task GivenSetterOnlyIndexer_WhenCallingGetter_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SetterOnlyIndexerServiceImposter();
            imposter[Imposter.Abstractions.Arg<int>.Any()].Getter();
        }
    }
}
"""
        );

        AssertSingleDiagnostic(
            diagnostics,
            WellKnownCsCompilerErrorCodes.MemberNotFound,
            expectedLine: 8
        );
    }

    [Fact]
    public async Task GivenNonOverrideableIndexerGetter_WhenCallingUseBaseImplementation_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IIndexerContractImposter();
            imposter[Imposter.Abstractions.Arg<int>.Any()].Getter().UseBaseImplementation();
        }
    }
}
"""
        );

        AssertSingleDiagnostic(
            diagnostics,
            WellKnownCsCompilerErrorCodes.MemberNotFound,
            expectedLine: 8
        );
    }

    [Fact]
    public async Task GivenAbstractGetterIndexer_WhenCallingUseBaseImplementation_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new AbstractGetterIndexerServiceImposter();
            imposter[Imposter.Abstractions.Arg<int>.Any()].Getter().UseBaseImplementation();
        }
    }
}
"""
        );

        AssertSingleDiagnostic(
            diagnostics,
            WellKnownCsCompilerErrorCodes.MemberNotFound,
            expectedLine: 8
        );
    }

    [Fact]
    public async Task GivenAbstractSetterIndexer_WhenCallingUseBaseImplementation_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new AbstractSetterIndexerServiceImposter();
            imposter[Imposter.Abstractions.Arg<int>.Any()].Setter().UseBaseImplementation();
        }
    }
}
"""
        );

        AssertSingleDiagnostic(
            diagnostics,
            WellKnownCsCompilerErrorCodes.MemberNotFound,
            expectedLine: 8
        );
    }

    [Fact]
    public async Task GivenNonOverrideableIndexerSetter_WhenCallingUseBaseImplementation_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IIndexerContractImposter();
            imposter[Imposter.Abstractions.Arg<int>.Any()].Setter().UseBaseImplementation();
        }
    }
}
"""
        );

        AssertSingleDiagnostic(
            diagnostics,
            WellKnownCsCompilerErrorCodes.MemberNotFound,
            expectedLine: 8
        );
    }

    [Fact]
    public async Task GivenIndexerGetterSetup_WhenCallingThenWithoutOutcome_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
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
"""
        );

        AssertSingleDiagnostic(
            diagnostics,
            WellKnownCsCompilerErrorCodes.MemberNotFound,
            expectedLine: 8
        );
    }

    [Fact]
    public async Task GivenIndexerGetterSetup_WhenRepeatingReturnsWithoutThen_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
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
"""
        );

        AssertSingleDiagnostic(
            diagnostics,
            WellKnownCsCompilerErrorCodes.MemberNotFound,
            expectedLine: 8
        );
    }

    [Fact]
    public async Task GivenIndexerGetterThrows_WhenRepeatingThrowsWithoutThen_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
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
"""
        );

        AssertSingleDiagnostic(
            diagnostics,
            WellKnownCsCompilerErrorCodes.MemberNotFound,
            expectedLine: 8
        );
    }

    [Fact]
    public async Task GivenIndexerGetterReturns_WhenThrowingGenericWithoutThen_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
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
"""
        );

        AssertSingleDiagnostic(
            diagnostics,
            WellKnownCsCompilerErrorCodes.MemberNotFound,
            expectedLine: 8
        );
    }

    [Fact]
    public async Task GivenIndexerGetterThrowsGeneric_WhenReturningWithoutThen_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
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
"""
        );

        AssertSingleDiagnostic(
            diagnostics,
            WellKnownCsCompilerErrorCodes.MemberNotFound,
            expectedLine: 8
        );
    }

    [Fact]
    public async Task GivenIndexerGetterCallback_WhenReturningWithoutThen_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
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
"""
        );

        AssertSingleDiagnostic(
            diagnostics,
            WellKnownCsCompilerErrorCodes.MemberNotFound,
            expectedLine: 8
        );
    }

    [Fact]
    public async Task GivenIndexerGetterCallback_WhenCallingCalled_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
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
"""
        );

        AssertSingleDiagnostic(
            diagnostics,
            WellKnownCsCompilerErrorCodes.MemberNotFound,
            expectedLine: 8
        );
    }

    [Fact]
    public async Task GivenIndexerSetter_WhenReturningValue_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
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
"""
        );

        AssertSingleDiagnostic(
            diagnostics,
            WellKnownCsCompilerErrorCodes.MemberNotFound,
            expectedLine: 8
        );
    }

    [Fact]
    public async Task GivenIndexerSetter_WhenReturningDelegate_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
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
"""
        );

        AssertSingleDiagnostic(
            diagnostics,
            WellKnownCsCompilerErrorCodes.MemberNotFound,
            expectedLine: 8
        );
    }

    [Fact]
    public async Task GivenIndexerSetter_WhenThrowingInstance_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
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
"""
        );

        AssertSingleDiagnostic(
            diagnostics,
            WellKnownCsCompilerErrorCodes.MemberNotFound,
            expectedLine: 8
        );
    }

    [Fact]
    public async Task GivenIndexerSetter_WhenThrowingGeneric_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
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
"""
        );

        AssertSingleDiagnostic(
            diagnostics,
            WellKnownCsCompilerErrorCodes.MemberNotFound,
            expectedLine: 8
        );
    }

    [Fact]
    public async Task GivenIndexerSetterCallback_WhenThrowingWithoutThen_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
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
"""
        );

        AssertSingleDiagnostic(
            diagnostics,
            WellKnownCsCompilerErrorCodes.MemberNotFound,
            expectedLine: 8
        );
    }

    [Fact]
    public async Task GivenIndexerGetterCallback_WhenThrowingWithoutThen_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
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
"""
        );

        AssertSingleDiagnostic(
            diagnostics,
            WellKnownCsCompilerErrorCodes.MemberNotFound,
            expectedLine: 8
        );
    }

    [Fact]
    public async Task GivenIndexerGetterOutcome_WhenCallingCalled_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
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
"""
        );

        AssertSingleDiagnostic(
            diagnostics,
            WellKnownCsCompilerErrorCodes.MemberNotFound,
            expectedLine: 8
        );
    }

    [Fact]
    public async Task GivenIndexerGetterFluent_WhenCallingCalled_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
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
"""
        );

        AssertSingleDiagnostic(
            diagnostics,
            WellKnownCsCompilerErrorCodes.MemberNotFound,
            expectedLine: 8
        );
    }

    [Fact]
    public async Task GivenIndexerSetter_WhenCallingThenWithoutCallback_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
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
"""
        );

        AssertSingleDiagnostic(
            diagnostics,
            WellKnownCsCompilerErrorCodes.MemberNotFound,
            expectedLine: 8
        );
    }

    [Fact]
    public async Task GivenIndexerSetterCallback_WhenCallingCalled_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
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
"""
        );

        AssertSingleDiagnostic(
            diagnostics,
            WellKnownCsCompilerErrorCodes.MemberNotFound,
            expectedLine: 8
        );
    }

    [Fact]
    public async Task GivenIndexerSetterCallback_WhenCallingThenCalled_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
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
"""
        );

        AssertSingleDiagnostic(
            diagnostics,
            WellKnownCsCompilerErrorCodes.MemberNotFound,
            expectedLine: 8
        );
    }

    private const string BaseSourceFileName = "IndexerFluentApiTests.Base.cs";
    private const string SnippetFileName = "IndexerFluentApiTests.Snippet.cs";

    private static readonly Task<GeneratorTestContext> TestContextTask =
        GeneratorTestHelper.CreateContext(
            Source,
            baseSourceFileName: BaseSourceFileName,
            snippetFileName: SnippetFileName,
            assemblyName: nameof(IndexerFluentApiTests),
            languageVersion: LanguageVersion.CSharp9
        );

    private static async Task<ImmutableArray<Diagnostic>> CompileSnippet(string snippet)
    {
        var context = await TestContextTask.ConfigureAwait(false);
        return context.CompileSnippet(snippet);
    }

    private static void AssertSingleDiagnostic(
        ImmutableArray<Diagnostic> diagnostics,
        string expectedId,
        int expectedLine
    ) =>
        GeneratorTestHelper.AssertSingleDiagnostic(
            diagnostics,
            expectedId,
            expectedLine,
            SnippetFileName
        );

    private static void AssertNoDiagnostics(ImmutableArray<Diagnostic> diagnostics) =>
        GeneratorTestHelper.AssertNoDiagnostics(diagnostics);
}
