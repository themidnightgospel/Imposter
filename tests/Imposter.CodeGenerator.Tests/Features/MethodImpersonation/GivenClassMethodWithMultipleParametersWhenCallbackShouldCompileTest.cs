using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.MethodImpersonation.MethodImpersonationTestShared;

namespace Imposter.CodeGenerator.Tests.Features.MethodImpersonation;

public partial class ReturnsAndAsyncSuccessFluentApiTests
{
    [Fact]
    public async Task GivenClassMethodWithMultipleParameters_WhenCallback_ShouldCompile()
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
                        imposter.VirtualAdd(Imposter.Abstractions.Arg<int>.Any(), Imposter.Abstractions.Arg<int>.Any())
                            .Callback((int left, int right) => { });
                    }
                }
            }
            """
        );

        AssertNoDiagnostics(diagnostics);
    }
}
