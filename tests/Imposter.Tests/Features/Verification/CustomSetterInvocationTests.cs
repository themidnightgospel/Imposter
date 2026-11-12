using System;
using Imposter.Abstractions;
using Imposter.Tests.Features.PropertyImpersonation;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.Verification
{
    public class CustomSetterInvocationTests
    {
        [Fact]
        public void GivenSpanSetter_WhenRecorded_ShouldCaptureReadOnlyValueAndSupportCoverageAndReset()
        {
            var sut = new ISpanPropertySutImposter();
            var instance = sut.Instance();
            var source = new byte[] { 1, 2 };
            var setup = sut.Buffer.Setter(SpanArg<byte>.Any());
            setup.Callback(value => value[0] = 7);
            instance.Buffer = source;
            source[0] = 9;
            instance.Buffer[0] = 8;
            sut.Buffer.Getter().Called(Count.Once());

            var snapshot = setup.Invocations;
            snapshot.Count.ShouldBe(1);
            snapshot[0].Value.Span[0].ShouldBe((byte)1);
            sut.VerifyAllSetupsUsed();
            setup.Called(Count.Once());
            sut.VerifyNoOtherCalls();

            sut.ClearInvocations();
            setup.Called(Count.Never());
            sut.VerifyAllSetupsUsed();
            sut.Reset();
            sut.Instance().ShouldBeSameAs(instance);
            setup.Callback(_ => throw new InvalidOperationException("detached"));
            instance.Buffer = new byte[] { 3 };
            snapshot[0].Value.Span[0].ShouldBe((byte)1);
            setup.Invocations.Count.ShouldBe(0);
        }

        [Fact]
        public void GivenRefStructSetter_WhenCallbackThrows_ShouldRecordExceptionAndCoverOnVerification()
        {
            var sut = new IRefStructPropertySutImposter();
            var failure = new InvalidOperationException("callback failed");
            var setup = sut.Current.Setter();
            setup.Callback(_ => throw failure);

            Should
                .Throw<InvalidOperationException>(() => sut.Instance().Current = new Bookmark(5))
                .ShouldBeSameAs(failure);

            setup.Invocations.Count.ShouldBe(1);
            setup.Invocations[0].Exception.ShouldBeSameAs(failure);
            sut.Invocations().Count.ShouldBe(1);
            sut.VerifyAllSetupsUsed();
            Should.Throw<VerificationFailedException>(() => setup.Called(Count.Never()));
            Should.Throw<VerificationFailedException>(() => sut.VerifyNoOtherCalls());
            setup.Called(Count.Once());
            sut.VerifyNoOtherCalls();
            sut.ClearInvocations();
            setup.Called(Count.Never());
            sut.VerifyAllSetupsUsed();
        }

        [Fact]
        public void GivenRefStructSetter_WhenReset_ShouldDetachBuildersAndRestoreExplicitMode()
        {
            var sut = new IRefStructPropertySutImposter(ImposterMode.Explicit);
            var instance = sut.Instance();
            var setup = sut.Current.Setter();
            setup.Callback(_ => { }).Lenient();
            instance.Current = new Bookmark(1);
            var snapshot = setup.Invocations;

            sut.Reset();

            sut.Instance().ShouldBeSameAs(instance);
            setup.Invocations.Count.ShouldBe(0);
            snapshot.Count.ShouldBe(1);
            setup.Callback(_ => { });
            Should.Throw<MissingImposterException>(() => instance.Current = new Bookmark(2));
            sut.Current.Setter().Invocations.Count.ShouldBe(1);
            sut.Current.Setter().Called(Count.Once());
            sut.VerifyNoOtherCalls();
            sut.VerifyAllSetupsUsed();
        }

        [Fact]
        public void GivenRefStructCallbackChains_WhenFirstThrows_ShouldLeaveLaterChainUnused()
        {
            var sut = new IRefStructPropertySutImposter();
            sut.Current.Setter().Callback(_ => throw new InvalidOperationException());
            var skipped = sut.Current.Setter();
            skipped.Callback(_ => { });
            Should.Throw<InvalidOperationException>(() => sut.Instance().Current = new Bookmark(3));
            Should.Throw<VerificationFailedException>(() => sut.VerifyAllSetupsUsed());
            skipped.Callback(_ => { }).Lenient();
            sut.VerifyAllSetupsUsed();
        }
    }
}
