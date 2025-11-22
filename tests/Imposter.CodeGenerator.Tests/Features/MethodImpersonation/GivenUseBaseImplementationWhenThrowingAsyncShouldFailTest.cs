using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.MethodImpersonation.MethodImpersonationTestShared;

namespace Imposter.CodeGenerator.Tests.Features.MethodImpersonation;

public partial class ReturnsAndBaseImplementationFailuresFluentApiTests
{
    [Fact]
    public async Task GivenUseBaseImplementation_WhenThrowingAsync_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
            namespace Sample
            {
                public static class Scenario
                {
                    public static void Execute()
                    {
                        var imposter = new ClassMethodTargetImposter();
                        imposter.VirtualComputeAsync(Imposter.Abstractions.Arg<int>.Any()).UseBaseImplementation().ThrowsAsync(new System.Exception("fail"));
                    }
                }
            }
            """
        );

        AssertSingleDiagnostic(
            diagnostics,
            WellKnownCsCompilerErrorCodes.MemberNotFound,
            expectedLine: 8
        );
    }
}
