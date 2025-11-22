using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Methods;

public partial class MethodNameCollisionPreventionTests : NamingCollisionPreventionTestsBase
{
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
}
