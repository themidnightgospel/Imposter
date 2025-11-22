using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Events;

public partial class EventAdditionalCollisionPreventionTests : EventNamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenEventsMatchingTypeOrNamespaceSegments_WhenSnippetIsCompiled_ShouldCompile()
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
            var imposter = new IEventTypeNameCollisionTargetImposter();
            var sender = new object();

            imposter.Imposter.Subscribed(Arg<EventHandler>.Any(), Count.AtLeast(1));
            imposter.CodeGenerator.Subscribed(Arg<EventHandler>.Any(), Count.AtLeast(1));
            imposter.Sample.Raise(sender, EventArgs.Empty);
            imposter.NamingCollision.Raised(Arg<object>.Any(), Arg<EventArgs>.Any(), Count.AtLeast(1));
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }

}

