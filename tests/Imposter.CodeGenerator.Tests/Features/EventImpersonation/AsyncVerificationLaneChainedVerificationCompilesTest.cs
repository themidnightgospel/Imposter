using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.EventImpersonation.EventImpersonationTestsShared;

namespace Imposter.CodeGenerator.Tests.Features.EventImpersonation;

public partial class EventBuilderFluentApiTests
{
    [Fact]
    public async Task GivenAsyncVerificationLane_WhenChainingVerificationMethods_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
            namespace Sample
            {
                public static class Scenario
                {
                    public static void Execute()
                    {
                        var imposter = new IEventSutImposter();
                        imposter.AsyncSomethingHappened
                            .Subscribed(global::Imposter.Abstractions.Arg<System.Func<object?, System.EventArgs, System.Threading.Tasks.Task>>.Any(), global::Imposter.Abstractions.Count.Exactly(1))
                            .Unsubscribed(global::Imposter.Abstractions.Arg<System.Func<object?, System.EventArgs, System.Threading.Tasks.Task>>.Any(), global::Imposter.Abstractions.Count.AtLeast(0))
                            .Raised(global::Imposter.Abstractions.Arg<object?>.Any(), global::Imposter.Abstractions.Arg<System.EventArgs>.Any(), global::Imposter.Abstractions.Count.AtMost(2))
                            .HandlerInvoked(global::Imposter.Abstractions.Arg<System.Func<object?, System.EventArgs, System.Threading.Tasks.Task>>.Any(), global::Imposter.Abstractions.Count.Exactly(1));
                    }
                }
            }
            """
        );

        AssertNoDiagnostics(diagnostics);
    }
}
