using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.MethodImposter;

public class CallbackAndCalledFailuresFluentApiTests : MethodImposterFluentApiTestsBase
{
    [Fact]
    public async Task GivenMethod_WhenCallingThen_ShouldFail()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.DoWork().Then();
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public async Task GivenMethodReturns_WhenCallingCalled_ShouldFail()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumber().Returns(1).Called(Imposter.Abstractions.Count.Exactly(1));
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public async Task GivenMethodReturns_WhenCallingThenCalled_ShouldFail()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumber().Returns(1).Then().Called(Imposter.Abstractions.Count.Exactly(1));
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public async Task GivenMethodCallback_WhenCallingCalled_ShouldFail()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumber()
                .Callback(default(IMethodInterfaceImposter.GetNumberCallbackDelegate))
                .Called(Imposter.Abstractions.Count.Exactly(1));
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 10);
    }

    [Fact]
    public async Task GivenMethodCallback_WhenThenCallingCalled_ShouldFail()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumber()
                .Callback(default(IMethodInterfaceImposter.GetNumberCallbackDelegate))
                .Then()
                .Called(Imposter.Abstractions.Count.Exactly(1));
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 11);
    }

    [Fact]
    public async Task GivenMethodThrows_WhenCallingCalled_ShouldFail()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumber().Throws<System.InvalidOperationException>().Called(Imposter.Abstractions.Count.Exactly(1));
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public async Task GivenMethodThrowsAsync_WhenCallingCalled_ShouldFail()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumberAsync().ThrowsAsync(new System.Exception("fail")).Called(Imposter.Abstractions.Count.Exactly(1));
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public async Task GivenMethodUseBaseImplementation_WhenCallingCalled_ShouldFail()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumber().UseBaseImplementation().Called(Imposter.Abstractions.Count.Exactly(1));
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public async Task GivenMethodCallback_WhenReturning_ShouldFail()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumber()
                .Callback(default(IMethodInterfaceImposter.GetNumberCallbackDelegate))
                .Returns(1);
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 10);
    }

    [Fact]
    public async Task GivenMethodCallback_WhenReturningAsync_ShouldFail()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumberAsync()
                .Callback(default(IMethodInterfaceImposter.GetNumberAsyncCallbackDelegate))
                .ReturnsAsync(1);
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 10);
    }

    [Fact]
    public async Task GivenMethodCallback_WhenThrowing_ShouldFail()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumber()
                .Callback(default(IMethodInterfaceImposter.GetNumberCallbackDelegate))
                .Throws<System.InvalidOperationException>();
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 10);
    }

    [Fact]
    public async Task GivenMethodCallback_WhenThrowingAsync_ShouldFail()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumberAsync()
                .Callback(default(IMethodInterfaceImposter.GetNumberAsyncCallbackDelegate))
                .ThrowsAsync(new System.Exception("fail"));
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 10);
    }

    [Fact]
    public async Task GivenMethodCallback_WhenUsingBaseImplementation_ShouldFail()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new ClassMethodTargetImposter();
            imposter.VirtualCompute(Imposter.Abstractions.Arg<int>.Any())
                .Callback(default(ClassMethodTargetImposter.VirtualComputeCallbackDelegate))
                .UseBaseImplementation();
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 10);
    }
}

