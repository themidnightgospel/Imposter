using Imposter.Abstractions;
using Imposter.Tests.Features.PropertyImpersonation;
using Shouldly;
using Xunit;

[assembly: GenerateImposter(typeof(ConstructorVirtualInitOnlyTarget))]
[assembly: GenerateImposter(typeof(ConstructorAbstractInitOnlyTarget))]

namespace Imposter.Tests.Features.PropertyImpersonation
{
    public class ConstructorVirtualInitOnlyTarget
    {
        public ConstructorVirtualInitOnlyTarget()
        {
            Value = 7;
            ConstructorValue = Value;
        }

        public int ConstructorValue { get; }
        public int BaseValue { get; private set; }
        public int BaseInitCount { get; private set; }

        public virtual int Value
        {
            get => BaseValue;
            init
            {
                BaseValue = value;
                BaseInitCount++;
            }
        }
    }

    public abstract class ConstructorAbstractInitOnlyTarget
    {
        protected ConstructorAbstractInitOnlyTarget()
        {
            Value = 7;
            ConstructorValue = Value;
        }

        public int ConstructorValue { get; }
        public abstract int Value { get; init; }
    }

    public class ConstructorInitOnlyPropertyTests
    {
        [Theory]
        [InlineData(ImposterMode.Implicit)]
        [InlineData(ImposterMode.Explicit)]
        public void Given_ConstructorInitializingVirtualProperty_When_CreatingImposter_Should_UseBaseWithoutRecording(
            ImposterMode mode
        )
        {
            var imposter = new ConstructorVirtualInitOnlyTargetImposter(mode);
            var instance = imposter.Instance();

            instance.ConstructorValue.ShouldBe(7);
            instance.BaseValue.ShouldBe(7);
            instance.BaseInitCount.ShouldBe(1);
            imposter.Value.Getter().Called(Count.Never());
            imposter.Value.Setter(Arg<int>.Any()).Called(Count.Never());

            var observed = 0;
            imposter.Value.UseBaseImplementation();
            imposter.Value.Setter(42).Callback(value => observed = value);
            InitOnlyAccessor.Invoke(instance, nameof(instance.Value), 42);

            observed.ShouldBe(42);
            instance.Value.ShouldBe(42);
            instance.BaseInitCount.ShouldBe(2);
            imposter.Value.Setter(42).Called(Count.Once());
        }

        [Theory]
        [InlineData(ImposterMode.Implicit)]
        [InlineData(ImposterMode.Explicit)]
        public void Given_ConstructorInitializingAbstractProperty_When_CreatingImposter_Should_UseDefaultsWithoutRecording(
            ImposterMode mode
        )
        {
            var imposter = new ConstructorAbstractInitOnlyTargetImposter(mode);
            var instance = imposter.Instance();

            instance.ConstructorValue.ShouldBe(0);
            imposter.Value.Getter().Called(Count.Never());
            imposter.Value.Setter(Arg<int>.Any()).Called(Count.Never());

            var observed = 0;
            imposter.Value.Setter(42).Callback(value => observed = value);
            imposter.Value.Getter().Returns(42);
            InitOnlyAccessor.Invoke(instance, nameof(instance.Value), 42);

            observed.ShouldBe(42);
            instance.Value.ShouldBe(42);
            imposter.Value.Setter(42).Called(Count.Once());
        }
    }
}
