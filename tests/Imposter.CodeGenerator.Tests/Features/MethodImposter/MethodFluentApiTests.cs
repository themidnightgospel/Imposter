using System.Collections.Immutable;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.MethodImposter;

public class MethodFluentApiTests
{
    private const string Source = /*lang=csharp*/"""
using System.Threading.Tasks;
using Imposter.Abstractions;

[assembly: GenerateImposter(typeof(Sample.IMethodInterface))]
[assembly: GenerateImposter(typeof(Sample.ClassMethodTarget))]
[assembly: GenerateImposter(typeof(Sample.SealedOverrideClass))]

namespace Sample
{
    public interface IMethodInterface
    {
        int GetNumber();
        void DoWork();
        Task<int> GetNumberAsync();
        Task DoWorkAsync();
        int Increment(int value);
        int RefOutParams(ref int seed, out int doubled, params string[] labels);
    }

    public class ClassMethodTarget
    {
        public virtual int VirtualCompute(int value) => value * 2;
        public virtual int VirtualAdd(int left, int right) => left + right;
        public virtual System.Threading.Tasks.Task<int> VirtualComputeAsync(int value) => System.Threading.Tasks.Task.FromResult(value * 2);
        public int NonVirtualCompute(int value) => value;
        public virtual void VirtualAction() { }
        public void NonVirtualAction() { }
        public virtual int VirtualRefOutParams(ref int seed, out int doubled, params string[] labels)
        {
            doubled = seed * 2;
            return doubled + labels.Length;
        }
    }

    public class SealedOverrideClass : ClassMethodTarget
    {
        public sealed override int VirtualCompute(int value) => base.VirtualCompute(value);
        public sealed override System.Threading.Tasks.Task<int> VirtualComputeAsync(int value) => base.VirtualComputeAsync(value);
        public sealed override void VirtualAction() => base.VirtualAction();
    }
}
""";

    [Fact]
    public void GivenMethodWithReturn_WhenReturningValues_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumber().Returns(1);
            imposter.GetNumber().Returns(() => 2);
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenAsyncMethod_WhenReturningAsyncValue_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumberAsync().ReturnsAsync(42);
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenAsyncMethod_WhenReturningTaskFromDelegate_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumberAsync().Returns(() => System.Threading.Tasks.Task.FromResult(1));
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenAsyncMethod_WhenChainingReturnsAsyncThenDelegate_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumberAsync()
                .ReturnsAsync(1)
                .Then()
                .Returns(() => System.Threading.Tasks.Task.FromResult(2));
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenMethod_WhenChainingReturnsThenDelegate_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumber()
                .Returns(1)
                .Then()
                .Returns(() => 2);
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenTaskMethod_WhenReturningCompletedTask_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.DoWorkAsync().Returns(System.Threading.Tasks.Task.CompletedTask);
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenTaskMethod_WhenCallingReturnsAsync_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.DoWorkAsync().ReturnsAsync(System.Threading.Tasks.Task.CompletedTask);
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenMethod_WhenThrowing_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumber().Throws<System.InvalidOperationException>();
            imposter.GetNumber().Throws(new System.Exception("fail"));
            imposter.GetNumber().Throws(() => new System.Exception("fail"));
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenAsyncMethod_WhenThrowingAsync_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumberAsync().ThrowsAsync(new System.Exception("fail"));
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenClassVirtualMethod_WhenUsingBaseImplementation_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new ClassMethodTargetImposter();
            imposter.VirtualCompute(Imposter.Abstractions.Arg<int>.Any()).UseBaseImplementation();
            imposter.VirtualAction().UseBaseImplementation();
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenClassMethod_WhenCallbackUsesParameters_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new ClassMethodTargetImposter();
            imposter.VirtualCompute(Imposter.Abstractions.Arg<int>.Any()).Callback((int value) => { });
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenClassMethod_WhenCallbackAfterReturnChain_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new ClassMethodTargetImposter();
            imposter.VirtualCompute(Imposter.Abstractions.Arg<int>.Is(x => x > 0))
                .Returns(123)
                .Then()
                .Callback((int _) => { });
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenClassVoidMethod_WhenCallbackThenUsesBaseImplementation_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new ClassMethodTargetImposter();
            imposter.VirtualAction()
                .Callback(() => { })
                .Then()
                .UseBaseImplementation();
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenClassMethod_WhenReturningWithDelegate_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new ClassMethodTargetImposter();
            imposter.VirtualCompute(Imposter.Abstractions.Arg<int>.Any())
                .Returns(value => value + 1);
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenClassAsyncMethod_WhenReturningAsyncValue_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new ClassMethodTargetImposter();
            imposter.VirtualComputeAsync(Imposter.Abstractions.Arg<int>.Any())
                .ReturnsAsync(42);
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenMethod_WhenCallbackThen_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.DoWork().Callback(() => { }).Then();
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenMethod_WhenCallingCalledOnGetNumber_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumber().Called(Imposter.Abstractions.Count.Exactly(1));
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenMethod_WhenCallingCalledOnDoWork_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.DoWork().Called(Imposter.Abstractions.Count.Exactly(1));
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenAsyncMethod_WhenCallingCalled_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumberAsync().Called(Imposter.Abstractions.Count.Exactly(1));
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenInterfaceMethodWithParameters_WhenReturningWithDelegate_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.Increment(1)
                .Returns(value => value + 2);
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenClassMethod_WhenCallingCalled_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new ClassMethodTargetImposter();
            imposter.VirtualCompute(Imposter.Abstractions.Arg<int>.Any()).Called(Imposter.Abstractions.Count.Exactly(1));
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenClassMethodWithMultipleParameters_WhenReturning_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new ClassMethodTargetImposter();
            imposter.VirtualAdd(
                Imposter.Abstractions.Arg<int>.Is(x => x > 0),
                Imposter.Abstractions.Arg<int>.Is(y => y < 10))
                .Returns(42);
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenClassMethodWithMultipleParameters_WhenCallback_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new ClassMethodTargetImposter();
            imposter.VirtualAdd(Imposter.Abstractions.Arg<int>.Any(), Imposter.Abstractions.Arg<int>.Any())
                .Callback((int left, int right) => { });
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenInterfaceMethodWithRefOutParams_WhenReturning_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.RefOutParams(
                Imposter.Abstractions.Arg<int>.Is(value => value > 0),
                Imposter.Abstractions.OutArg<int>.Any(),
                Imposter.Abstractions.Arg<string[]>.Any())
                .Returns(5);
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenClassMethodWithRefOutParams_WhenCallback_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new ClassMethodTargetImposter();
            imposter.VirtualRefOutParams(
                Imposter.Abstractions.Arg<int>.Any(),
                Imposter.Abstractions.OutArg<int>.Any(),
                Imposter.Abstractions.Arg<string[]>.Any())
                .Callback((ref int seed, out int doubled, string[] _) => doubled = seed * 2);
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenMethodCallback_WhenChainingCallback_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumber()
                .Callback(default(IMethodInterfaceImposter.GetNumberCallbackDelegate))
                .Callback(default(IMethodInterfaceImposter.GetNumberCallbackDelegate));
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenMethodCallback_WhenCallingThen_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumber()
                .Callback(default(IMethodInterfaceImposter.GetNumberCallbackDelegate))
                .Then();
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenMethod_WhenCallingThen_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
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
    public void GivenMethodReturns_WhenCallingCalled_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
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
    public void GivenMethodReturns_WhenCallingThenCalled_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
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
    public void GivenMethodCallback_WhenCallingCalled_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
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
    public void GivenMethodCallback_WhenThenCallingCalled_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
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
    public void GivenMethodThrows_WhenCallingCalled_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
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
    public void GivenMethodThrowsAsync_WhenCallingCalled_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
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
    public void GivenMethodUseBaseImplementation_WhenCallingCalled_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
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
    public void GivenVoidMethod_WhenReturning_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.DoWork().Returns(1);
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenClassVoidMethod_WhenReturning_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new ClassMethodTargetImposter();
            imposter.VirtualAction().Returns(1);
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenNonAsyncMethod_WhenCallingReturnsAsync_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumber().ReturnsAsync(1);
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenNonAsyncMethod_WhenCallingThrowsAsync_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumber().ThrowsAsync(new System.Exception("fail"));
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenInterfaceMethod_WhenUsingBaseImplementation_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumber().UseBaseImplementation();
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenNonVirtualClassMethod_WhenUsingBaseImplementation_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new ClassMethodTargetImposter();
            imposter.NonVirtualCompute(Imposter.Abstractions.Arg<int>.Any()).UseBaseImplementation();
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenNonVirtualMethod_WhenAccessingFluentApi_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new ClassMethodTargetImposter();
            imposter.NonVirtualCompute(Imposter.Abstractions.Arg<int>.Any()).Returns(1);
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenSealedOverrideMethod_WhenAccessingFluentApi_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new SealedOverrideClassImposter();
            imposter.VirtualCompute(Imposter.Abstractions.Arg<int>.Any()).Returns(1);
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenMethodReturns_WhenReturningAgain_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumber().Returns(1).Returns(2);
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenMethodReturns_WhenReturningAsync_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumberAsync().Returns(System.Threading.Tasks.Task.FromResult(1)).ReturnsAsync(2);
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenMethodReturns_WhenThrowing_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumber().Returns(1).Throws<System.InvalidOperationException>();
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenMethodReturns_WhenThrowingAsync_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumberAsync().Returns(System.Threading.Tasks.Task.FromResult(1)).ThrowsAsync(new System.Exception("fail"));
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenMethodReturns_WhenUsingBaseImplementation_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new ClassMethodTargetImposter();
            imposter.VirtualCompute(Imposter.Abstractions.Arg<int>.Any()).Returns(1).UseBaseImplementation();
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenMethodReturnsAsync_WhenReturning_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumberAsync().ReturnsAsync(1).Returns(System.Threading.Tasks.Task.FromResult(2));
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenMethodReturnsAsync_WhenReturningAsync_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumberAsync().ReturnsAsync(1).ReturnsAsync(2);
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenMethodReturnsAsync_WhenThrowing_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumberAsync().ReturnsAsync(1).Throws<System.InvalidOperationException>();
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenMethodReturnsAsync_WhenThrowingAsync_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumberAsync().ReturnsAsync(1).ThrowsAsync(new System.Exception("fail"));
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenMethodReturnsAsync_WhenUsingBaseImplementation_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new ClassMethodTargetImposter();
            imposter.VirtualComputeAsync(Imposter.Abstractions.Arg<int>.Any()).ReturnsAsync(1).UseBaseImplementation();
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenMethodThrowsGeneric_WhenReturning_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumber().Throws<System.InvalidOperationException>().Returns(1);
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenMethodThrowsGeneric_WhenReturningAsync_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumberAsync().Throws<System.InvalidOperationException>().ReturnsAsync(1);
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenMethodThrowsGeneric_WhenThrowingAgain_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumber().Throws<System.InvalidOperationException>().Throws<System.Exception>();
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenMethodThrowsGeneric_WhenThrowingAsync_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumberAsync().Throws<System.InvalidOperationException>().ThrowsAsync(new System.Exception("fail"));
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenMethodThrowsGeneric_WhenUsingBaseImplementation_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new ClassMethodTargetImposter();
            imposter.VirtualCompute(Imposter.Abstractions.Arg<int>.Any()).Throws<System.InvalidOperationException>().UseBaseImplementation();
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenMethodThrowsInstance_WhenReturning_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumber().Throws(new System.Exception("fail")).Returns(1);
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenMethodThrowsInstance_WhenReturningAsync_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumberAsync().Throws(new System.Exception("fail")).ReturnsAsync(1);
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenMethodThrowsInstance_WhenThrowingAgain_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumber().Throws(new System.Exception("fail")).Throws<System.Exception>();
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenMethodThrowsInstance_WhenThrowingAsync_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumberAsync().Throws(new System.Exception("fail")).ThrowsAsync(new System.Exception("fail again"));
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenMethodThrowsInstance_WhenUsingBaseImplementation_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new ClassMethodTargetImposter();
            imposter.VirtualCompute(Imposter.Abstractions.Arg<int>.Any()).Throws(new System.Exception("fail")).UseBaseImplementation();
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenMethodThrowsGenerator_WhenReturning_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumber().Throws(() => new System.Exception("fail")).Returns(1);
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenMethodThrowsGenerator_WhenReturningAsync_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumberAsync().Throws(() => new System.Exception("fail")).ReturnsAsync(1);
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenMethodThrowsGenerator_WhenThrowingAgain_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumber().Throws(() => new System.Exception("fail")).Throws<System.Exception>();
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenMethodThrowsGenerator_WhenThrowingAsync_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumberAsync().Throws(() => new System.Exception("fail")).ThrowsAsync(new System.Exception("fail again"));
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenMethodThrowsGenerator_WhenUsingBaseImplementation_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new ClassMethodTargetImposter();
            imposter.VirtualCompute(Imposter.Abstractions.Arg<int>.Any()).Throws(value => new System.Exception("fail")).UseBaseImplementation();
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenMethodThrowsAsync_WhenReturning_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumberAsync().ThrowsAsync(new System.Exception("fail")).Returns(System.Threading.Tasks.Task.FromResult(1));
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenMethodThrowsAsync_WhenReturningAsync_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumberAsync().ThrowsAsync(new System.Exception("fail")).ReturnsAsync(1);
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenMethodThrowsAsync_WhenThrowingAgain_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumberAsync().ThrowsAsync(new System.Exception("fail")).Throws<System.Exception>();
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenMethodThrowsAsync_WhenThrowingAsyncAgain_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumberAsync().ThrowsAsync(new System.Exception("fail")).ThrowsAsync(new System.Exception("fail again"));
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenMethodThrowsAsync_WhenUsingBaseImplementation_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new ClassMethodTargetImposter();
            imposter.VirtualComputeAsync(Imposter.Abstractions.Arg<int>.Any()).ThrowsAsync(new System.Exception("fail")).UseBaseImplementation();
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenUseBaseImplementation_WhenReturning_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new ClassMethodTargetImposter();
            imposter.VirtualCompute(Imposter.Abstractions.Arg<int>.Any()).UseBaseImplementation().Returns(1);
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenUseBaseImplementation_WhenReturningAsync_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new ClassMethodTargetImposter();
            imposter.VirtualComputeAsync(Imposter.Abstractions.Arg<int>.Any()).UseBaseImplementation().ReturnsAsync(1);
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenUseBaseImplementation_WhenThrowing_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new ClassMethodTargetImposter();
            imposter.VirtualCompute(Imposter.Abstractions.Arg<int>.Any()).UseBaseImplementation().Throws<System.InvalidOperationException>();
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenUseBaseImplementation_WhenThrowingAsync_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new ClassMethodTargetImposter();
            imposter.VirtualComputeAsync(Imposter.Abstractions.Arg<int>.Any()).UseBaseImplementation().ThrowsAsync(new System.Exception("fail"));
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenUseBaseImplementation_WhenChainingUseBaseImplementation_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new ClassMethodTargetImposter();
            imposter.VirtualCompute(Imposter.Abstractions.Arg<int>.Any()).UseBaseImplementation().UseBaseImplementation();
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound, expectedLine: 8);
    }

    [Fact]
    public void GivenMethodCallback_WhenReturning_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
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
    public void GivenMethodCallback_WhenReturningAsync_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
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
    public void GivenMethodCallback_WhenThrowing_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
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
    public void GivenMethodCallback_WhenThrowingAsync_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
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
    public void GivenMethodCallback_WhenUsingBaseImplementation_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
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

    [Fact]
    public void GivenMethodReturns_WhenCallingCallback_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumber()
                .Returns(1)
                .Callback(default(IMethodInterfaceImposter.GetNumberCallbackDelegate));
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenMethodReturns_WhenCallingThen_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumber()
                .Returns(1)
                .Then();
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenMethodReturnsAsync_WhenCallingCallback_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumberAsync()
                .ReturnsAsync(1)
                .Callback(default(IMethodInterfaceImposter.GetNumberAsyncCallbackDelegate));
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenMethodReturnsAsync_WhenCallingThen_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumberAsync()
                .ReturnsAsync(1)
                .Then();
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenMethodThrowsGeneric_WhenCallingCallback_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumber()
                .Throws<System.InvalidOperationException>()
                .Callback(default(IMethodInterfaceImposter.GetNumberCallbackDelegate));
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenMethodThrowsGeneric_WhenCallingThen_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumber()
                .Throws<System.InvalidOperationException>()
                .Then();
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenMethodThrowsInstance_WhenCallingCallback_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumber()
                .Throws(new System.Exception("fail"))
                .Callback(default(IMethodInterfaceImposter.GetNumberCallbackDelegate));
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenMethodThrowsInstance_WhenCallingThen_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumber()
                .Throws(new System.Exception("fail"))
                .Then();
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenMethodThrowsGenerator_WhenCallingCallback_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumber()
                .Throws(() => new System.Exception("fail"))
                .Callback(default(IMethodInterfaceImposter.GetNumberCallbackDelegate));
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenMethodThrowsGenerator_WhenCallingThen_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumber()
                .Throws(() => new System.Exception("fail"))
                .Then();
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenMethodThrowsGeneric_WhenUsingNonExceptionType_ShouldFail()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumber().Throws<int>();
        }
    }
}
""");

        AssertSingleDiagnostic(diagnostics, "CS0315", expectedLine: 8);
    }

    [Fact]
    public void GivenMethodThrowsAsync_WhenCallingCallback_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumberAsync()
                .ThrowsAsync(new System.Exception("fail"))
                .Callback(default(IMethodInterfaceImposter.GetNumberAsyncCallbackDelegate));
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenMethodThrowsAsync_WhenCallingThen_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodInterfaceImposter();
            imposter.GetNumberAsync()
                .ThrowsAsync(new System.Exception("fail"))
                .Then();
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenUseBaseImplementation_WhenCallingCallback_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new ClassMethodTargetImposter();
            imposter.VirtualCompute(Imposter.Abstractions.Arg<int>.Any())
                .UseBaseImplementation()
                .Callback(default(ClassMethodTargetImposter.VirtualComputeCallbackDelegate));
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public void GivenUseBaseImplementation_WhenCallingThen_ShouldCompile()
    {
        var diagnostics = CompileSnippet(/*lang=csharp*/"""
namespace Sample
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new ClassMethodTargetImposter();
            imposter.VirtualCompute(Imposter.Abstractions.Arg<int>.Any())
                .UseBaseImplementation()
                .Then();
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
            assemblyName: nameof(MethodFluentApiTests));

    private static ImmutableArray<Diagnostic> CompileSnippet(string snippet) => TestContext.CompileSnippet(snippet);

    private static void AssertNoDiagnostics(ImmutableArray<Diagnostic> diagnostics) =>
        GeneratorTestHelper.AssertNoDiagnostics(diagnostics);

    private static void AssertSingleDiagnostic(ImmutableArray<Diagnostic> diagnostics, string expectedId, int expectedLine) =>
        GeneratorTestHelper.AssertSingleDiagnostic(diagnostics, expectedId, expectedLine, SnippetFileName);
}
