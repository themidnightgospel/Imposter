using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.MethodImpersonation.MethodImpersonationTestShared;

namespace Imposter.CodeGenerator.Tests.Features.MethodImpersonation;

public partial class ReturnsAndAsyncSuccessFluentApiTests
{
    [Fact]
    public async Task GivenClassAsyncMethod_WhenReturningAsyncValue_ShouldCompile()
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
                        imposter.VirtualComputeAsync(Imposter.Abstractions.Arg<int>.Any())
                            .ReturnsAsync(42);
                    }
                }
            }
            """
        );

        AssertNoDiagnostics(diagnostics);
    }
}
