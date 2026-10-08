using Imposter.Abstractions;
using Imposter.Tests.Features.ClassImpersonation;
using Shouldly;
using Xunit;

[assembly: GenerateImposter(typeof(ConstructorVirtualMethodTarget))]
[assembly: GenerateImposter(typeof(ConstructorAbstractMethodTarget))]
[assembly: GenerateImposter(typeof(ConstructorVirtualPropertyTarget))]

namespace Imposter.Tests.Features.ClassImpersonation
{
    public class ConstructorVirtualMethodTarget
    {
        public ConstructorVirtualMethodTarget(bool invokeMember)
        {
            if (invokeMember)
            {
                ConstructorValue = Get();
            }
        }

        public int ConstructorValue { get; }

        public virtual int Get() => 1;
    }

    public abstract class ConstructorAbstractMethodTarget
    {
        protected ConstructorAbstractMethodTarget(bool invokeMember)
        {
            if (invokeMember)
            {
                ConstructorValue = Get();
            }
        }

        public int ConstructorValue { get; }

        public abstract int Get();
    }

    public class ConstructorVirtualPropertyTarget
    {
        public ConstructorVirtualPropertyTarget(bool invokeMember)
        {
            if (invokeMember)
            {
                ConstructorValue = Value;
            }
        }

        public int ConstructorValue { get; }

        public virtual int Value => 1;
    }

    public class ConstructorVirtualDispatchTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Given_ConstructorCallingVirtualMethod_When_CreatingImposter_Should_AllowSetup(
            bool invokeMember
        )
        {
            var imposter = new ConstructorVirtualMethodTargetImposter(invokeMember);
            imposter.Instance().ConstructorValue.ShouldBe(invokeMember ? 1 : 0);
            imposter.Get().Called(Count.Never());

            imposter.Get().Returns(42);

            imposter.Instance().Get().ShouldBe(42);
            imposter.Get().Called(Count.Once());
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Given_ConstructorCallingAbstractMethod_When_CreatingImposter_Should_AllowSetup(
            bool invokeMember
        )
        {
            var imposter = new ConstructorAbstractMethodTargetImposter(invokeMember);
            imposter.Instance().ConstructorValue.ShouldBe(0);
            imposter.Get().Called(Count.Never());

            imposter.Get().Returns(42);

            imposter.Instance().Get().ShouldBe(42);
            imposter.Get().Called(Count.Once());
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Given_ConstructorReadingVirtualProperty_When_CreatingImposter_Should_AllowSetup(
            bool invokeMember
        )
        {
            var imposter = new ConstructorVirtualPropertyTargetImposter(invokeMember);
            imposter.Instance().ConstructorValue.ShouldBe(invokeMember ? 1 : 0);
            imposter.Value.Getter().Called(Count.Never());

            imposter.Value.Getter().Returns(42);

            imposter.Instance().Value.ShouldBe(42);
            imposter.Value.Getter().Called(Count.Once());
        }
    }
}
