using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.EventImpersonation.EventImpersonationTestsShared;

namespace Imposter.CodeGenerator.Tests.Features.EventImpersonation;

public partial class EventBuilderFluentApiTests
{
    [Fact]
    public async Task GivenActionEventBuilder_WhenCallingRaise_ShouldCompile()
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
                        imposter.ActionOnly.Raise();
                    }
                }
            }
            """
        );

        AssertNoDiagnostics(diagnostics);
    }
}
