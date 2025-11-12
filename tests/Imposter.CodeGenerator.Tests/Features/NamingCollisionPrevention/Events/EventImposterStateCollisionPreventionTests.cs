using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Events;

public class EventImposterStateCollisionPreventionTests : EventNamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenEventsMatchingImposterMembers_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
using System;
using Imposter.Abstractions;
using Sample.NamingCollision;

namespace Sample.NamingCollisionUsage
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IEventImposterMemberCollisionTargetImposter();
            var sender = new object();
            var args = EventArgs.Empty;

            var instance = ((IHaveImposterInstance<IEventImposterMemberCollisionTarget>)imposter).Instance();
            _ = instance;

            imposter.Instance.Raise(sender, args);
            imposter.ImposterTargetInstance.Raise(sender, args);
            imposter._imposterInstance.Raise(sender, args);
            imposter._invocationBehavior.Raise(sender, args);
            imposter.Default.Raised(Arg<object>.Any(), Arg<EventArgs>.Any(), Count.AtLeast(1));
            imposter.Count.HandlerInvoked(Arg<EventHandler>.Any(), Count.AtLeast(1));
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenEventsMatchingBuilderFieldNames_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
using System;
using Imposter.Abstractions;
using Sample.NamingCollision;

namespace Sample.NamingCollisionUsage
{
    public static class Scenario
    {
        public static void Execute()
        {
            var imposter = new IEventBuilderFieldCollisionTargetImposter();
            var sender = new object();

            imposter._MyEvent.Raise(sender, EventArgs.Empty);
            imposter._handlerOrder.OnSubscribe(it => { });
            imposter._handlerInvocations.HandlerInvoked(Arg<EventHandler>.Any(), Count.AtLeast(1));
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenEventsMatchingBuilderParameterNames_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
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
""");

        AssertNoDiagnostics(diagnostics);
    }
}


