using System.Collections.Generic;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

[assembly: GenerateImposter(typeof(Imposter.Tests.Docs.InvocationHistory.IHistoryExample))]

namespace Imposter.Tests.Docs.InvocationHistory
{
    public interface IHistoryExample
    {
        string Hello(string name, int times);
    }

    public class InvocationHistoryTests
    {
        [Fact]
        public void GivenCompletedCalls_WhenReadingSnapshots_ShouldExposeTypedAndCombinedHistory()
        {
#if USE_CSHARP14
            var imposter = IHistoryExample.Imposter();
#else
            var imposter = new IHistoryExampleImposter();
#endif
            var instance = imposter.Instance();
            instance.Hello("Ann", 1);
            instance.Hello("Bob", 2);

            var calls = imposter.Hello(Arg<string>.Any(), Arg<int>.Any()).Invocations;
            calls[0].Name.ShouldBe("Ann");
            calls[0].Times.ShouldBe(1);
            calls[0].Sequence.ShouldBeLessThan(calls[1].Sequence);

            IReadOnlyList<IImposterInvocation> history = imposter.Invocations();
            history.Count.ShouldBe(2);
            history[0].Member.ShouldBe("Hello");
            history[0].ShouldBeSameAs(calls[0]);
        }
    }
}
