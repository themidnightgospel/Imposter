using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Methods;

public class MethodParameterCollisionPreventionTests : NamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenParametersMatchingImposterLocals_WhenSnippetIsCompiled_ShouldCompile()
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
    public async Task GivenParametersMatchingInvocationSetupNames_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
using System;
using Sample.NamingCollision;
using Imposter.Abstractions;

namespace Sample.NamingCollisionUsage
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodParameterInvocationSetupCollisionTargetImposter();
            var value = imposter.SetupNames(
                value: 1,
                resultGenerator: Arg<Func<int>>.Any(), 
                exception: new InvalidOperationException(),
                exceptionGenerator: Arg<Func<Exception>>.Any(), 
                callback: Arg<Action>.Any());

            _ = value;
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenParametersMatchingArgumentsCriteriaNames_WhenSnippetIsCompiled_ShouldCompile()
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
    public async Task GivenParametersMatchingInvocationHistoryNames_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
using System;
using Sample.NamingCollision;
using Imposter.Abstractions;

namespace Sample.NamingCollisionUsage
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodParameterHistoryCollisionTargetImposter();
            imposter.HistoryNames(Arg<int>.Any(), OutArg<int>.Any(), OutArg<Exception>.Any());
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenParametersMatchingAdapterNames_WhenSnippetIsCompiled_ShouldCompile()
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
    public async Task GivenParametersMatchingBaseImplementationAdapterName_WhenSnippetIsCompiled_ShouldCompile()
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




