using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.MethodImposter;

public class ReturnsAndAsyncSuccessFluentApiTests : MethodImposterFluentApiTestsBase
{
    [Fact]
    public void GivenMethodWithReturn_WhenReturningValues_ShouldCompile()
    {
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
    public void GivenMethod_WhenThrowing_ShouldCompile()
    {
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
}