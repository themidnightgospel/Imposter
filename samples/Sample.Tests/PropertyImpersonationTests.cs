using Imposter.Abstractions;
using Sample.Services;
using Shouldly;
using Xunit;

namespace Sample.Tests;

public class PropertyImpersonationTests
{
    [Fact]
    public void PropertyImpersonation_WhenGetterConfigured_ShouldReturnConfiguredValues()
    {
        var imposter = new ICustomerSessionImposter();

        imposter.Status.Getter().Returns(CustomerStatus.Authenticated);
        imposter.CurrentCustomerId.Getter().Returns("customer-123");

        var session = imposter.Instance();
        session.Status.ShouldBe(CustomerStatus.Authenticated);
        session.CurrentCustomerId.ShouldBe("customer-123");
    }

    [Fact]
    public void PropertyImpersonation_WhenUnset_ShouldBehaveLikeBackingProperty()
    {
        var imposter = new ICustomerSessionImposter();
        var session = imposter.Instance();

        session.CurrentCustomerId.ShouldBeNull();

        session.CurrentCustomerId = "customer-789";
        session.CurrentCustomerId.ShouldBe("customer-789");
    }
}
