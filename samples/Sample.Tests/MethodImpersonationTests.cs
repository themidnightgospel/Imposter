using Imposter.Abstractions;
using Sample.Services;
using Shouldly;
using Xunit;

namespace Sample.Tests;

public class MethodImpersonationTests
{
    [Fact]
    public void MethodImpersonation_WhenConfigured_ShouldReturnConfiguredTotal()
    {
        var imposter = new IPricingServiceImposter();
        var order = new Order("ORD-001", new OrderLine("SKU-TRAVEL-MUG", 2, 15.50m));

        imposter.CalculateOrderTotal(Arg<Order>.Any()).Returns(120.75m);

        var total = imposter.Instance().CalculateOrderTotal(order);

        total.ShouldBe(120.75m);
    }

    [Fact]
    public void MethodImpersonation_WithMatchers_ShouldRouteToSpecificSetup()
    {
        var imposter = new IPricingServiceImposter();

        imposter
            .ApplyDiscount(Arg<string>.Any(), Arg<decimal>.Any())
            .Returns((string _, decimal amount) => amount); // fallback
        imposter
            .ApplyDiscount("vip", Arg<decimal>.Any())
            .Returns((string _, decimal amount) => amount * 0.9m);

        imposter.Instance().ApplyDiscount("vip", 200m).ShouldBe(180m);
        imposter.Instance().ApplyDiscount("guest", 200m).ShouldBe(200m);
    }
}
