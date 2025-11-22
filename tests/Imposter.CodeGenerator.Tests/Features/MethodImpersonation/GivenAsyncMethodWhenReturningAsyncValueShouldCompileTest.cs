using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.MethodImpersonation.MethodImpersonationTestShared;

namespace Imposter.CodeGenerator.Tests.Features.MethodImpersonation;

public partial class ReturnsAndAsyncSuccessFluentApiTests
{
    [Fact]
    public async Task GivenAsyncMethod_WhenReturningAsyncValue_ShouldCompile()
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
                        imposter.GetNumberAsync().ReturnsAsync(42);
                    }
                }
            }
            """
        );

        AssertNoDiagnostics(diagnostics);
    }
}
