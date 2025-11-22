using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.MethodImpersonation.MethodImpersonationTestShared;

namespace Imposter.CodeGenerator.Tests.Features.MethodImpersonation;

public partial class CallbackAndCalledSuccessFluentApiTests
{
    [Fact]
    public async Task GivenMethodThrowsInstance_WhenCallingThen_ShouldCompile()
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
                        imposter.GetNumber()
                            .Throws(new System.Exception("fail"))
                            .Then();
                    }
                }
            }
            """
        );

        AssertNoDiagnostics(diagnostics);
    }
}
