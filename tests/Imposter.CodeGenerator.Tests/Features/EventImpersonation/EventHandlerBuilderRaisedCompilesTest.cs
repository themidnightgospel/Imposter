using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.EventImpersonation.EventImpersonationTestsShared;

namespace Imposter.CodeGenerator.Tests.Features.EventImpersonation;

public partial class EventBuilderFluentApiTests
{
    [Fact]
    public async Task GivenEventHandlerBuilder_WhenVerifyingRaised_ShouldCompile()
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
                            .Raised(global::Imposter.Abstractions.Arg<object?>.Any(), global::Imposter.Abstractions.Arg<System.EventArgs>.Any(), global::Imposter.Abstractions.Count.Exactly(1));
                    }
                }
            }
            """
        );

        AssertNoDiagnostics(diagnostics);
    }
}
