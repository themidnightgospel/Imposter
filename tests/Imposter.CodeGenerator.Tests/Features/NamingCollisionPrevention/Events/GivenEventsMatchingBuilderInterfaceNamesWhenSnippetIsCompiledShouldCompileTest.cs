using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Events;

public partial class EventBuilderOperationCollisionPreventionTests
    : EventNamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenEventsMatchingBuilderInterfaceNames_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
using System;
using Imposter.Abstractions;
using Sample.NamingCollision;

namespace Sample.NamingCollisionUsage
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IEventBuilderInterfaceCollisionTargetImposter();
            var count = Count.AtLeast(1);

            imposter.IUniqueEventImposterBuilder.Subscribed(Arg<EventHandler>.Any(), count);
            imposter.IUniqueEventImposterSetupBuilder.Raise(new object(), EventArgs.Empty);
            imposter.IUniqueEventImposterVerificationBuilder.Raised(Arg<object>.Any(), Arg<EventArgs>.Any(), count);
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }
}
