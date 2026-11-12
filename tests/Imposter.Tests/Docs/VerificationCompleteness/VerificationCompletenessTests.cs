using Imposter.Abstractions;
using Xunit;

[assembly: GenerateImposter(
    typeof(Imposter.Tests.Docs.VerificationCompleteness.IRepositoryExample)
)]

namespace Imposter.Tests.Docs.VerificationCompleteness
{
    public interface IRepositoryExample
    {
        void Save(int id);
        string Load(int id);
    }

    public class VerificationCompletenessTests
    {
        [Fact]
        public void GivenUsedAndOptionalSetups_WhenVerifying_ShouldCoverCallsAndIgnoreLenientSetup()
        {
#if USE_CSHARP14
            var imposter = IRepositoryExample.Imposter();
#else
            var imposter = new IRepositoryExampleImposter();
#endif
            imposter.Save(42).Callback(_ => { });
            imposter.Load(42).Returns("optional").Then().Returns("later").Lenient();
            imposter.Instance().Save(42);
            imposter.Save(42).Called(Count.Once());
            imposter.VerifyNoOtherCalls();
            imposter.VerifyAllSetupsUsed();
        }

        [Fact]
        public void GivenSeveralImposters_WhenVerifyingCounts_ShouldCoverCallsAcrossInstances()
        {
            var repository = new IRepositoryExampleImposter();
            var audit = new IRepositoryExampleImposter();
            Imposters.VerifyNoInteractions(repository, audit);
            repository.Instance().Save(42);
            audit.Instance().Save(42);
            repository.Save(42).Called(Count.Once());
            audit.Save(42).Called(Count.Once());
            Imposters.VerifyNoOtherCalls(repository, audit);
            Imposters.VerifyAllSetupsUsed(repository, audit);
        }
    }
}
