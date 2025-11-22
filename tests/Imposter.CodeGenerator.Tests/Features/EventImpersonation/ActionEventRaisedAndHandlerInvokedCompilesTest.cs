using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.EventImpersonation.EventImpersonationTestsShared;

namespace Imposter.CodeGenerator.Tests.Features.EventImpersonation;

public partial class EventBuilderFluentApiTests
{
    [Fact]
    public async Task GivenActionEventBuilder_WhenVerifyingRaisedAndHandlerInvoked_ShouldCompile()
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
                        imposter.ActionOnly
                            .Raised(global::Imposter.Abstractions.Count.Once())
                            .HandlerInvoked(global::Imposter.Abstractions.Arg<System.Action>.Any(), global::Imposter.Abstractions.Count.Once());
                    }
                }
            }
            """
        );

        AssertNoDiagnostics(diagnostics);
    }
}
