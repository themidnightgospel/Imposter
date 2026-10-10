using System;
using System.Collections.Generic;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.IndexerImpersonation
{
    public class RefStructIndexerTests
    {
        private readonly IRefStructIndexerSutImposter _sut = new IRefStructIndexerSutImposter();

        [Fact]
        public void GivenReturnsDelegate_WhenRead_ShouldReturnWhatItProduces()
        {
            _sut[Arg<int>.Any()].Getter().Returns(index => new Marker(index + 1));

            _sut.Instance()[2].Position.ShouldBe(3);
        }

        [Fact]
        public void GivenNoSetup_WhenRead_ShouldReturnTheDefault()
        {
            _sut.Instance()[2].Position.ShouldBe(0);
        }

        // The imposter can't keep the value, so a set doesn't change what the getter returns.
        [Fact]
        public void GivenValueSetWithoutSetup_WhenRead_ShouldReturnTheDefault()
        {
            _sut.Instance()[1] = new Marker(5);

            _sut.Instance()[1].Position.ShouldBe(0);
        }

        [Fact]
        public void GivenSetupsForDifferentKeys_WhenRead_ShouldReturnEachKeysValue()
        {
            _sut[Arg<int>.Is(1)].Getter().Returns(_ => new Marker(10));
            _sut[Arg<int>.Is(2)].Getter().Returns(_ => new Marker(20));

            _sut.Instance()[2].Position.ShouldBe(20);
        }

        [Fact]
        public void GivenReturnsDelegatesInSequence_WhenReadTwice_ShouldReturnEachInOrder()
        {
            _sut[Arg<int>.Any()]
                .Getter()
                .Returns(_ => new Marker(1))
                .Then()
                .Returns(_ => new Marker(2));
            _ = _sut.Instance()[0].Position;

            _sut.Instance()[0].Position.ShouldBe(2);
        }

        [Fact]
        public void GivenThrows_WhenRead_ShouldThrow()
        {
            _sut[Arg<int>.Any()].Getter().Throws<InvalidOperationException>();

            Should.Throw<InvalidOperationException>(() => _sut.Instance()[0].Position);
        }

        [Fact]
        public void GivenReads_WhenGetterIsVerified_ShouldCountThemByKey()
        {
            _ = _sut.Instance()[1].Position;
            _ = _sut.Instance()[1].Position;
            _ = _sut.Instance()[2].Position;

            _sut[Arg<int>.Is(1)].Getter().Called(Count.Exactly(2));
        }

        [Fact]
        public void GivenSetterCallback_WhenSet_ShouldGetTheKeyAndTheValue()
        {
            var sets = new List<(int, int)>();
            _sut[Arg<int>.Any()]
                .Setter()
                .Callback((index, value) => sets.Add((index, value.Position)));

            _sut.Instance()[3] = new Marker(4);

            sets.ShouldBe(new[] { (3, 4) });
        }

        [Fact]
        public void GivenSets_WhenSetterIsVerified_ShouldCountThemByKey()
        {
            _sut.Instance()[1] = new Marker(1);
            _sut.Instance()[2] = new Marker(2);

            _sut[Arg<int>.Is(1)].Setter().Called(Count.Once());
        }

        [Fact]
        public void GivenNoSet_WhenSetterIsVerifiedOnce_ShouldThrow()
        {
            Should.Throw<VerificationFailedException>(() =>
                _sut[Arg<int>.Any()].Setter().Called(Count.Once())
            );
        }

        [Fact]
        public void GivenExplicitModeWithoutSetup_WhenRead_ShouldThrow()
        {
            var imposter = new IRefStructIndexerSutImposter(ImposterMode.Explicit);

            Should.Throw<MissingImposterException>(() => imposter.Instance()[0].Position);
        }

        [Fact]
        public void GivenNoSetup_WhenClassIndexerIsRead_ShouldReturnTheBaseValue()
        {
            var imposter = new RefStructIndexerClassImposter();

            imposter.Instance()[2].Position.ShouldBe(20);
        }

        [Fact]
        public void GivenGetterUseBaseImplementation_WhenClassIndexerIsRead_ShouldReturnTheBaseValue()
        {
            var imposter = new RefStructIndexerClassImposter();
            imposter[Arg<int>.Any()].Getter().UseBaseImplementation();

            imposter.Instance()[3].Position.ShouldBe(30);
        }

        [Fact]
        public void GivenSetterUseBaseImplementation_WhenClassIndexerIsSet_ShouldSetTheBaseValue()
        {
            var imposter = new RefStructIndexerClassImposter();
            imposter[Arg<int>.Any()].Setter().UseBaseImplementation();

            imposter.Instance()[0] = new Marker(9);

            imposter.Instance().LastSet.ShouldBe(9);
        }

        [Fact]
        public void GivenNoSetup_WhenClassIndexerIsSet_ShouldNotSetTheBaseValue()
        {
            var imposter = new RefStructIndexerClassImposter();

            imposter.Instance()[0] = new Marker(9);

            imposter.Instance().LastSet.ShouldBe(0);
        }
    }
}
