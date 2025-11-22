using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.EventImpersonation.EventImpersonationTestsShared;

namespace Imposter.CodeGenerator.Tests.Features.EventImpersonation;

public partial class EventBuilderFluentApiTests
{
    [Fact]
    public async Task GivenAsyncEventBuilder_WhenAccessingCalled_ShouldFail()
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
                        imposter.AsyncSomethingHappened.Called(global::Imposter.Abstractions.Count.Once());
                    }
                }
            }
            """
        );

        AssertSingleDiagnostic(diagnostics, WellKnownCsCompilerErrorCodes.MemberNotFound);
    }
}
