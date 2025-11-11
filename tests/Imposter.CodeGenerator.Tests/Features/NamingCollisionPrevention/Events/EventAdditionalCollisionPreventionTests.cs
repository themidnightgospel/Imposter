using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Events;

public class EventAdditionalCollisionPreventionTests : EventNamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenEventsMatchingTypeOrNamespaceSegments_WhenSnippetIsCompiled_ShouldCompile()
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
            var imposter = new IEventTypeNameCollisionTargetImposter();
            var handler = new EventHandler((sender, args) => { });
            var sender = new object();

            imposter.Imposter.Subscribe(handler);
            imposter.CodeGenerator.Subscribe(handler);
            imposter.Sample.Raise(sender, EventArgs.Empty);
            imposter.NamingCollision.Raised(Arg<object>.Any(), Arg<EventArgs>.Any(), Count.AtLeast(1));
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }

    [Fact]
    public async Task GivenEventsDifferingOnlyByCase_WhenSnippetIsCompiled_ShouldCompile()
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
            var imposter = new IEventCaseSensitivityCollisionTargetImposter();
            var sender = new object();

            var lower = imposter.raise;
            lower.Raise(sender, EventArgs.Empty);
            lower.Subscribed(Arg<EventHandler>.Any(), Count.AtLeast(1));

            var upper = imposter.Raise;
            upper.Raise(sender, EventArgs.Empty);
            upper.Subscribed(Arg<EventHandler>.Any(), Count.AtLeast(1));
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }
}
