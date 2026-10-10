#if USE_CSHARP14
using System;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.MethodImpersonation
{
    public class AllowsRefStructTests
    {
        private readonly IAllowsRefStructSutImposter _sut = new IAllowsRefStructSutImposter();

        [Fact]
        public void GivenReturnsDelegate_WhenInvokedWithARefStruct_ShouldPassItTheValue()
        {
            _sut.Measure<Gauge>(Arg<int>.Any()).Returns((value, scale) => value.Level * scale);

            _sut.Instance().Measure(new Gauge(2), 3).ShouldBe(6);
        }

        [Fact]
        public void GivenSetupForTheOtherArguments_WhenInvoked_ShouldMatchWhateverTheValue()
        {
            _sut.Measure<Gauge>(Arg<int>.Is(1)).Returns(10);

            _sut.Instance().Measure(new Gauge(5), 1).ShouldBe(10);
            _sut.Instance().Measure(new Gauge(5), 2).ShouldBe(0);
        }

        [Fact]
        public void GivenSetupForATypeArgumentThatIsNotARefStruct_WhenInvoked_ShouldPassItTheValue()
        {
            _sut.Measure<int>(Arg<int>.Any()).Returns((value, scale) => value + scale);

            _sut.Instance().Measure(4, 1).ShouldBe(5);
        }

        // Another generic method's setup also applies to calls whose type arguments convert to its own. This one's
        // applies to its own type arguments only.
        [Fact]
        public void GivenSetupForOneTypeArgument_WhenInvokedWithAnother_ShouldNotApplyIt()
        {
            _sut.Measure<object>(Arg<int>.Any()).Returns(10);

            _sut.Instance().Measure<object>("value", 1).ShouldBe(10);
            _sut.Instance().Measure<string>("value", 1).ShouldBe(0);
        }

        [Fact]
        public void GivenCallback_WhenInvoked_ShouldPassItTheRefStruct()
        {
            var seen = 0;
            _sut.Measure<Gauge>(Arg<int>.Any()).Callback((value, scale) => seen = value.Level);

            _sut.Instance().Measure(new Gauge(4), 1);

            seen.ShouldBe(4);
        }

        [Fact]
        public void GivenThrowsDelegate_WhenInvoked_ShouldThrowWhatItCreatesFromTheValue()
        {
            _sut.Measure<Gauge>(Arg<int>.Any())
                .Throws((value, scale) => new InvalidOperationException(value.Level.ToString()));

            Should
                .Throw<InvalidOperationException>(() => _sut.Instance().Measure(new Gauge(3), 1))
                .Message.ShouldBe("3");
        }

        [Fact]
        public void GivenInvocations_WhenVerified_ShouldCountThemByTypeArgumentAndTheOtherArguments()
        {
            _sut.Instance().Measure(new Gauge(1), 1);
            _sut.Instance().Measure(new Gauge(2), 1);
            _sut.Instance().Measure(new Gauge(1), 2);
            _sut.Instance().Measure(1, 1);

            _sut.Measure<Gauge>(Arg<int>.Is(1)).Called(Count.Exactly(2));
            _sut.Measure<int>(Arg<int>.Any()).Called(Count.Once());
        }

        [Fact]
        public void GivenReturnsDelegate_WhenInvoked_ShouldReturnTheRefStructItCreates()
        {
            _sut.Echo<Gauge>().Returns(value => new Gauge(value.Level + 1));

            _sut.Instance().Echo(new Gauge(1)).Level.ShouldBe(2);
        }

        [Fact]
        public void GivenSequenceOfReturnsDelegates_WhenInvoked_ShouldReturnTheirResultsInOrder()
        {
            _sut.Create<Gauge>().Returns(() => new Gauge(1)).Then().Returns(() => new Gauge(2));

            _sut.Instance().Create<Gauge>().Level.ShouldBe(1);
            _sut.Instance().Create<Gauge>().Level.ShouldBe(2);
        }

        [Fact]
        public void GivenNoSetup_WhenInvoked_ShouldReturnTheDefault()
        {
            _sut.Instance().Create<Gauge>().Level.ShouldBe(0);
        }

        [Fact]
        public void GivenCallbackThatChangesTheValue_WhenInvoked_ShouldChangeTheCallersValue()
        {
            _sut.Reset<Gauge>().Callback((ref Gauge value) => value.Level = 0);
            var gauge = new Gauge(5);

            _sut.Instance().Reset(ref gauge);

            gauge.Level.ShouldBe(0);
        }

        [Fact]
        public void GivenExplicitMode_WhenInvokedWithoutSetup_ShouldThrowMissingImposterException()
        {
            var sut = new IAllowsRefStructSutImposter(ImposterMode.Explicit);

            Should.Throw<MissingImposterException>(() => sut.Instance().Measure(new Gauge(1), 1));
        }

        [Fact]
        public void GivenBaseImplementation_WhenInvoked_ShouldCallTheBaseWithTheValue()
        {
            var sut = new AllowsRefStructClassImposter();
            sut.Measure<Gauge>(Arg<int>.Any()).UseBaseImplementation();
            sut.Echo<Gauge>().UseBaseImplementation();

            sut.Instance().Measure(new Gauge(1), 2).ShouldBe(20);
            sut.Instance().Echo(new Gauge(6)).Level.ShouldBe(6);
        }
    }
}
#endif
