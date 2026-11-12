using System;
using Imposter.Abstractions;
using Xunit;

[assembly: GenerateImposter(typeof(Imposter.Tests.Docs.OrderedVerification.IOrderedExample))]

namespace Imposter.Tests.Docs.OrderedVerification
{
    public interface IOrderedExample
    {
        void Save(int id);
        event EventHandler SomethingHappened;
    }

    public class OrderedVerificationTests
    {
        [Fact]
        public void GivenCallsAcrossInstances_WhenVerifyingOrder_ShouldCoverSelectedCalls()
        {
#if USE_CSHARP14
            var repository = IOrderedExample.Imposter();
            var audit = IOrderedExample.Imposter();
#else
            var repository = new IOrderedExampleImposter();
            var audit = new IOrderedExampleImposter();
#endif
            repository.Instance().Save(42);
            audit.Instance().Save(42);
            Imposters.InOrder().Then(repository.Save(42)).Then(audit.Save(42)).Verify();
            Imposters.VerifyNoOtherCalls(repository, audit);
        }

        [Fact]
        public void GivenEventHandlerSteps_WhenVerifyingOrder_ShouldCoverSubscriptionAndUnsubscription()
        {
            var imposter = new IOrderedExampleImposter();
            var service = imposter.Instance();
            EventHandler h = (sender, args) => { };
            var subscribed = imposter.SomethingHappened.Subscribed(Arg<EventHandler>.Is(h));
            var unsubscribed = imposter.SomethingHappened.Unsubscribed(Arg<EventHandler>.Is(h));
            service.SomethingHappened += h;
            service.SomethingHappened -= h;
            Imposters.InOrder().Then(subscribed).Then(unsubscribed).Verify();
            imposter.VerifyNoOtherCalls();
        }
    }
}
