using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.MethodImposter;

public class CallbackAndCalledSuccessFluentApiTests : MethodImposterFluentApiTestsBase
{
    [Fact]
    public void GivenMethodCallback_WhenChainingCallback_ShouldCompile()
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
    public void GivenMethodReturns_WhenCallingCallback_ShouldCompile()
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
    public void GivenMethodThrowsAsync_WhenCallingCallback_ShouldCompile()
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
}