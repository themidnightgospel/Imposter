using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Events;

public class EventBaseImplementationCollisionPreventionTests : EventNamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenVirtualEventsMatchingBaseImplementationFlow_WhenSnippetIsCompiled_ShouldCompile()
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
            var imposter = new EventBaseImplementationCollisionTargetImposter();
            var handler = new EventHandler((_, _) => { });
            var count = Count.AtLeast(1);

            imposter.UseBaseImplementation.UseBaseImplementation();
            imposter.UseBaseImplementation.Subscribe(handler, () => { });
            imposter.UseBaseImplementation.Unsubscribe(handler, () => { });

            imposter.Subscribe.Subscribe(handler, () => { });
            imposter.Unsubscribe.Unsubscribe(handler, () => { });
            imposter.Then.HandlerInvoked(Arg<EventHandler>.Any(), count);
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenRaiseAsyncEventsWithConflictingNames_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
using System;
using System.Threading.Tasks;
using Sample.NamingCollision;

namespace Sample.NamingCollisionUsage
{
    public static class Scenario
    {
        public static async Task ExecuteAsync()
        {
            var asyncImposter = new IAsyncRaiseAsyncEventCollisionTargetImposter();
            var sender = new object();

            asyncImposter.RaiseAsync.Raise(sender, EventArgs.Empty);
            await asyncImposter.RaiseAsync.RaiseAsync(sender, EventArgs.Empty);

            var syncImposter = new INonAsyncRaiseAsyncEventCollisionTargetImposter();
            syncImposter.RaiseAsync.Raise(sender, EventArgs.Empty);
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenDuplicateEventsAcrossInheritance_WhenSnippetIsCompiled_ShouldCompile()
    {
        var diagnostics = await CompileSnippet(/*lang=csharp*/"""
using System;
using Sample.NamingCollision;

namespace Sample.NamingCollisionUsage
{
    public static class Scenario
    {
        public static void Execute()
        {
            var child = new IEventDuplicateChildCollisionTargetImposter();
            child.Raise.Subscribe(new EventHandler((_, _) => { }));

            var parent = new IEventDuplicateBaseCollisionTargetImposter();
            parent.Raise.Subscribe(new EventHandler((_, _) => { }));
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenMultipleOperationNamedEventsOnSameType_WhenSnippetIsCompiled_ShouldCompile()
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
            var imposter = new IEventOperationClusterCollisionTargetImposter();
            var sender = new object();
            var handler = new EventHandler((_, _) => { });
            var count = Count.AtLeast(1);

            imposter.Raise.Raise(sender, EventArgs.Empty);
            imposter.Subscribe.Subscribe(handler);
            imposter.Callback.Callback(handler);
            imposter.Raised.Raised(Arg<object>.Any(), Arg<EventArgs>.Any(), count);
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }
}


