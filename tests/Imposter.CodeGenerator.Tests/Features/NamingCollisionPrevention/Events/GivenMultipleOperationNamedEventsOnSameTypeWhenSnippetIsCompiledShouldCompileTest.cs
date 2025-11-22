using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Events;

public partial class EventBaseImplementationCollisionPreventionTests
    : EventNamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenMultipleOperationNamedEventsOnSameType_WhenSnippetIsCompiled_ShouldCompile()
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
            var imposter = new IEventOperationClusterCollisionTargetImposter();
            var sender = new object();
            var handler = new EventHandler((sender, args) => { });
            var count = Count.AtLeast(1);

            imposter.Raise.Raise(sender, EventArgs.Empty);
            imposter.Subscribe.Subscribed(Arg<EventHandler>.Any(), count);
            imposter.Callback.Callback(handler.Invoke);
            imposter.Raised.Raised(Arg<object>.Any(), Arg<EventArgs>.Any(), count);
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }
}
