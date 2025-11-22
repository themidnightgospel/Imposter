using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Events;

public partial class EventImposterStateCollisionPreventionTests
    : EventNamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenEventsMatchingBuilderParameterNames_WhenSnippetIsCompiled_ShouldCompile()
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
            var imposter = new EventBuilderParameterCollisionTargetImposter();
            var handler = new EventHandler((sender, arg) => { });
            var sender = new object();
            var args = EventArgs.Empty;
            var criteria = Arg<EventHandler>.Any();
            var count = Count.AtLeast(1);

            imposter.handler.Raise(sender, args);
            imposter.handler.Callback((eventSender, eventArgs) => handler(eventSender, eventArgs));

            imposter.interceptor.OnSubscribe(it => { });
            imposter.criteria.Subscribed(criteria, count);
            imposter.handlerCriteria.HandlerInvoked(criteria, count);
            imposter.callback.Callback(handler.Invoke);
            imposter.baseImplementation.UseBaseImplementation();
            imposter.baseImplementation.OnSubscribe(it => { });
            imposter.baseImplementation.OnUnsubscribe(it => { });
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }
}
