using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.MethodImpersonation.MethodImpersonationTestShared;

namespace Imposter.CodeGenerator.Tests.Features.MethodImpersonation;

public partial class CallbackAndCalledFailuresFluentApiTests
{
    [Fact]
    public async Task GivenMethodCallback_WhenCallingCalled_ShouldFail()
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
                .Callback(default(IMethodInterfaceImposter.GetNumberCallbackDelegate))
                .Called(Imposter.Abstractions.Count.Exactly(1));
        }
    }
}
"""
        );

        AssertSingleDiagnostic(
            diagnostics,
            WellKnownCsCompilerErrorCodes.MemberNotFound,
            expectedLine: 10
        );
    }
}
