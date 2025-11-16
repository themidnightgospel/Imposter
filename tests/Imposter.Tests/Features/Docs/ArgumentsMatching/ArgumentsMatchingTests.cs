using Imposter.Abstractions;
using Imposter.Tests.Features.Docs.ArgumentsMatching;
using Shouldly;
using Xunit;

[assembly: GenerateImposter(typeof(IArgumentMatchingService))]

namespace Imposter.Tests.Features.Docs.ArgumentsMatching
{
    public interface IArgumentMatchingService
    {
        int Add(int a, int b);
        int Increment(int value);
        int Sum(params int[] values);
        string Format(string template, string value);
        int InOnly(in int input);
        int RefOnly(ref int state);
        int OutOnly(out int result);
    }

    public class ArgumentsMatchingTests
    {
        [Fact]
        public void GivenArgumentMatchers_WhenUsingBasicEqualityAndImplicitMatchers_ShouldMatchConfiguredValues()
        {
            var imposter = new IArgumentMatchingServiceImposter();
            var service = imposter.Instance();

            // Implicit equality matchers.
            // Equivalent to imposter.Add(Arg<int>.Is(1), Arg<int>.Is(2)).Returns(3);
            imposter.Add(1, 2).Returns(3);

            // Explicit equality matchers
            imposter.Add(Arg<int>.Is(1), Arg<int>.Is(2)).Returns(30);

            // Wildcard on first argument, fixed second
            imposter.Add(Arg<int>.Any(), 0).Returns(0);

            service.Add(1, 2).ShouldBe(30);
            service.Add(10, 0).ShouldBe(0);
            service.Add(5, 5).ShouldBe(0);
        }

        [Fact]
        public void GivenArgumentMatchers_WhenUsingPredicatesAndRanges_ShouldPartitionInputsIntoBuckets()
        {
            var imposter = new IArgumentMatchingServiceImposter();
            var service = imposter.Instance();

            imposter.Increment(Arg<int>.Is(x => x < 0)).Returns(-1);
            imposter.Increment(Arg<int>.Is(x => x >= 0 && x <= 10)).Returns(10);
            imposter.Increment(Arg<int>.Is(x => x > 10)).Returns(100);

            service.Increment(-5).ShouldBe(-1);
            service.Increment(3).ShouldBe(10);
            service.Increment(50).ShouldBe(100);
        }

        [Fact]
        public void GivenArgumentMatchers_WhenUsingNegationAndDefaults_ShouldRespectNegationAndDefaults()
        {
            var imposter = new IArgumentMatchingServiceImposter();
            var service = imposter.Instance();

            // Not equal
            imposter.Increment(Arg<int>.IsNot(0)).Returns(1);

            // Default(T)
            imposter.Increment(Arg<int>.IsDefault()).Returns(-1);

            service.Increment(0).ShouldBe(-1);
            service.Increment(5).ShouldBe(1);
        }

        [Fact]
        public void GivenArgumentMatchers_WhenUsingMembershipAndCollections_ShouldMatchMembershipCorrectly()
        {
            var imposter = new IArgumentMatchingServiceImposter();
            var service = imposter.Instance();

            // Discrete set
            imposter.Increment(Arg<int>.IsIn(new[] { 1, 2, 3 })).Returns(10);
            imposter.Increment(Arg<int>.IsNotIn(new[] { 1, 2, 3 })).Returns(99);

            service.Increment(2).ShouldBe(10);
            service.Increment(5).ShouldBe(99);
        }

        [Fact]
        public void GivenArgumentMatchers_WhenUsingCollectionsParamsAndArrays_ShouldMatchOnArrayShape()
        {
            var imposter = new IArgumentMatchingServiceImposter();
            var service = imposter.Instance();

            // Params / array arguments
            imposter.Sum(Arg<int[]>.Is(arr => arr.Length == 0)).Returns(0);
            imposter.Sum(Arg<int[]>.Is(arr => arr.Length == 1)).Returns(1);
            imposter.Sum(Arg<int[]>.Is(arr => arr.Length > 1)).Returns(2);

            service.Sum().ShouldBe(0);
            service.Sum(10).ShouldBe(1);
            service.Sum(1, 2, 3).ShouldBe(2);
        }

        [Fact]
        public void GivenArgumentMatchers_WhenMatchingInParameters_ShouldRespectInParameterSemantics()
        {
            var imposter = new IArgumentMatchingServiceImposter();
            var service = imposter.Instance();

            imposter.InOnly(Arg<int>.Is(x => x > 0)).Returns(99);

            int input = 5;
            var result = service.InOnly(in input);

            result.ShouldBe(99);
        }

        [Fact]
        public void GivenArgumentMatchers_WhenMatchingRefParameters_ShouldAllowRefMutationAndMatch()
        {
            var imposter = new IArgumentMatchingServiceImposter();
            var service = imposter.Instance();

            imposter
                .RefOnly(Arg<int>.Is(x => x >= 0))
                .Returns(
                    (ref int state) =>
                    {
                        state += 10;
                        return state;
                    }
                );

            int state = 1;
            var total = service.RefOnly(ref state);

            total.ShouldBe(11);
            state.ShouldBe(11);
        }

        [Fact]
        public void GivenArgumentMatchers_WhenMatchingOutParameters_ShouldPopulateOutAndReturnStatus()
        {
            var imposter = new IArgumentMatchingServiceImposter();
            var service = imposter.Instance();

            imposter
                .OutOnly(OutArg<int>.Any())
                .Returns(
                    (out int value) =>
                    {
                        value = 42;
                        return 1;
                    }
                );

            int result;
            var status = service.OutOnly(out result);

            status.ShouldBe(1);
            result.ShouldBe(42);
        }
    }
}
