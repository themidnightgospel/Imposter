using System.Threading.Tasks;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Events;

public class EventCrossMemberStressCollisionPreventionTests : EventNamingCollisionPreventionTestsBase
{
    [Fact]
    public async Task Given_CrossMemberCollisionNames_ShouldCompile()
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
            var imposter = new ICrossMemberEventCollisionTargetImposter();
            var handler = new EventHandler((_, _) => { });
            var sender = new object();
            var args = EventArgs.Empty;
            var count = Count.AtLeast(1);

            var instance = ((IHaveImposterInstance<ICrossMemberEventCollisionTarget>)imposter).Instance();
            _ = instance;

            imposter.Instance.Subscribe(handler);
            imposter.IChaosEventImposterBuilder.Subscribe(handler);
            imposter.IChaosEventImposterSetupBuilder.Raise(sender, args);
            imposter.IChaosEventImposterVerificationBuilder.Raised(Arg<object>.Any(), Arg<EventArgs>.Any(), count);

            imposter.Raise.Raise(sender, args);
            imposter.Subscribe.Subscribe(handler);
            imposter._imposterInstance.Subscribe(handler);
            imposter._invocationBehavior.Subscribe(handler);
            imposter._handlerOrder.CountMatches(Arg<EventHandler>.Any());
            imposter.Default.Raised(Arg<object>.Any(), Arg<EventArgs>.Any(), count);
            imposter.Count.HandlerInvoked(Arg<EventHandler>.Any(), count);
            imposter.HandlerInvoked.HandlerInvoked(Arg<EventHandler>.Any(), count);

            var asyncBuilder = imposter.RaiseAsync;
            asyncBuilder.Raise(sender, args);
            await asyncBuilder.RaiseAsync(sender, args);
        }
    }
}
""");

        AssertNoDiagnostics(diagnostics);
    }
}
