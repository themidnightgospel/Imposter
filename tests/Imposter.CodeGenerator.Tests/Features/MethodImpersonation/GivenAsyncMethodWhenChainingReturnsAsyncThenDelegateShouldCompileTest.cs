using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.MethodImpersonation.MethodImpersonationTestShared;

namespace Imposter.CodeGenerator.Tests.Features.MethodImpersonation;

public partial class ReturnsAndAsyncSuccessFluentApiTests
{
    [Fact]
    public async Task GivenAsyncMethod_WhenChainingReturnsAsyncThenDelegate_ShouldCompile()
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
                        imposter.GetNumberAsync()
                            .ReturnsAsync(1)
                            .Then()
                            .Returns(() => System.Threading.Tasks.Task.FromResult(2));
                    }
                }
            }
            """
        );

        AssertNoDiagnostics(diagnostics);
    }
}
