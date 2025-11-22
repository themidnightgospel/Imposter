using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Methods;

public partial class MethodNameCollisionPreventionTests : NamingCollisionPreventionTestsBase
{
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
