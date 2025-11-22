using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.EventImpersonation.EventImpersonationTestsShared;

namespace Imposter.CodeGenerator.Tests.Features.EventImpersonation;

public partial class EventBuilderFluentApiTests
{
    [Fact]
    public async Task GivenAsyncSetupLane_WhenSwitchingToVerificationAfterAwait_ShouldFail()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
            namespace Sample
            {
                public static class Scenario
                {
                    public static async System.Threading.Tasks.Task ExecuteAsync()
                    {
                        var imposter = new IEventSutImposter();
                        var builder = await imposter.AsyncSomethingHappened.RaiseAsync(new object(), System.EventArgs.Empty);
                        builder.Subscribed(default!, default);
                    }
                }
            }
            """
        );

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound);
    }
}
