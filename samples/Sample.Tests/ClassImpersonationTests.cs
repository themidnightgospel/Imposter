using Imposter.Abstractions;
using Sample.Services;
using Shouldly;
using Xunit;

namespace Sample.Tests;

public class ClassImpersonationTests
{
    [Fact]
    public void ClassImpersonation_WithUseBaseImplementation_ShouldCallBaseLogic()
    {
        var imposter = new PaymentGatewayImposter();
        imposter.Charge(Arg<string>.Any(), Arg<decimal>.Any()).UseBaseImplementation();

        var result = imposter.Instance().Charge("ORD-900", 55m);

        result.Success.ShouldBeTrue();
        result.Message.ShouldContain("ORD-900");
    }

    [Fact]
    public void ClassImpersonation_WithSequencedBaseAndOverride_ShouldSwitchBehaviors()
    {
        var imposter = new PaymentGatewayImposter();

        imposter
            .Charge(Arg<string>.Is(id => id == "ORD-DECLINE"), Arg<decimal>.Any())
            .UseBaseImplementation()
            .Then()
            .Returns(new PaymentResult(false, "Declined by fraud rules"));

        var gateway = imposter.Instance();

        gateway
            .Charge("ORD-DECLINE", 40m)
            .ShouldBe(new PaymentResult(true, "Authorized ORD-DECLINE for 40.00"));
        gateway
            .Charge("ORD-DECLINE", 40m)
            .ShouldBe(new PaymentResult(false, "Declined by fraud rules"));
    }
}
