using System;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.IndexerImpersonation
{
    public class IndexerParameterCollisionTests
    {
        private readonly ClassWithCollidingIndexerParametersImposter _sut =
            new ClassWithCollidingIndexerParametersImposter();

        [Fact]
        public void Given_CollidingParameters_When_GetterConfigured_Should_PreserveArgumentsAndVerification()
        {
            var observed = Array.Empty<int>();
            var getter = _sut[1, 2, 3, 4, 5, 6, 7].Getter();
            getter.Callback((a, b, c, d, e, f, g) => observed = new[] { a, b, c, d, e, f, g });
            getter.Returns((a, b, c, d, e, f, g) => a + b + c + d + e + f + g);

            _sut.Instance()[1, 2, 3, 4, 5, 6, 7].ShouldBe(28);

            observed.ShouldBe(new[] { 1, 2, 3, 4, 5, 6, 7 });
            getter.Called(Count.Once());
        }

        [Fact]
        public void Given_CollidingParameters_When_SetterConfigured_Should_OnlyInvokeMatchingCallbacks()
        {
            var observed = Array.Empty<int>();
            var callbackCount = 0;
            var setter = _sut[1, 2, 3, 4, 5, 6, 7].Setter();
            setter.Callback(
                (a, b, c, d, e, f, g, value) =>
                {
                    callbackCount++;
                    observed = new[] { a, b, c, d, e, f, g, value };
                }
            );

            var instance = _sut.Instance();
            instance[1, 2, 3, 4, 5, 6, 7] = 42;
            instance[99, 2, 3, 4, 5, 6, 7] = 100;

            observed.ShouldBe(new[] { 1, 2, 3, 4, 5, 6, 7, 42 });
            callbackCount.ShouldBe(1);
            setter.Called(Count.Once());
        }

        [Fact]
        public void Given_CollidingParameters_When_UsingBaseImplementation_Should_ForwardMatchingCalls()
        {
            var getter = _sut[1, 2, 3, 4, 5, 6, 7].Getter();
            var setter = _sut[1, 2, 3, 4, 5, 6, 7].Setter();
            getter.UseBaseImplementation();
            setter.UseBaseImplementation();

            var instance = _sut.Instance();
            instance[1, 2, 3, 4, 5, 6, 7].ShouldBe(28);
            instance[1, 2, 3, 4, 5, 6, 7] = 42;
            instance[99, 2, 3, 4, 5, 6, 7] = 100;

            instance.LastSet.ShouldBe(new[] { 1, 2, 3, 4, 5, 6, 7, 42 });
            getter.Called(Count.Once());
            setter.Called(Count.Once());
        }
    }
}
