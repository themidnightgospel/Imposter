#if USE_CSHARP14
using Imposter.Abstractions;
using Imposter.Tests.Shared;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.MethodImpersonation
{
    public class RefReadOnlyParameterTests
    {
        private readonly IRefReadOnlyParameterSutImposter _sut =
            new IRefReadOnlyParameterSutImposter();

        [Fact]
        public void GivenSetupForArgument_WhenInvoked_ShouldReturnSetupValue()
        {
            _sut.Read(Arg<int>.Is(4)).Returns(40);
            var key = 4;

            _sut.Instance().Read(in key).ShouldBe(40);
        }

        [Fact]
        public void GivenReturnsDelegate_WhenInvoked_ShouldPassTheArgumentToTheDelegate()
        {
            _sut.Read(Arg<int>.Any()).Returns((ref readonly int key) => key * 2);
            var key = 5;

            _sut.Instance().Read(in key).ShouldBe(10);
        }

        [Fact]
        public void GivenCallback_WhenInvoked_ShouldPassTheArgumentToTheCallback()
        {
            var received = 0;
            _sut.Read(Arg<int>.Any()).Callback((ref readonly int key) => received = key);
            var key = 7;

            _sut.Instance().Read(in key);

            received.ShouldBe(7);
        }

        [Fact]
        public void GivenInvocation_WhenVerifiedWithItsArgument_ShouldCountIt()
        {
            var key = 3;
            _sut.Instance().Read(in key);

            _sut.Read(Arg<int>.Is(3)).Called(Count.Once());
        }

        [Fact]
        public void GivenGenericSetupOnBaseType_WhenInvokedWithDerivedType_ShouldReturnSetupValue()
        {
            _sut.Accepts<IAnimal>(Arg<IAnimal>.Any()).Returns(true);
            var cat = new Cat("fluffy");

            _sut.Instance().Accepts(in cat).ShouldBeTrue();
        }

        [Fact]
        public void GivenClassMethodWithUseBaseImplementation_WhenInvoked_ShouldPassTheArgumentToTheBaseMethod()
        {
            var imposter = new RefReadOnlyParameterClassImposter();
            imposter.Read(Arg<int>.Any()).UseBaseImplementation();
            var key = 4;

            imposter.Instance().Read(in key).ShouldBe(40);
        }

        [Fact]
        public void GivenProtectedMethodSetup_WhenInvokedThroughThePublicMember_ShouldReturnSetupValue()
        {
            var imposter = new RefReadOnlyParameterClassImposter();
            imposter.ProtectedRead(Arg<int>.Is(4)).Returns(7);

            imposter.Instance().InvokeProtectedRead(4).ShouldBe(7);
        }

        [Fact]
        public void GivenConstructorWithRefReadOnlyParameter_WhenImposterIsCreated_ShouldPassTheArgumentToTheBaseConstructor()
        {
            var seed = 4;

            new RefReadOnlyConstructorClassImposter(in seed).Instance().Seed.ShouldBe(4);
        }
    }
}
#endif
