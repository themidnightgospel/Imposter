using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.EventImpersonation.EventImpersonationTestsShared;

namespace Imposter.CodeGenerator.Tests.Features.EventImpersonation;

public partial class EventBuilderFluentApiTests
{
    [Fact]
    public async Task GivenAsyncSetupLane_WhenAwaitingRaiseAsync_ShouldAllowFurtherSetup()
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
                        builder = await builder
                            .OnUnsubscribe(handler => { })
                            .Callback((sender, args) => System.Threading.Tasks.Task.CompletedTask)
                            .RaiseAsync(new object(), System.EventArgs.Empty);

                        builder.OnSubscribe(handler => { });
                        await builder.RaiseAsync(new object(), System.EventArgs.Empty);
                    }
                }
            }
            """
        );

        AssertNoDiagnostics(diagnostics);
    }
}
