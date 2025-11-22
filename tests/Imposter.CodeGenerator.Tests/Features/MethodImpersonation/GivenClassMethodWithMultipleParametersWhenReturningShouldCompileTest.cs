using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.MethodImpersonation.MethodImpersonationTestShared;

namespace Imposter.CodeGenerator.Tests.Features.MethodImpersonation;

public partial class ReturnsAndAsyncSuccessFluentApiTests
{
    [Fact]
    public async Task GivenClassMethodWithMultipleParameters_WhenReturning_ShouldCompile()
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
                        imposter.VirtualAdd(
                            Imposter.Abstractions.Arg<int>.Is(x => x > 0),
                            Imposter.Abstractions.Arg<int>.Is(y => y < 10))
                            .Returns(42);
                    }
                }
            }
            """
        );

        AssertNoDiagnostics(diagnostics);
    }
}
