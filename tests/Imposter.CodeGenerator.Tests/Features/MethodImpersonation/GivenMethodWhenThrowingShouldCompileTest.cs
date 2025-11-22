using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.MethodImpersonation.MethodImpersonationTestShared;

namespace Imposter.CodeGenerator.Tests.Features.MethodImpersonation;

public partial class ReturnsAndAsyncSuccessFluentApiTests
{
    [Fact]
    public async Task GivenMethod_WhenThrowing_ShouldCompile()
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
                        imposter.GetNumber().Throws<System.InvalidOperationException>();
                        imposter.GetNumber().Throws(new System.Exception("fail"));
                        imposter.GetNumber().Throws(() => new System.Exception("fail"));
                    }
                }
            }
            """
        );

        AssertNoDiagnostics(diagnostics);
    }
}
