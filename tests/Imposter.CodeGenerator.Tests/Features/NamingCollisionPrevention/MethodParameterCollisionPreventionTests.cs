using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention;

public class MethodParameterCollisionPreventionTests : NamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task Given_ParametersMatchingImposterLocals_ShouldCompileWithoutDiagnostics()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
using Sample.NamingCollision;

namespace Sample.NamingCollisionUsage
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodParameterLocalNameCollisionTargetImposter();
            var value = imposter.InvokeWithLocals(
                ex: 1,
                result: 2,
                matchingInvocationImposterGroup: 3,
                arguments: 4,
                baseImplementation: 5);

            _ = value;
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task Given_ParametersMatchingInvocationSetupNames_ShouldCompileWithoutDiagnostics()
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
            var imposter = new IMethodParameterInvocationSetupCollisionTargetImposter();
            var value = imposter.SetupNames(
                value: 1,
                resultGenerator: () => 10,
                exception: new InvalidOperationException(),
                exceptionGenerator: () => new ApplicationException(),
                callback: () => { });

            _ = value;
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task Given_ParametersMatchingArgumentsCriteriaNames_ShouldCompileWithoutDiagnostics()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
using Sample.NamingCollision;

namespace Sample.NamingCollisionUsage
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodParameterArgumentsCriteriaCollisionTargetImposter();
            imposter.CriteriaNames(As: 1, Matches: 2);
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task Given_ParametersMatchingInvocationHistoryNames_ShouldCompileWithoutDiagnostics()
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
            var imposter = new IMethodParameterHistoryCollisionTargetImposter();
            var arguments = 0;
            int result;
            Exception exception;

            imposter.HistoryNames(ref arguments, out result, out exception);
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task Given_ParametersMatchingAdapterNames_ShouldCompileWithoutDiagnostics()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
using Sample.NamingCollision;

namespace Sample.NamingCollisionUsage
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodParameterAdapterCollisionTargetImposter();
            var value = imposter.AdapterNames(result: 1, arguments: "label", target: new object(), _target: new object());
            _ = value;
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task Given_ParametersMatchingBaseImplementationAdapterName_ShouldCompileWithoutDiagnostics()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
using Sample.NamingCollision;

namespace Sample.NamingCollisionUsage
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodParameterBaseImplementationCollisionTargetImposter();
            var value = imposter.BaseImplementationName(baseImplementation: 42);
            _ = value;
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }
}
