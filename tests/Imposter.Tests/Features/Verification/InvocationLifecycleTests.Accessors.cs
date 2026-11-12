using Imposter.Abstractions;
using Imposter.Tests.Features.PropertyImpersonation;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.Verification
{
    public partial class InvocationLifecycleTests
    {
        [Fact]
        public void GivenSequentialGetterAndIndexer_WhenHistoryCleared_ShouldContinueTheirOutcomePositions()
        {
            var getter = _sut.Value.Getter();
            var indexer = _sut[7].Getter();
            getter.Returns(1).Then().Returns(2).Then().Returns(3);
            indexer.Returns(10).Then().Returns(20).Then().Returns(30);
            var instance = _sut.Instance();
            instance.Value.ShouldBe(1);
            instance[7].ShouldBe(10);

            ((IImposter)_sut).ClearInvocations();

            getter.Called(Count.Never());
            indexer.Called(Count.Never());
            instance.Value.ShouldBe(2);
            instance[7].ShouldBe(20);
            getter.Called(Count.Once());
            indexer.Called(Count.Once());
            ((IImposter)_sut).ClearInvocations();
            instance.Value.ShouldBe(3);
            instance[7].ShouldBe(30);
        }

        [Fact]
        public void GivenSpanAccessors_WhenClearedAndReset_ShouldClearCountersAndHistoryAndPreserveOnlyClearedState()
        {
            var sut = new ISpanPropertySutImposter();
            var instance = sut.Instance();
            var getter = sut.Buffer.Getter();
            var setter = sut.Buffer.Setter(SpanArg<byte>.Any());
            var readonlyGetter = sut.Name.Getter();
            var callbacks = 0;
            setter.Callback(_ => callbacks++);
            readonlyGetter.Returns(new[] { 'a', 'b' });
            instance.Buffer = new byte[] { 1, 2 };
            instance.Buffer.ToArray().ShouldBe(new byte[] { 1, 2 });
            instance.Name.ToString().ShouldBe("ab");
            getter.Called(Count.Once());
            readonlyGetter.Called(Count.Once());
            setter.Called(Count.Once());
            var snapshot = setter.Invocations;

            sut.ClearInvocations();

            getter.Called(Count.Never());
            readonlyGetter.Called(Count.Never());
            setter.Called(Count.Never());
            setter.Invocations.ShouldBeEmpty();
            snapshot.ShouldHaveSingleItem().Value.ToArray().ShouldBe(new byte[] { 1, 2 });
            instance.Buffer.ToArray().ShouldBe(new byte[] { 1, 2 });
            instance.Name.ToString().ShouldBe("ab");
            instance.Buffer = new byte[] { 3, 4 };
            callbacks.ShouldBe(2);
            getter.Called(Count.Once());
            readonlyGetter.Called(Count.Once());
            setter.Called(Count.Once());

            sut.Reset();

            sut.Instance().ShouldBeSameAs(instance);
            sut.Buffer.Getter().Called(Count.Never());
            sut.Name.Getter().Called(Count.Never());
            sut.Buffer.Setter(SpanArg<byte>.Any()).Called(Count.Never());
            instance.Buffer.IsEmpty.ShouldBeTrue();
            instance.Name.IsEmpty.ShouldBeTrue();
            instance.Buffer = new byte[] { 5 };
            callbacks.ShouldBe(2);
            setter.Invocations.ShouldBeEmpty();
            sut.Buffer.Setter(SpanArg<byte>.Any()).Called(Count.Once());
        }

        [Fact]
        public void GivenRefStructAccessors_WhenClearedAndReset_ShouldClearCounterAndPassThroughHistory()
        {
            var sut = new IRefStructPropertySutImposter();
            var instance = sut.Instance();
            var getter = sut.Current.Getter();
            var setter = sut.Current.Setter();
            getter.Returns(() => new Bookmark(7));
            var pages = 0;
            setter.Callback(value => pages += value.Page);
            instance.Current = new Bookmark(2);
            instance.Current.Page.ShouldBe(7);
            getter.Called(Count.Once());
            setter.Called(Count.Once());
            var snapshot = setter.Invocations;

            sut.ClearInvocations();

            getter.Called(Count.Never());
            setter.Called(Count.Never());
            setter.Invocations.ShouldBeEmpty();
            snapshot.ShouldHaveSingleItem();
            instance.Current.Page.ShouldBe(7);
            instance.Current = new Bookmark(3);
            pages.ShouldBe(5);
            getter.Called(Count.Once());
            setter.Called(Count.Once());

            sut.Reset();

            sut.Current.Getter().Called(Count.Never());
            sut.Current.Setter().Called(Count.Never());
            instance.Current.Page.ShouldBe(0);
            instance.Current = new Bookmark(9);
            pages.ShouldBe(5);
            setter.Invocations.ShouldBeEmpty();
            sut.Current.Setter().Called(Count.Once());
        }

        [Fact]
        public void GivenClassBaseStateAndSetup_WhenCleared_ShouldKeepInstanceBaseStateAndSetupPosition()
        {
            var sut = new LifecycleClassTargetImposter(11);
            var instance = sut.Instance();
            var setter = sut.Value.Setter(Arg<int>.Any());
            setter.UseBaseImplementation();
            instance.Value = 23;
            var getter = sut.Value.Getter();
            getter.Returns(99).Then().UseBaseImplementation();
            instance.Value.ShouldBe(99);

            sut.ClearInvocations();

            sut.Instance().ShouldBeSameAs(instance);
            getter.Called(Count.Never());
            setter.Called(Count.Never());
            instance.Value.ShouldBe(23);
            instance.Value = 31;
            instance.Value.ShouldBe(31);
            getter.Called(Count.Exactly(2));
            setter.Called(Count.Once());
        }
    }
}
