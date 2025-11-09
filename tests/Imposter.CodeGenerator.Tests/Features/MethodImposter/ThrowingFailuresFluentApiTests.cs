using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.MethodImposter;

public class ThrowingFailuresFluentApiTests : MethodImposterFluentApiTestsBase
{
    [Fact]
    public void GivenMethodThrowsGeneric_WhenReturning_ShouldFail()
    {
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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
    public void GivenMethodThrowsGeneric_WhenUsingNonExceptionType_ShouldFail()
    {
        var diagnostics = CompileSnippet( /*lang=csharp*/"""
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