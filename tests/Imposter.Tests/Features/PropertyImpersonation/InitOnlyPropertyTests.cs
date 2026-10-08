using System;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.PropertyImpersonation
{
    public class InterfaceInitOnlyPropertyTests
    {
        private readonly IInitOnlyPropertySutImposter _sut = new IInitOnlyPropertySutImposter();

        [Fact]
        public void Given_InitOnlyProperty_When_Initialized_Should_StoreValueAndInvokeCallback()
        {
            var observed = 0;
            _sut.Value.Setter(42).Callback(value => observed = value);
            var instance = _sut.Instance();

            InitOnlyAccessor.Invoke(instance, nameof(instance.Value), 42);

            instance.Value.ShouldBe(42);
            observed.ShouldBe(42);
            _sut.Value.Setter(42).Called(Count.Once());
            _sut.Value.Setter(7).Called(Count.Never());
        }

        [Fact]
        public void Given_InitOnlyProperty_When_GetterIsConfigured_Should_ReturnConfiguredValue()
        {
            _sut.Value.Getter().Returns(99);

            _sut.Instance().Value.ShouldBe(99);
        }
    }

    public class AbstractInitOnlyPropertyTests
    {
        private readonly AbstractInitOnlyPropertySutImposter _sut =
            new AbstractInitOnlyPropertySutImposter();

        [Fact]
        public void Given_AbstractInitOnlyProperty_When_Initialized_Should_StoreValueAndVerifyCall()
        {
            var instance = _sut.Instance();

            InitOnlyAccessor.Invoke(instance, nameof(instance.Value), 42);

            instance.Value.ShouldBe(42);
            _sut.Value.Setter(42).Called(Count.Once());
        }
    }

    public class VirtualInitOnlyPropertyTests
    {
        private readonly VirtualInitOnlyPropertySutImposter _sut =
            new VirtualInitOnlyPropertySutImposter();

        [Fact]
        public void Given_InitOnlyProperty_When_BaseIsNotConfigured_Should_UseImposterStorage()
        {
            var instance = _sut.Instance();

            InitOnlyAccessor.Invoke(instance, nameof(instance.Value), 42);

            instance.Value.ShouldBe(42);
            instance.BaseValue.ShouldBe(10);
            instance.BaseInitCount.ShouldBe(0);
            _sut.Value.Setter(42).Called(Count.Once());
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Given_InitOnlyProperty_When_BaseIsConfigured_Should_InvokeCallbackBeforeBase(
            bool configureWholeProperty
        )
        {
            if (configureWholeProperty)
            {
                _sut.Value.UseBaseImplementation();
            }
            else
            {
                _sut.Value.Getter().UseBaseImplementation();
                _sut.Value.Setter(Arg<int>.Any()).UseBaseImplementation();
            }
            var instance = _sut.Instance();
            var valueBeforeBase = 0;
            _sut.Value.Setter(42).Callback(_ => valueBeforeBase = instance.BaseValue);

            InitOnlyAccessor.Invoke(instance, nameof(instance.Value), 42);

            valueBeforeBase.ShouldBe(10);
            instance.BaseValue.ShouldBe(42);
            instance.BaseInitCount.ShouldBe(1);
            instance.Value.ShouldBe(42);
            _sut.Value.Setter(42).Called(Count.Once());
        }

        [Fact]
        public void Given_InitOnlyProperty_When_CallbackThrows_Should_NotInvokeBase()
        {
            _sut.Value.UseBaseImplementation();
            _sut.Value.Setter(42).Callback(_ => throw new InvalidOperationException("callback"));
            var instance = _sut.Instance();

            Should
                .Throw<InvalidOperationException>(() =>
                    InitOnlyAccessor.Invoke(instance, nameof(instance.Value), 42)
                )
                .Message.ShouldBe("callback");

            instance.BaseInitCount.ShouldBe(0);
            _sut.Value.Setter(42).Called(Count.Once());
        }

        [Fact]
        public void Given_WriteOnlyInit_When_BaseIsConfigured_Should_InitializeBase()
        {
            _sut.WriteOnly.Setter(Arg<int>.Any()).UseBaseImplementation();
            var instance = _sut.Instance();

            InitOnlyAccessor.Invoke(instance, nameof(instance.WriteOnly), 42);

            instance.BaseValue.ShouldBe(42);
            instance.BaseInitCount.ShouldBe(1);
            _sut.WriteOnly.Setter(42).Called(Count.Once());
        }

        [Fact]
        public void Given_ThrowingBaseInit_When_Initialized_Should_PropagateExceptionAndRecordCall()
        {
            _sut.Throwing.UseBaseImplementation();
            var instance = _sut.Instance();

            Should
                .Throw<InvalidOperationException>(() =>
                    InitOnlyAccessor.Invoke(instance, nameof(instance.Throwing), 42)
                )
                .Message.ShouldBe("Base init failed.");

            _sut.Throwing.Setter(42).Called(Count.Once());
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Given_ExplicitMode_When_Initializing_Should_RequireSetterSetup(
            bool configureBase
        )
        {
            var sut = new VirtualInitOnlyPropertySutImposter(ImposterMode.Explicit);
            var instance = sut.Instance();
            if (configureBase)
            {
                sut.Value.UseBaseImplementation();
                InitOnlyAccessor.Invoke(instance, nameof(instance.Value), 42);
                instance.BaseValue.ShouldBe(42);
                sut.Value.Setter(42).Called(Count.Once());
            }
            else
            {
                Should.Throw<MissingImposterException>(() =>
                    InitOnlyAccessor.Invoke(instance, nameof(instance.Value), 42)
                );
                instance.BaseInitCount.ShouldBe(0);
                sut.Value.Setter(42).Called(Count.Never());
            }
        }
    }

    internal static class InitOnlyAccessor
    {
        // Instance() returns an already constructed object, so C# cannot assign its init accessor.
        // Invoke that accessor directly to test its runtime behavior; compilation tests enforce
        // the init-only restriction and the legality of the generated base assignment.
        internal static void Invoke(object instance, string propertyName, int value) =>
            instance
                .GetType()
                .GetProperty(propertyName)!
                .SetMethod!.CreateDelegate<Action<int>>(instance)(value);
    }
}
