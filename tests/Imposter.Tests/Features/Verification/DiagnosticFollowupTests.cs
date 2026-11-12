using System;
using Imposter.Abstractions;
using Imposter.Tests.Features.PropertyImpersonation;
using Shouldly;
using Xunit;

[assembly: GenerateImposter(
    typeof(Imposter.Tests.Features.Verification.IGenericDiagnosticOwner<,>)
)]

namespace Imposter.Tests.Features.Verification
{
    public interface IGenericDiagnosticOwner<TValue, TUnused>
    {
        void Save(TValue value);
        void Delete(TValue value);
    }

    public class DiagnosticFollowupTests
    {
        [Fact]
        public void GivenGenericImposter_WhenReportingOtherInteractions_ShouldOmitClrArity()
        {
            var sut = new IGenericDiagnosticOwnerImposter<int, string>();
            sut.Instance().Delete(42);

            var failure = Should.Throw<VerificationFailedException>(() =>
                sut.Save(Arg<int>.Any()).Called(Count.Once())
            );

            failure.Message.ShouldBe(
                "Expected Save(value: any) exactly 1 time(s), performed 0 times."
                    + Environment.NewLine
                    + "Performed invocations of Save: none."
                    + Environment.NewLine
                    + "Other interactions on IGenericDiagnosticOwnerImposter:"
                    + Environment.NewLine
                    + "  Delete(value: <42>)"
            );
        }

        [Fact]
        public void GivenUnconfiguredGetters_WhenSharedOrCustomGetterFails_ShouldIdentifyGetAccess()
        {
            var shared = new IPropertySetupSutImposter(ImposterMode.Explicit);
            var custom = new IRefStructPropertySutImposter(ImposterMode.Explicit);

            var sharedFailure = Should.Throw<MissingImposterException>(() =>
            {
                _ = shared.Instance().Age;
            });
            var customFailure = Should.Throw<MissingImposterException>(() =>
            {
                _ = custom.Instance().Current;
            });

            sharedFailure.Message.ShouldStartWith(
                $"No setup matched get {typeof(IPropertySetupSut).FullName}.Age."
                    + Environment.NewLine
            );
            customFailure.Message.ShouldStartWith(
                $"No setup matched get {typeof(IRefStructPropertySut).FullName}.Current."
                    + Environment.NewLine
            );
        }
    }
}
