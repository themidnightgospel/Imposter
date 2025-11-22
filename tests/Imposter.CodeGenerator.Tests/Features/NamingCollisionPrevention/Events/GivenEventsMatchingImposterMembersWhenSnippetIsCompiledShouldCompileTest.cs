using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Events;

public partial class EventImposterStateCollisionPreventionTests
    : EventNamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenEventsMatchingImposterMembers_WhenSnippetIsCompiled_ShouldCompile()
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
"""
        );

        AssertNoDiagnostics(diagnostics);
    }
}
