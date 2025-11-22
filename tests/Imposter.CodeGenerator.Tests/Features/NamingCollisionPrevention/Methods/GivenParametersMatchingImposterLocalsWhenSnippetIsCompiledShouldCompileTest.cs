using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Methods;

public partial class MethodParameterCollisionPreventionTests : NamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenParametersMatchingImposterLocals_WhenSnippetIsCompiled_ShouldCompile()
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
"""
        );

        AssertNoDiagnostics(diagnostics);
    }
}
