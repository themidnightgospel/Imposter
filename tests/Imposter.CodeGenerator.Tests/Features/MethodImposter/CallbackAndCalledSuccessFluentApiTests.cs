using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.MethodImposter;

public class CallbackAndCalledSuccessFluentApiTests : MethodImposterFluentApiTestsBase
{
    [Fact]
    public async Task Given_MethodCallback_WhenChainingCallback_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
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
    public async Task Given_MethodCallback_WhenCallingThen_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
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
    public async Task Given_MethodReturns_WhenCallingCallback_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
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
    public async Task Given_MethodReturns_WhenCallingThen_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
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
    public async Task Given_MethodReturnsAsync_WhenCallingCallback_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
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
    public async Task Given_MethodReturnsAsync_WhenCallingThen_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
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
    public async Task Given_MethodThrowsGeneric_WhenCallingCallback_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
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
    public async Task Given_MethodThrowsGeneric_WhenCallingThen_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
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
    public async Task Given_MethodThrowsInstance_WhenCallingCallback_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
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
    public async Task Given_MethodThrowsInstance_WhenCallingThen_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
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
    public async Task Given_MethodThrowsGenerator_WhenCallingCallback_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
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
    public async Task Given_MethodThrowsGenerator_WhenCallingThen_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
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
    public async Task Given_MethodThrowsAsync_WhenCallingCallback_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
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
    public async Task Given_MethodThrowsAsync_WhenCallingThen_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
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
    public async Task Given_UseBaseImplementation_WhenCallingCallback_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
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
    public async Task Given_UseBaseImplementation_WhenCallingThen_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
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

