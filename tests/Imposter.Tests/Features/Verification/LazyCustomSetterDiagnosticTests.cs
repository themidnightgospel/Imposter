using System;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

[assembly: GenerateImposter(typeof(Imposter.Tests.Features.Verification.ILazyDiagnosticSetter))]

namespace Imposter.Tests.Features.Verification
{
    public interface ILazyDiagnosticSetter
    {
        Span<DiagnosticElement> Buffer { set; }
    }

    public sealed class DiagnosticElement
    {
        public int FormatCalls { get; private set; }

        public override string ToString()
        {
            FormatCalls++;
            return "element";
        }
    }

    public class LazyCustomSetterDiagnosticTests
    {
        [Fact]
        public void GivenSpanEqualityMatcher_WhenCreatingAndVerifyingSetter_ShouldFormatOnlyOnFailure()
        {
            var value = new DiagnosticElement();
            var sut = new ILazyDiagnosticSetterImposter();
            var matcher = SpanArg<DiagnosticElement>.Is(value);
            var setter = sut.Buffer.Setter(matcher);
            value.FormatCalls.ShouldBe(0);
            setter.Callback(_ => { });
            sut.Instance().Buffer = new[] { value };
            setter.Called(Count.Once());
            value.FormatCalls.ShouldBe(0);
            var failure = Should.Throw<VerificationFailedException>(() =>
                setter.Called(Count.Twice())
            );
            value.FormatCalls.ShouldBeGreaterThan(0);
            failure.Expected.ShouldContain("[<element>]");
        }
    }
}
