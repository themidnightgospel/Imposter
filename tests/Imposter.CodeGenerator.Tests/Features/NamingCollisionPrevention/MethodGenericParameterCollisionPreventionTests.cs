using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention;

public class MethodGenericParameterCollisionPreventionTests : NamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task Given_GenericMethodWithTargetNamedTypeParameters_ShouldCompileWithoutDiagnostics()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
using Sample.NamingCollision;

namespace Sample.NamingCollisionUsage
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodGenericTypeParameterCollisionTargetImposter();
            imposter.GenericMethod<int, string, double, decimal>(1, "", 2d, 3m);
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task Given_GenericMethodWithTExceptionParameter_ShouldCompileWithoutDiagnostics()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
using Sample.NamingCollision;

namespace Sample.NamingCollisionUsage
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodGenericExceptionTypeParameterCollisionTargetImposter();
            imposter.ThrowsGeneric<System.InvalidOperationException>();
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task Given_GenericMethodWithTResultParameter_ShouldCompileWithoutDiagnostics()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
using System;
using Sample.NamingCollision;

namespace Sample.NamingCollisionUsage
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodGenericResultTypeParameterCollisionTargetImposter();
            var value = imposter.ReturnsGeneric<int>(5);
            var other = imposter.ReturnsWithGenerator<int>(() => 10);
            _ = value + other;
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }
}
