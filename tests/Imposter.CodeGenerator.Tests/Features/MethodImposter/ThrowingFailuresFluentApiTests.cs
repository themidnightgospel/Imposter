using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.MethodImposter;

public class ThrowingFailuresFluentApiTests : MethodImposterFluentApiTestsBase
{
    [Fact]
    public async Task Given_MethodThrowsGeneric_WhenReturning_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
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
    public async Task Given_MethodThrowsGeneric_WhenReturningAsync_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
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
    public async Task Given_MethodThrowsGeneric_WhenThrowingAgain_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
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
    public async Task Given_MethodThrowsGeneric_WhenThrowingAsync_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
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
    public async Task Given_MethodThrowsGeneric_WhenUsingBaseImplementation_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
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
    public async Task Given_MethodThrowsInstance_WhenReturning_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
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
    public async Task Given_MethodThrowsInstance_WhenReturningAsync_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
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
    public async Task Given_MethodThrowsInstance_WhenThrowingAgain_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
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
    public async Task Given_MethodThrowsInstance_WhenThrowingAsync_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
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
    public async Task Given_MethodThrowsInstance_WhenUsingBaseImplementation_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
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
    public async Task Given_MethodThrowsGenerator_WhenReturning_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
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
    public async Task Given_MethodThrowsGenerator_WhenReturningAsync_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
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
    public async Task Given_MethodThrowsGenerator_WhenThrowingAgain_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
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
    public async Task Given_MethodThrowsGenerator_WhenThrowingAsync_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
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
    public async Task Given_MethodThrowsGenerator_WhenUsingBaseImplementation_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
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
    public async Task Given_MethodThrowsAsync_WhenReturning_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
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
    public async Task Given_MethodThrowsAsync_WhenReturningAsync_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
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
    public async Task Given_MethodThrowsAsync_WhenThrowingAgain_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
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
    public async Task Given_MethodThrowsAsync_WhenThrowingAsyncAgain_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
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
    public async Task Given_MethodThrowsAsync_WhenUsingBaseImplementation_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
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
    public async Task Given_MethodThrowsGeneric_WhenUsingNonExceptionType_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/"""
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
}

