using Imposter.Abstractions;
using Imposter.Tests.Features.Docs.Methods.Verification;
using Shouldly;
using Xunit;

[assembly: GenerateImposter(typeof(IVerifyService))]

namespace Imposter.Tests.Features.Docs.Methods.Verification
{
    public interface IVerifyService
    {
        void Increment(int v);
        int Combine(int a, int b);
    }

    public class VerificationTests
    {
        [Fact]
        public void Methods_Verification_Basic_Counts()
        {
            var imposter = new IVerifyServiceImposter();
            var service = imposter.Instance();

            service.Increment(1);
            service.Increment(2);

            Should.NotThrow(() => imposter.Increment(Arg<int>.Any()).Called(Count.AtLeast(2)));
            Should.NotThrow(() => imposter.Increment(2).Called(Count.Once()));
        }

        [Fact]
        public void Methods_Verification_Matching_Arguments()
        {
            var imposter = new IVerifyServiceImposter();
            var service = imposter.Instance();

            // Exercise some calls
            for (int i = 0; i < 3; i++)
                service.Increment(11);
            service.Combine(2, 5);

            Should.NotThrow(() =>
                imposter.Increment(Arg<int>.Is(x => x > 10)).Called(Count.Exactly(3))
            );
            Should.NotThrow(() =>
                imposter
                    .Combine(Arg<int>.Is(x => x > 0), Arg<int>.Is(y => y < 10))
                    .Called(Count.Once())
            );
        }

        [Fact]
        public void Methods_Verification_Count_Basics()
        {
            var imposter = new IVerifyServiceImposter();
            var service = imposter.Instance();

            service.Increment(1);
            service.Increment(2);
            service.Increment(2);

            Should.NotThrow(() => imposter.Increment(Arg<int>.Any()).Called(Count.AtLeast(3)));
            Should.NotThrow(() => imposter.Increment(2).Called(Count.Exactly(2)));
            Should.NotThrow(() => imposter.Increment(1).Called(Count.Once()));
            Should.NotThrow(() => imposter.Increment(999).Called(Count.Never()));
            Should.NotThrow(() => imposter.Increment(Arg<int>.Any()).Called(Count.Any));
        }
    }
}
