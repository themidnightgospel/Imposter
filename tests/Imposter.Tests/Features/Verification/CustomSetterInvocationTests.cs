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
        public void GivenSpanSetter_WhenRecorded_ShouldKeepReadOnlyEntrySnapshot()
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
        }

        [Fact]
        public void GivenRefStructSetter_WhenCallbackThrows_ShouldRecordException()
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
            Should.Throw<VerificationFailedException>(() => setup.Called(Count.Never()));
        }
    }
}
