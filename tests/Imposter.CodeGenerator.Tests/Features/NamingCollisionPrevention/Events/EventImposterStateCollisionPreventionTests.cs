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
            var handler = new EventHandler((_, _) => { });

            var instance = ((IHaveImposterInstance<IEventImposterMemberCollisionTarget>)imposter).Instance();
            _ = instance;

            imposter.Instance.Subscribe(handler);
            imposter.ImposterTargetInstance.Subscribe(handler);
            imposter._imposterInstance.Subscribe(handler);
            imposter._invocationBehavior.Subscribe(handler);
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
            imposter._handlerOrder.Subscribe(new EventHandler((_, _) => { }));
            imposter._handlerCounts.CountMatches(Arg<EventHandler>.Any());
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
            var handler = new EventHandler((_, _) => { });
            var criteria = Arg<EventHandler>.Any();
            var count = Count.AtLeast(1);

            imposter.handler.Subscribe(handler);
            imposter.handler.Subscribe(handler, () => { });

            imposter.interceptor.OnSubscribe(_ => { });
            imposter.criteria.Subscribed(criteria, count);
            imposter.handlerCriteria.HandlerInvoked(criteria, count);
            imposter.count.CountMatches(criteria);
            imposter.count.EnsureCountMatches(criteria, count);
            imposter.callback.Callback(handler);
            imposter.baseImplementation.Subscribe(handler, () => { });
            imposter.baseImplementation.Unsubscribe(handler, () => { });
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }
}


