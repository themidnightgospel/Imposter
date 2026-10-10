using System;
using System.Collections.Generic;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.IndexerImpersonation
{
    public class RefStructIndexerKeyTests
    {
        private readonly IRefStructIndexerKeySutImposter _sut =
            new IRefStructIndexerKeySutImposter();

        [Fact]
        public void GivenReturnsDelegate_WhenRead_ShouldGetTheRefStructKey()
        {
            _sut[Arg<int>.Any()].Getter().Returns((row, locator) => row + locator.Offset);

            _sut.Instance()[2, new Locator(3)].ShouldBe(5);
        }

        [Fact]
        public void GivenSetupsForDifferentKeys_WhenRead_ShouldMatchTheOtherKeysOnly()
        {
            _sut[Arg<int>.Is(1)].Getter().Returns(10);
            _sut[Arg<int>.Is(2)].Getter().Returns(20);

            _sut.Instance()[2, new Locator(7)].ShouldBe(20);
        }

        // The default behaviour keeps a value set by the other keys, whatever the ref struct key.
        [Fact]
        public void GivenValueSetWithoutSetup_WhenReadWithAnotherRefStructKey_ShouldReturnIt()
        {
            _sut.Instance()[1, new Locator(1)] = 5;

            _sut.Instance()[1, new Locator(2)].ShouldBe(5);
        }

        [Fact]
        public void GivenThrowsDelegate_WhenRead_ShouldThrowWhatItProduces()
        {
            _sut[Arg<int>.Any()]
                .Getter()
                .Throws((row, locator) => new InvalidOperationException(locator.Offset.ToString()));

            Should
                .Throw<InvalidOperationException>(() => _sut.Instance()[0, new Locator(4)])
                .Message.ShouldBe("4");
        }

        [Fact]
        public void GivenGetterCallback_WhenRead_ShouldGetTheRefStructKey()
        {
            var offsets = new List<int>();
            _sut[Arg<int>.Any()].Getter().Callback((row, locator) => offsets.Add(locator.Offset));

            _ = _sut.Instance()[0, new Locator(6)];

            offsets.ShouldBe(new[] { 6 });
        }

        [Fact]
        public void GivenSetterCallback_WhenSet_ShouldGetTheKeysAndTheValue()
        {
            var sets = new List<(int, int, int)>();
            _sut[Arg<int>.Any()]
                .Setter()
                .Callback((row, locator, value) => sets.Add((row, locator.Offset, value)));

            _sut.Instance()[1, new Locator(2)] = 3;

            sets.ShouldBe(new[] { (1, 2, 3) });
        }

        [Fact]
        public void GivenReads_WhenGetterIsVerified_ShouldCountThemByTheOtherKeys()
        {
            _ = _sut.Instance()[1, new Locator(1)];
            _ = _sut.Instance()[1, new Locator(2)];
            _ = _sut.Instance()[2, new Locator(1)];

            _sut[Arg<int>.Is(1)].Getter().Called(Count.Exactly(2));
        }

        [Fact]
        public void GivenOnlyRefStructKeys_WhenSetUpByTheMethod_ShouldReturnWhatTheDelegateProduces()
        {
            _sut.Indexer_1().Getter().Returns(locator => locator.Offset.ToString());

            _sut.Instance()[new Locator(8)].ShouldBe("8");
        }

        [Fact]
        public void GivenOnlyRefStructKeys_WhenSet_ShouldCountEverySet()
        {
            _sut.Instance()[new Locator(1)] = "a";
            _sut.Instance()[new Locator(2)] = "b";

            _sut.Indexer_1().Setter().Called(Count.Exactly(2));
        }

        [Fact]
        public void GivenNoSetup_WhenClassIndexerIsRead_ShouldReturnTheBaseValue()
        {
            var imposter = new RefStructIndexerKeyClassImposter();

            imposter.Instance()[2, new Locator(3)].ShouldBe(23);
        }

        [Fact]
        public void GivenGetterUseBaseImplementation_WhenClassIndexerIsRead_ShouldPassTheRefStructKey()
        {
            var imposter = new RefStructIndexerKeyClassImposter();
            imposter[Arg<int>.Any()].Getter().UseBaseImplementation();

            imposter.Instance()[1, new Locator(4)].ShouldBe(14);
        }

        [Fact]
        public void GivenSetterUseBaseImplementation_WhenClassIndexerIsSet_ShouldPassTheRefStructKey()
        {
            var imposter = new RefStructIndexerKeyClassImposter();
            imposter[Arg<int>.Any()].Setter().UseBaseImplementation();

            imposter.Instance()[1, new Locator(4)] = 5;

            imposter.Instance().LastSet.ShouldBe(9);
        }
    }
}
