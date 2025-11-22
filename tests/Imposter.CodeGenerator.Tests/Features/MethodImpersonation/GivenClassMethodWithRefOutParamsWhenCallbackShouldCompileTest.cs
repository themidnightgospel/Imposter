using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.MethodImpersonation.MethodImpersonationTestShared;

namespace Imposter.CodeGenerator.Tests.Features.MethodImpersonation;

public partial class ReturnsAndAsyncSuccessFluentApiTests
{
    [Fact]
    public async Task GivenClassMethodWithRefOutParams_WhenCallback_ShouldCompile()
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
                        imposter.VirtualRefOutParams(
                            Imposter.Abstractions.Arg<int>.Any(),
                            Imposter.Abstractions.OutArg<int>.Any(),
                            Imposter.Abstractions.Arg<string[]>.Any())
                            .Callback((ref int seed, out int doubled, string[] _) => doubled = seed * 2);
                    }
                }
            }
            """
        );

        AssertNoDiagnostics(diagnostics);
    }
}
