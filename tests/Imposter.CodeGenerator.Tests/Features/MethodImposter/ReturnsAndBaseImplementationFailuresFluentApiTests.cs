using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.MethodImposter;

public class ReturnsAndBaseImplementationFailuresFluentApiTests : MethodImposterFluentApiTestsBase
{
    [Fact]
    public void GivenVoidMethod_WhenReturning_ShouldFail()
    {
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
    public void GivenTaskMethod_WhenCallingReturnsAsync_ShouldFail()
    {
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
    public void GivenUseBaseImplementation_WhenReturning_ShouldFail()
    {
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
}