using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Methods;

public class MethodGenericParameterCollisionPreventionTests : NamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenGenericMethodWithTargetNamedTypeParameters_WhenSnippetIsCompiled_ShouldCompile()
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
    public async Task GivenGenericMethodWithTExceptionParameter_WhenSnippetIsCompiled_ShouldCompile()
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
    public async Task GivenGenericMethodWithTResultParameter_WhenSnippetIsCompiled_ShouldCompile()
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
            Func<int> defaultResultGenerator = () => 10;
            var valueSetup = imposter.ReturnsGeneric<int>(5);
            var otherSetup = imposter.ReturnsWithGenerator<int>(defaultResultGenerator);
            valueSetup.Returns(5);
            otherSetup.Returns(10);
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }
}




