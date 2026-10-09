using System;
using Imposter.Abstractions;
using Imposter.Tests.Features.ClassImpersonation.Suts;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.ClassImpersonation
{
    public class NullableGenericMethodsTests
    {
        private readonly NullableGenericMethodsClassImposter _sut =
            new NullableGenericMethodsClassImposter();

        [Fact]
        public void GivenNullableReferenceConstraintSetup_WhenInvokedWithNull_ShouldReturnSetupValue()
        {
            var disposable = new Disposable();
            _sut.EchoNullableReference<Disposable?>(Arg<Disposable?>.Is(it => it == null))
                .Returns(disposable);

            _sut.Instance().EchoNullableReference<Disposable?>(null).ShouldBeSameAs(disposable);
        }

        [Fact]
        public void GivenNullableReferenceConstraintBaseImplementation_WhenInvoked_ShouldReturnTheArgument()
        {
            var disposable = new Disposable();
            _sut.EchoNullableReference<Disposable>(Arg<Disposable>.Any()).UseBaseImplementation();

            _sut.Instance().EchoNullableReference(disposable).ShouldBeSameAs(disposable);
        }

        [Fact]
        public void GivenMaybeDefaultSetup_WhenInvokedWithNull_ShouldReturnSetupValue()
        {
            _sut.EchoMaybeDefault<string>(Arg<string?>.Is(it => it == null)).Returns("fallback");

            _sut.Instance().EchoMaybeDefault<string>(null).ShouldBe("fallback");
        }

        [Fact]
        public void GivenMaybeDefaultBaseImplementation_WhenInvokedWithNull_ShouldReturnNull()
        {
            _sut.EchoMaybeDefault<string>(Arg<string?>.Any()).UseBaseImplementation();

            _sut.Instance().EchoMaybeDefault<string>(null).ShouldBeNull();
        }

        private sealed class Disposable : IDisposable
        {
            public void Dispose() { }
        }
    }
}
