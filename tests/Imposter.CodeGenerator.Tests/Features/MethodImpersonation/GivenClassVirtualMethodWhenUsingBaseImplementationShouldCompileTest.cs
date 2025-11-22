using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.MethodImpersonation.MethodImpersonationTestShared;

namespace Imposter.CodeGenerator.Tests.Features.MethodImpersonation;

public partial class ReturnsAndAsyncSuccessFluentApiTests
{
    [Fact]
    public async Task GivenClassVirtualMethod_WhenUsingBaseImplementation_ShouldCompile()
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
                        imposter.VirtualCompute(Imposter.Abstractions.Arg<int>.Any()).UseBaseImplementation();
                        imposter.VirtualAction().UseBaseImplementation();
                    }
                }
            }
            """
        );

        AssertNoDiagnostics(diagnostics);
    }
}
