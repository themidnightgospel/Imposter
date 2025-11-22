using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Events;

public partial class EventBuilderOperationCollisionPreventionTests
    : EventNamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenEventsMatchingBuilderOperations_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet( /*lang=csharp*/
            """
using System;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Sample.NamingCollision;

namespace Sample.NamingCollisionUsage
{
    public static class Scenario
    {
        public static async Task ExecuteAsync()
        {
            var imposter = new IEventBuilderOperationCollisionTargetImposter();
            var sender = new object();
            var args = EventArgs.Empty;
            var handler = new EventHandler((sender, args) => { });

            imposter.Raise.Raise(sender, args);

            var asyncBuilder = imposter.RaiseAsync;
            await asyncBuilder.RaiseAsync(sender, args);
            await asyncBuilder.RaiseAsync(sender, args);

            imposter.Subscribe.Raise(sender, args);
            imposter.Unsubscribe.Raise(sender, args);

            imposter.Callback.Callback(handler.Invoke);

            imposter.OnSubscribe.OnSubscribe(interceptor => { });
            imposter.OnUnsubscribe.OnUnsubscribe(interceptor => { });

            var criteria = Arg<EventHandler>.Any();
            var count = Count.AtLeast(1);

            imposter.Subscribed.Subscribed(criteria, count);
            imposter.Unsubscribed.Unsubscribed(criteria, count);
            imposter.HandlerInvoked.HandlerInvoked(criteria, count);

            imposter.Raised.Raised(Arg<object>.Any(), Arg<EventArgs>.Any(), count);
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }
}
