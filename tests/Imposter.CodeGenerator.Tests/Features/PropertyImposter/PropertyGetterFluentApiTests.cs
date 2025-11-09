using System.Collections.Immutable;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.PropertyImposter;

public class PropertyGetterFluentApiTests
{
    private const string Source = /*lang=csharp*/"""
using Imposter.Abstractions;

[assembly: GenerateImposter(typeof(Sample.SampleService))]

namespace Sample
{
    public class SampleService
    {
        public virtual int Age { get; }
    }
}
""";

    [Fact]
    public void GivenGetterSetup_WhenRepeatingReturnsWithoutThen_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SampleServiceImposter();
            imposter.Age.Getter().Returns(1).Returns(2);
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenGetterSetup_WhenReturningDelegateThenValueWithoutThen_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SampleServiceImposter();
            imposter.Age.Getter().Returns(() => 1).Returns(2);
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenGetterSetup_WhenRepeatingThrowsWithoutThen_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SampleServiceImposter();
            imposter.Age.Getter().Throws(new System.Exception("fail")).Throws(new System.Exception("fail again"));
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenGetterSetup_WhenReturnsThenThrowsGenericWithoutThen_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SampleServiceImposter();
            imposter.Age.Getter().Returns(1).Throws<System.InvalidOperationException>();
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenGetterSetup_WhenReturnsThenThrowsWithoutThen_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SampleServiceImposter();
            imposter.Age.Getter().Returns(1).Throws(new System.Exception("fail"));
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenGetterSetup_WhenThrowsGenericThenReturnsWithoutThen_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SampleServiceImposter();
            imposter.Age.Getter().Throws<System.InvalidOperationException>().Returns(1);
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenGetterSetup_WhenThrowsThenReturnsWithoutThen_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SampleServiceImposter();
            imposter.Age.Getter().Throws(new System.Exception("fail")).Returns(1);
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenGetterContinuation_WhenCallingCalled_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SampleServiceImposter();
            imposter.Age.Getter().Returns(1).Called(Imposter.Abstractions.Count.AtLeast(1));
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenGetterThen_WhenCallingCalled_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SampleServiceImposter();
            imposter.Age.Getter().Returns(1).Then().Called(Imposter.Abstractions.Count.AtLeast(1));
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenGetterCallback_WhenCallingCalled_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SampleServiceImposter();
            imposter.Age.Getter().Callback(() => { }).Called(Imposter.Abstractions.Count.AtLeast(1));
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenGetterCallback_WhenReturningWithoutThen_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SampleServiceImposter();
            imposter.Age.Getter().Callback(() => { }).Returns(1);
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenGetterCallback_WhenThrowingWithoutThen_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SampleServiceImposter();
            imposter.Age.Getter().Callback(() => { }).Throws(new System.Exception("fail"));
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenGetterThrows_WhenCallingCalled_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SampleServiceImposter();
            imposter.Age.Getter().Throws(new System.Exception("fail")).Called(Imposter.Abstractions.Count.AtLeast(1));
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenGetterThrowsGeneric_WhenCallingCalled_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SampleServiceImposter();
            imposter.Age.Getter().Throws<System.InvalidOperationException>().Called(Imposter.Abstractions.Count.AtLeast(1));
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenTopLevelBuilder_WhenCallingCalled_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SampleServiceImposter();
            imposter.Age.Getter().Called(Imposter.Abstractions.Count.AtLeast(1));
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenGetterThen_WhenReturning_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SampleServiceImposter();
            imposter.Age.Getter().Then().Returns(1);
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenGetterThen_WhenCallingCallback_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SampleServiceImposter();
            imposter.Age.Getter().Then().Callback(() => { });
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenGetterThen_WhenCallingCalledFromStart_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SampleServiceImposter();
            imposter.Age.Getter().Then().Called(Imposter.Abstractions.Count.AtLeast(1));
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenGetterReturns_WhenCallingCallback_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SampleServiceImposter();
            imposter.Age.Getter().Returns(1).Callback(() => { });
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenGetterReturns_WhenCallingThen_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SampleServiceImposter();
            imposter.Age.Getter().Returns(1).Then();
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenGetterCallback_WhenChainingCallback_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SampleServiceImposter();
            imposter.Age.Getter().Callback(() => { }).Callback(() => { });
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenGetterCallback_WhenCallingThen_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SampleServiceImposter();
            imposter.Age.Getter().Callback(() => { }).Then();
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenGetterThrows_WhenCallingCallback_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SampleServiceImposter();
            imposter.Age.Getter().Throws(new System.Exception("fail")).Callback(() => { });
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenGetterThrowsGeneric_WhenCallingCallback_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SampleServiceImposter();
            imposter.Age.Getter().Throws<System.InvalidOperationException>().Callback(() => { });
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenContinuation_WhenCallingCallback_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SampleServiceImposter();
            imposter.Age.Getter().Returns(1).Then().Callback(() => { });
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenContinuation_WhenSwitchingToReturns_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SampleServiceImposter();
            imposter.Age.Getter().Returns(() => 1).Then().Returns(2);
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenChainedContinuation_WhenMixingMethods_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SampleServiceImposter();
            imposter.Age.Getter().Returns(1).Then().Callback(() => { }).Then().Returns(() => 2);
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenContinuation_WhenThrowingThenReturning_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SampleServiceImposter();
            imposter.Age.Getter().Throws(new System.Exception("fail")).Then().Returns(1);
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    private const string BaseSourceFileName = "GeneratorInput.cs";
    private const string SnippetFileName = "Snippet.cs";

    private static readonly GeneratorTestContext TestContext =
        GeneratorTestHelper.CreateContext(
            Source,
            baseSourceFileName: BaseSourceFileName,
            snippetFileName: SnippetFileName,
            assemblyName: nameof(PropertyGetterFluentApiTests));

    private static ImmutableArray<Diagnostic> CompileSnippet(string snippet) => TestContext.CompileSnippet(snippet);

    private static void AssertNoDiagnostics(ImmutableArray<Diagnostic> diagnostics) =>
        GeneratorTestHelper.AssertNoDiagnostics(diagnostics);

    private static void AssertSingleDiagnostic(ImmutableArray<Diagnostic> diagnostics, string expectedId, int expectedLine) =>
        GeneratorTestHelper.AssertSingleDiagnostic(diagnostics, expectedId, expectedLine, SnippetFileName);
}
