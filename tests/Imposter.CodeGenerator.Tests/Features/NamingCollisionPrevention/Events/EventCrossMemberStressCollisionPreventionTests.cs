using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Events;

public class EventCrossMemberStressCollisionPreventionTests
    : EventNamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task GivenCrossMemberCollisionNames_WhenSnippetIsCompiled_ShouldCompile()
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
            var imposter = new ICrossMemberEventCollisionTargetImposter();
            var sender = new object();
            var args = EventArgs.Empty;
            var count = Count.AtLeast(1);

            var instance = ((IHaveImposterInstance<ICrossMemberEventCollisionTarget>)imposter).Instance();
            _ = instance;

            imposter.Instance.Raise(sender, args);
            imposter.IChaosEventImposterBuilder.Raise(sender, args);
            imposter.IChaosEventImposterSetupBuilder.Raise(sender, args);
            imposter.IChaosEventImposterVerificationBuilder.Raised(Arg<object>.Any(), Arg<EventArgs>.Any(), count);

            imposter.Raise.Raise(sender, args);
            imposter.Subscribe.Subscribed(Arg<EventHandler>.Any(), count);
            imposter._imposterInstance.Raise(sender, args);
            imposter._invocationBehavior.Raise(sender, args);
            imposter.Default.Raised(Arg<object>.Any(), Arg<EventArgs>.Any(), count);
            imposter.Count.HandlerInvoked(Arg<EventHandler>.Any(), count);
            imposter.HandlerInvoked.HandlerInvoked(Arg<EventHandler>.Any(), count);

            await imposter.RaiseAsync.RaiseAsync(sender, args);
        }
    }
}
"""
        );

        AssertNoDiagnostics(diagnostics);
    }
}
