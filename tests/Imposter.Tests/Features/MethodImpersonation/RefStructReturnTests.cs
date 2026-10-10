using System;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.MethodImpersonation
{
    public class RefStructReturnTests
    {
        private readonly IRefStructReturnSutImposter _sut = new IRefStructReturnSutImposter();

        [Fact]
        public void GivenReturnsDelegate_WhenInvoked_ShouldReturnWhatItProduces()
        {
            _sut.Rent(Arg<int>.Any()).Returns(size => new Slot(size + 1));

            _sut.Instance().Rent(2).Index.ShouldBe(3);
        }

        [Fact]
        public void GivenNoSetup_WhenInvoked_ShouldReturnTheDefault()
        {
            _sut.Instance().Rent(2).Index.ShouldBe(0);
        }

        [Fact]
        public void GivenReturnsDelegatesInSequence_WhenInvokedTwice_ShouldReturnEachInOrder()
        {
            _sut.Rent(Arg<int>.Any()).Returns(_ => new Slot(1)).Then().Returns(_ => new Slot(2));
            _sut.Instance().Rent(0);

            _sut.Instance().Rent(0).Index.ShouldBe(2);
        }

        [Fact]
        public void GivenThrows_WhenInvoked_ShouldThrow()
        {
            _sut.Rent(Arg<int>.Any()).Throws<InvalidOperationException>();

            Should.Throw<InvalidOperationException>(() => _sut.Instance().Rent(1));
        }

        [Fact]
        public void GivenInvocations_WhenVerified_ShouldCountThem()
        {
            _sut.Instance().Rent(1);
            _sut.Instance().Rent(1);

            _sut.Rent(Arg<int>.Is(1)).Called(Count.Exactly(2));
        }

        [Fact]
        public void GivenReturnsDelegateTakingARefStruct_WhenInvoked_ShouldReturnWhatItProduces()
        {
            _sut.Next().Returns(current => new Slot(current.Index + 1));

            _sut.Instance().Next(new Slot(4)).Index.ShouldBe(5);
        }

        [Fact]
        public void GivenUseBaseImplementation_WhenClassMethodIsInvoked_ShouldReturnTheBaseResult()
        {
            var imposter = new RefStructReturnClassImposter();
            imposter.Rent(Arg<int>.Any()).UseBaseImplementation();

            imposter.Instance().Rent(2).Index.ShouldBe(20);
        }
    }
}
