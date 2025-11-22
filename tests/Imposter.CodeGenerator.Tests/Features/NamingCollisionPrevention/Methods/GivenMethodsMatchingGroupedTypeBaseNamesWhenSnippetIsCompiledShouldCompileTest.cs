using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Methods;

public partial class MethodNameCollisionPreventionTests : NamingCollisionPreventionTestsBase
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
}
