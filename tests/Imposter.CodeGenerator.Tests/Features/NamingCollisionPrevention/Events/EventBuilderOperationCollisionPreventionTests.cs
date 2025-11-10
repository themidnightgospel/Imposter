using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Events;

public class EventBuilderOperationCollisionPreventionTests : EventNamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenEventsMatchingBuilderOperations_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
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
            var handler = new EventHandler((_, _) => { });

            imposter.Raise.Raise(sender, args);

            var asyncBuilder = imposter.RaiseAsync;
            asyncBuilder.Raise(sender, args);
            await asyncBuilder.RaiseAsync(sender, args);

            imposter.Subscribe.Subscribe(handler);
            imposter.Unsubscribe.Unsubscribe(handler);

            imposter.Callback.Callback(handler);

            imposter.OnSubscribe.OnSubscribe(_ => { });
            imposter.OnUnsubscribe.OnUnsubscribe(_ => { });

            var criteria = Arg<EventHandler>.Any();
            var count = Count.AtLeast(1);

            imposter.Subscribed.Subscribed(criteria, count);
            imposter.Unsubscribed.Unsubscribed(criteria, count);
            imposter.HandlerInvoked.HandlerInvoked(criteria, count);

            imposter.CountMatches.CountMatches(criteria);
            imposter.EnsureCountMatches.EnsureCountMatches(criteria, count);
            imposter.Raised.Raised(Arg<object>.Any(), Arg<EventArgs>.Any(), count);
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenEventsMatchingBuilderInternalNames_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
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
            var imposter = new IEventBuilderInternalNameCollisionTargetImposter();
            var sender = new object();

            imposter.RaiseInternal.Raise(sender, EventArgs.Empty);

            var asyncBuilder = imposter.RaiseCoreAsync;
            asyncBuilder.Raise(sender, EventArgs.Empty);
            await asyncBuilder.RaiseAsync(sender, EventArgs.Empty);

            imposter.EnumerateActiveHandlers.CountMatches(Arg<EventHandler>.Any());
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenEventsMatchingBuilderInterfaceNames_WhenSnippetIsCompiled_ShouldCompile()
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
            var imposter = new IEventBuilderInterfaceCollisionTargetImposter();
            var handler = new EventHandler((_, _) => { });
            var count = Count.AtLeast(1);

            imposter.IUniqueEventImposterBuilder.Subscribe(handler);
            imposter.IUniqueEventImposterSetupBuilder.Raise(new object(), EventArgs.Empty);
            imposter.IUniqueEventImposterVerificationBuilder.Raised(Arg<object>.Any(), Arg<EventArgs>.Any(), count);
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }
}


