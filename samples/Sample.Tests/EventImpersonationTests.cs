using Imposter.Abstractions;
using Sample.Services;
using Shouldly;
using Xunit;

namespace Sample.Tests;

public class EventImpersonationTests
{
    [Fact]
    public void EventImpersonation_WhenRaised_ShouldInvokeSubscribers()
    {
        var imposter = new IOrderNotifierImposter();
        var received = new List<OrderStatusChangedEventArgs>();

        imposter.Instance().OrderStatusChanged += (_, args) => received.Add(args);

        var args = new OrderStatusChangedEventArgs("ORD-222", "Shipped");
        imposter.OrderStatusChanged.Raise(this, args);

        received.ShouldHaveSingleItem();
        received[0].OrderId.ShouldBe("ORD-222");
        received[0].Status.ShouldBe("Shipped");
    }

    [Fact]
    public void EventImpersonation_WithMultipleHandlers_ShouldNotifyAll()
    {
        var imposter = new IOrderNotifierImposter();
        var handlerCount = 0;

        imposter.Instance().OrderStatusChanged += (_, _) => handlerCount++;
        imposter.Instance().OrderStatusChanged += (_, _) => handlerCount++;

        imposter.OrderStatusChanged.Raise(
            this,
            new OrderStatusChangedEventArgs("ORD-333", "Delivered")
        );

        handlerCount.ShouldBe(2);
    }
}
