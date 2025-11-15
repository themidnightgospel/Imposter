using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Methods;

public class MethodNameCollisionPreventionTests : NamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenMethodsMatchingGroupedTypeBaseNames_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
using Sample.NamingCollision;

namespace Sample.NamingCollisionUsage
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodGroupedTypeCollisionTargetImposter();
            _ = imposter.MethodImposter(0);
            _ = imposter.MethodInvocationImposterGroup("");
            _ = imposter.MethodInvocationHistory();
            _ = imposter.ArgumentsCriteria(0, 1);
            _ = imposter.Arguments("");
            _ = imposter.Delegate(0);
            _ = imposter.CallbackDelegate(0, 0);
            _ = imposter.ExceptionGeneratorDelegate(default);
            _ = imposter.Collection(0);
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenOverloadedMethodNames_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
using Sample.NamingCollision;

namespace Sample.NamingCollisionUsage
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodOverloadCollisionTargetImposter();
            _ = imposter.Duplicate();
            _ = imposter.Duplicate(1);
            _ = imposter.Duplicate("", 2);
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenMethodsMatchingBuilderOperationNames_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
using Sample.NamingCollision;

namespace Sample.NamingCollisionUsage
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodOperationNameCollisionTargetImposter();
            _ = imposter.Returns();
            _ = imposter.ReturnsAsync();
            _ = imposter.Throws();
            _ = imposter.ThrowsAsync();
            _ = imposter.Callback();
            _ = imposter.Then();
            _ = imposter.UseBaseImplementation();
            _ = imposter.DefaultResultGenerator();
            _ = imposter.Default();
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenMethodsMatchingHelperNames_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
using Sample.NamingCollision;

namespace Sample.NamingCollisionUsage
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodHelperNameCollisionTargetImposter();
            _ = imposter.Adapter();
            _ = imposter.HasMatchingInvocationImposterGroup();
            _ = imposter.As("");
            _ = imposter.Invoke(0);
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenMethodsMatchingImposterMembers_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
using Sample.NamingCollision;
using Imposter.Abstractions;

namespace Sample.NamingCollisionUsage
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IMethodImposterMemberCollisionTargetImposter();
            _ = imposter.Instance();
            _ = imposter.ImposterTargetInstance();
            _ = imposter._imposterInstance();
            _ = imposter._invocationBehavior();

            var seed = 0;
            _ = imposter.InitializeOutParametersWithDefaultValues(Arg<int>.Any(), OutArg<int>.Any());
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }
}
