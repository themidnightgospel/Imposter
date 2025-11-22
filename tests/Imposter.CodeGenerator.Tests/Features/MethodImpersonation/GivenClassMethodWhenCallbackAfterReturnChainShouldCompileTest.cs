using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.MethodImpersonation.MethodImpersonationTestShared;

namespace Imposter.CodeGenerator.Tests.Features.MethodImpersonation;

public partial class ReturnsAndAsyncSuccessFluentApiTests
{
    [Fact]
    public async Task GivenClassMethod_WhenCallbackAfterReturnChain_ShouldCompile()
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
                        imposter.VirtualCompute(Imposter.Abstractions.Arg<int>.Is(x => x > 0))
                            .Returns(123)
                            .Then()
                            .Callback((int _) => { });
                    }
                }
            }
            """
        );

        AssertNoDiagnostics(diagnostics);
    }
}
