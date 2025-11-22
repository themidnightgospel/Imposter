using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.MethodImpersonation.MethodImpersonationTestShared;

namespace Imposter.CodeGenerator.Tests.Features.MethodImpersonation;

public partial class CallbackAndCalledFailuresFluentApiTests
{
    [Fact]
    public async Task GivenMethodCallback_WhenUsingBaseImplementation_ShouldFail()
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
            imposter.VirtualCompute(Imposter.Abstractions.Arg<int>.Any())
                .Callback(default(ClassMethodTargetImposter.VirtualComputeCallbackDelegate))
                .UseBaseImplementation();
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
