using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.MethodImpersonation.MethodImpersonationTestShared;

namespace Imposter.CodeGenerator.Tests.Features.MethodImpersonation;

public partial class ReturnsAndAsyncSuccessFluentApiTests
{
    [Fact]
    public async Task GivenInterfaceMethodWithRefOutParams_WhenReturning_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
            namespace Sample
            {
                public static class Scenario
                {
                    public static void Execute()
                    {
                        var imposter = new IMethodInterfaceImposter();
                        imposter.RefOutParams(
                            Imposter.Abstractions.Arg<int>.Is(value => value > 0),
                            Imposter.Abstractions.OutArg<int>.Any(),
                            Imposter.Abstractions.Arg<string[]>.Any())
                            .Returns(5);
                    }
                }
            }
            """
        );

        AssertNoDiagnostics(diagnostics);
    }
}
