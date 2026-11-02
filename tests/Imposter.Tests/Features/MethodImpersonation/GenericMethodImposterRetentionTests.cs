using System.Reflection;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.MethodImpersonation
{
    public class GenericMethodImposterRetentionTests
    {
        private const int InvocationCount = 100;

        private readonly IMethodSetupFeatureSutImposter _sut = new IMethodSetupFeatureSutImposter();

        [Fact]
        public void Given_NoSetup_When_GenericMethodIsInvokedRepeatedly_Should_NotRetainMethodImposters()
        {
            for (var i = 0; i < InvocationCount; i++)
            {
                _sut.Instance().GenericSingleParam(i);
            }

            StoredMethodImposterCount(_sut, "_genericSingleParamMethodImposterCollection")
                .ShouldBe(0);
        }

        [Fact]
        public void Given_SetupForOtherTypeArgument_When_GenericMethodIsInvokedRepeatedly_Should_RetainOnlyConfiguredImposter()
        {
            _sut.GenericReturnType<string>().Returns("configured");

            for (var i = 0; i < InvocationCount; i++)
            {
                _sut.Instance().GenericReturnType<int>();
            }

            StoredMethodImposterCount(_sut, "_genericReturnTypeMethodImposterCollection")
                .ShouldBe(1);
        }

        [Fact]
        public void Given_UnconfiguredInvocations_When_VerifyingCalls_Should_CountAllInvocations()
        {
            for (var i = 0; i < InvocationCount; i++)
            {
                _sut.Instance().GenericSingleParam(i);
            }

            _sut.GenericSingleParam(Arg<int>.Any()).Called(Count.Exactly(InvocationCount));
        }

        [Fact]
        public void Given_UnconfiguredInvocations_When_SetupIsAddedLater_Should_ApplySetup()
        {
            for (var i = 0; i < InvocationCount; i++)
            {
                _sut.Instance().GenericReturnType<int>();
            }

            _sut.GenericReturnType<int>().Returns(42);

            _sut.Instance().GenericReturnType<int>().ShouldBe(42);
        }

        [Fact]
        public void Given_ExplicitModeWithoutSetup_When_GenericMethodIsInvoked_Should_ThrowMissingImposterException()
        {
            var sut = new IMethodSetupFeatureSutImposter(ImposterMode.Explicit);

            Should.Throw<MissingImposterException>(() => sut.Instance().GenericReturnType<int>());
        }

        [Fact]
        public void Given_RepeatedSetupsOfOneTypeArgument_When_Configuring_Should_ShareOneImposter()
        {
            for (var i = 0; i < 10; i++)
            {
                _sut.GenericSingleParam(Arg<int>.Is(i));
            }

            StoredMethodImposterCount(_sut, "_genericSingleParamMethodImposterCollection")
                .ShouldBe(1);
        }

        [Fact]
        public void Given_SetupsInterleavedAcrossTypeArguments_When_Invoking_Should_UseNewestMatchingSetup()
        {
            _sut.GenericReturnType<int>().Returns(1);
            _sut.GenericReturnType<string>().Returns("text");
            _sut.GenericReturnType<int>().Returns(2);
            _sut.GenericReturnType<int>();

            _sut.Instance().GenericReturnType<int>().ShouldBe(2);
            _sut.Instance().GenericReturnType<string>().ShouldBe("text");
            StoredMethodImposterCount(_sut, "_genericReturnTypeMethodImposterCollection")
                .ShouldBe(3);
        }

        // Method imposter collections are generated implementation details, so reflection is the only
        // way to observe whether an imposter is retained for every invocation that has no matching setup.
        private static int StoredMethodImposterCount(
            IMethodSetupFeatureSutImposter imposter,
            string collectionFieldName
        )
        {
            var collection = typeof(IMethodSetupFeatureSutImposter)
                .GetField(collectionFieldName, BindingFlags.Instance | BindingFlags.NonPublic)
                .ShouldNotBeNull()
                .GetValue(imposter)
                .ShouldNotBeNull();

            var imposters = collection
                .GetType()
                .GetField("_imposters", BindingFlags.Instance | BindingFlags.NonPublic)
                .ShouldNotBeNull()
                .GetValue(collection)
                .ShouldNotBeNull();

            var enumerator = imposters
                .GetType()
                .GetMethod(nameof(SetupCollection<object>.GetEnumerator))
                .ShouldNotBeNull()
                .Invoke(imposters, null)
                .ShouldNotBeNull();
            var moveNext = enumerator
                .GetType()
                .GetMethod(nameof(SetupCollection<object>.Enumerator.MoveNext))
                .ShouldNotBeNull();
            var count = 0;
            while ((bool)moveNext.Invoke(enumerator, null)!)
            {
                count++;
            }

            return count;
        }
    }
}
