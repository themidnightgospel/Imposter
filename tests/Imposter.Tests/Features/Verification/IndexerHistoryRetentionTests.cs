using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

[assembly: GenerateImposter(typeof(Imposter.Tests.Features.Verification.IndexerRetentionTarget))]
[assembly: GenerateImposter(typeof(Imposter.Tests.Features.Verification.IOptionalIndexerKeyTarget))]

namespace Imposter.Tests.Features.Verification
{
#pragma warning disable CS1066 // Deliberately exercises a target declaring an ineffective indexer default.
    public interface IOptionalIndexerKeyTarget
    {
        int this[int key = 0] { get; set; }
    }
#pragma warning restore CS1066

    public class IndexerRetentionTarget
    {
        public virtual object this[int key]
        {
            get => new object();
            set { }
        }
    }

    public class IndexerHistoryRetentionTests
    {
        private readonly IndexerRetentionTargetImposter _sut = new();

        [Fact]
        public void GivenRepeatedIndexerKeys_WhenComparingInvocations_ShouldKeepDistinctCallIdentities()
        {
            _sut.Instance()[1] = new object();
            _sut.Instance()[1] = new object();

            var calls = _sut[1].Setter().Invocations;
            calls.Count.ShouldBe(2);
            calls.Select(call => call.Key).ShouldBe(new[] { 1, 1 });
            calls.Select(call => call.Sequence).Distinct().Count().ShouldBe(2);
            calls[0].Sequence.ShouldBeLessThan(calls[1].Sequence);
        }

        [Fact]
        public void GivenOptionalIndexerKey_WhenReadingAndWriting_ShouldRetainValuesAndHistory()
        {
            var sut = new IOptionalIndexerKeyTargetImposter();
            sut.Instance()[0] = 42;
            sut.Instance()[0].ShouldBe(42);
            sut[0].Setter().Invocations[0].Value.ShouldBe(42);
            sut[0].Getter().Invocations[0].Result.ShouldBe(42);
            sut[0].Setter().Called(Count.Once());
            sut[0].Getter().Called(Count.Once());
            sut.VerifyNoOtherCalls();
            sut[0].Getter().Returns(17);
            sut.Instance()[0].ShouldBe(17);
            sut.VerifyAllSetupsUsed();
        }

        [Fact]
        public void GivenStoredIndexerValues_WhenHistoryIsCleared_ShouldReleaseUnrelatedResults()
        {
            var stored = new object();
            var previousResult = RecordAndClear(_sut, stored);
            // Force collection so this test can distinguish unreachable values from retained history.
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            previousResult.IsAlive.ShouldBeFalse();
            _sut.Instance()[1].ShouldBeSameAs(stored);
            GC.KeepAlive(_sut);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static WeakReference RecordAndClear(
            IndexerRetentionTargetImposter sut,
            object stored
        )
        {
            var result = sut.Instance()[0];
            sut.Instance()[1] = stored;
            sut.ClearInvocations();
            return new WeakReference(result);
        }
    }
}
