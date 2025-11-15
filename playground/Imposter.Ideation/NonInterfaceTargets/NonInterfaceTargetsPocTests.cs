using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Ideation.NonInterfaceTargets
{
    public class NonInterfaceTargetsPocTests
    {
        [Fact]
        public void Reserve_method_can_be_configured_with_argument_matchers()
        {
            var imposter = new WarehouseAllocationImposter();
            var builder = imposter
                .Reserve(Arg<string>.Is("sku-1"), Arg<int>.Any())
                .Returns((sku, quantity) => quantity * 2);

            var sut = imposter.Instance();

            sut.Reserve("sku-1", 3).ShouldBe(6);
            builder.Called(Count.Once());
        }

        [Fact]
        public void Missing_setups_follow_invocation_behavior()
        {
            var strict = new WarehouseAllocationImposter(ImposterMode.Explicit);
            Should.Throw<MissingImposterException>(() => strict.Instance().Reserve("missing", 1));

            var loose = new WarehouseAllocationImposter(ImposterMode.Implicit);
            loose.Instance().Reserve("missing", 1).ShouldBe(default);
        }

        [Fact]
        public void Protected_virtual_members_can_be_overridden()
        {
            var imposter = new WarehouseAllocationImposter();
            var builder = imposter
                .FormatAudit(Arg<string>.Any(), Arg<int>.Any())
                .Returns((sku, quantity) => $"audit::{sku}:{quantity}");

            var sut = imposter.Instance();

            sut.EmitAuditLine("sku-9", 2).ShouldBe("audit::sku-9:2");
            builder.Called(Count.Once());
        }

        [Fact]
        public void Region_property_can_be_overridden()
        {
            var imposter = new WarehouseAllocationImposter();
            imposter.RegionProperty().Returns("eu-west-1");

            imposter.Region.ShouldBe("eu-west-1");
            imposter.Instance().Region.ShouldBe("eu-west-1");
        }

        [Fact]
        public void Active_reservations_property_can_act_like_auto_property()
        {
            var imposter = new WarehouseAllocationImposter();
            imposter.ActiveReservationsProperty().ActsLikeProperty(10);

            var sut = imposter.Instance();
            sut.ActiveReservations.ShouldBe(10);

            sut.ActiveReservations = 42;
            sut.ActiveReservations.ShouldBe(42);
        }

        [Fact]
        public void Active_reservations_setter_callback_is_invoked()
        {
            var imposter = new WarehouseAllocationImposter();
            var observed = 0;

            imposter
                .ActiveReservationsProperty()
                .ActsLikeProperty()
                .SetterCallback(value => observed = value);

            var sut = imposter.Instance();
            sut.ActiveReservations = 7;

            observed.ShouldBe(7);
        }
    }
}
