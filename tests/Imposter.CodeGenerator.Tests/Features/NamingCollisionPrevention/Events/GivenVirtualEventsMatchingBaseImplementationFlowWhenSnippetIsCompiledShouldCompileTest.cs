using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Events;

public partial class EventBaseImplementationCollisionPreventionTests
    : EventNamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenVirtualEventsMatchingBaseImplementationFlow_WhenSnippetIsCompiled_ShouldCompile()
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
            var imposter = new EventBaseImplementationCollisionTargetImposter();
            var sender = new object();
            var args = EventArgs.Empty;
            var count = Count.AtLeast(1);

            imposter.UseBaseImplementation.UseBaseImplementation();
            imposter.UseBaseImplementation.OnSubscribe(_ => { });
            imposter.UseBaseImplementation.OnUnsubscribe(_ => { });

            imposter.Subscribe.Raise(sender, args);
            imposter.Unsubscribe.Raise(sender, args);
            imposter.Then.HandlerInvoked(Arg<EventHandler>.Any(), count);
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }
}
