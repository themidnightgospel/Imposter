using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Methods;

public partial class MethodNameCollisionPreventionTests : NamingCollisionPreventionTestsBase
{
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
}
