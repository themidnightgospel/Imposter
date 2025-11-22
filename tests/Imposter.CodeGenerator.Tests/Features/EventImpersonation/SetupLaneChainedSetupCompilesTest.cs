using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.EventImpersonation.EventImpersonationTestsShared;

namespace Imposter.CodeGenerator.Tests.Features.EventImpersonation;

public partial class EventBuilderFluentApiTests
{
    [Fact]
    public async Task GivenSetupLane_WhenChainingSetupMethods_ShouldCompile()
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
                        imposter.SomethingHappened
                            .Callback((sender, args) => { })
                            .OnSubscribe(handler => { })
                            .OnUnsubscribe(handler => { })
                            .Raise(new object(), System.EventArgs.Empty);
                    }
                }
            }
            """
        );

        AssertNoDiagnostics(diagnostics);
    }
}
