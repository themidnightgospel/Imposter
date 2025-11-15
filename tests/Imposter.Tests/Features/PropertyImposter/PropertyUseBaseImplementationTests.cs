using System;
using Imposter.Abstractions;
using Imposter.Tests.Features.PropertyImposter;
using Shouldly;
using Xunit;

[assembly: GenerateImposter(typeof(ClassWithVirtualProperty))]
[assembly: GenerateImposter(typeof(AdvancedClassWithVirtualProperties))]

namespace Imposter.Tests.Features.PropertyImposter
{
    public class PropertyUseBaseImplementationTests
    {
        [Fact]
        public void GivenVirtualProperty_WhenUseBaseImplementationCalled_ShouldReturnBaseImplementation()
        {
            var imposter = new ClassWithVirtualPropertyImposter();

            imposter
                .VirtualProperty.Getter()
                .UseBaseImplementation()
                .Then()
                .Returns("overridden")
                .Then()
                .UseBaseImplementation();

            var instance = imposter.Instance();

            instance.VirtualProperty.ShouldBe(null);
            instance.VirtualProperty.ShouldBe("overridden");
            instance.VirtualProperty.ShouldBe(null);
        }

        [Fact]
        public void GivenVirtualPropertyWithInitializer_WhenUseBaseImplementationCalled_ShouldReturnInitialValue()
        {
            var imposter = new ClassWithVirtualPropertyImposter();

            imposter
                .VirtualPropertyWithInitializer.Getter()
                .UseBaseImplementation()
                .Then()
                .Returns("overridden")
                .Then()
                .UseBaseImplementation();

            var instance = imposter.Instance();

            instance.VirtualPropertyWithInitializer.ShouldBe("initial-value");
            instance.VirtualPropertyWithInitializer.ShouldBe("overridden");
            instance.VirtualPropertyWithInitializer.ShouldBe("initial-value");
        }

        [Fact]
        public void GivenVirtualProperty_WhenUseBaseImplementationAlternatesWithReturns_ShouldFollowConfiguredSequence()
        {
            var imposter = new ClassWithVirtualPropertyImposter();

            imposter.VirtualProperty.Setter(Arg<string>.Any()).UseBaseImplementation();

            imposter
                .VirtualProperty.Getter()
                .UseBaseImplementation()
                .Then()
                .Returns("override-1")
                .Then()
                .UseBaseImplementation()
                .Then()
                .Returns("override-2")
                .Then()
                .UseBaseImplementation();

            var instance = imposter.Instance();

            instance.VirtualProperty = "base-1";
            instance.VirtualProperty.ShouldBe("base-1");
            instance.VirtualProperty.ShouldBe("override-1");
            instance.VirtualProperty = "base-2";
            instance.VirtualProperty.ShouldBe("base-2");
            instance.VirtualProperty.ShouldBe("override-2");
            instance.VirtualProperty = "base-3";
            instance.VirtualProperty.ShouldBe("base-3");
        }

        [Fact]
        public void GivenVirtualProperty_WhenGetterCallbackUsesBaseImplementation_ShouldInvokeCallbackAndReturnBaseValue()
        {
            var imposter = new ClassWithVirtualPropertyImposter();

            imposter.VirtualProperty.Setter(Arg<string>.Any()).UseBaseImplementation();

            var callbackCount = 0;

            imposter
                .VirtualProperty.Getter()
                .UseBaseImplementation()
                .Callback(() => callbackCount++);

            var instance = imposter.Instance();

            instance.VirtualProperty = "callback-base";

            instance.VirtualProperty.ShouldBe("callback-base");
            instance.VirtualProperty.ShouldBe("callback-base");

            callbackCount.ShouldBe(2);
        }

        [Fact]
        public void GivenVirtualPropertyWithThrowingGetter_WhenUseBaseImplementationCalled_ShouldPropagateException()
        {
            var imposter = new AdvancedClassWithVirtualPropertiesImposter();

            imposter.ThrowingGetterVirtualProperty.Getter().UseBaseImplementation();

            var instance = imposter.Instance();

            Should.Throw<InvalidOperationException>(() => instance.ThrowingGetterVirtualProperty);
        }

        [Fact]
        public void GivenGetterOnlyVirtualProperty_WhenUseBaseImplementationCalled_ShouldReturnBaseValueAndExposeNoSetter()
        {
            var imposter = new AdvancedClassWithVirtualPropertiesImposter();

            imposter.GetterOnlyVirtualProperty.Getter().UseBaseImplementation();

            var instance = imposter.Instance();
            instance.SetGetterOnlyBacking("getter-only-base");

            instance.GetterOnlyVirtualProperty.ShouldBe("getter-only-base");

            var builderType =
                typeof(AdvancedClassWithVirtualPropertiesImposter.IGetterOnlyVirtualPropertyPropertyBuilder);
            var hasSetter = false;

            foreach (var method in builderType.GetMethods())
            {
                if (method.Name == "Setter")
                {
                    hasSetter = true;
                    break;
                }
            }

            hasSetter.ShouldBeFalse();
        }

        [Fact]
        public void GivenVirtualPropertySetter_WhenUseBaseImplementationConfigured_ShouldUpdateBaseState()
        {
            var imposter = new ClassWithVirtualPropertyImposter();

            imposter.VirtualProperty.Setter(Arg<string>.Any()).UseBaseImplementation();
            imposter.VirtualProperty.Getter().UseBaseImplementation();

            var instance = imposter.Instance();

            instance.VirtualProperty = "x";
            instance.VirtualProperty.ShouldBe("x");
        }

        [Fact]
        public void GivenVirtualPropertyWithInitializerSetter_WhenUseBaseImplementationConfigured_ShouldUpdateBaseState()
        {
            var imposter = new ClassWithVirtualPropertyImposter();

            imposter
                .VirtualPropertyWithInitializer.Setter(Arg<string>.Any())
                .UseBaseImplementation();
            imposter.VirtualPropertyWithInitializer.Getter().UseBaseImplementation();

            var instance = imposter.Instance();

            instance.VirtualPropertyWithInitializer.ShouldBe("initial-value");

            instance.VirtualPropertyWithInitializer = "y";
            instance.VirtualPropertyWithInitializer.ShouldBe("y");
        }

        [Fact]
        public void GivenVirtualPropertySetter_WhenCallbackCombinedWithUseBaseImplementation_ShouldInvokeCallbackAndPersistValue()
        {
            var imposter = new ClassWithVirtualPropertyImposter();

            var callbackCount = 0;
            string callbackValue = string.Empty;

            imposter
                .VirtualProperty.Setter(Arg<string>.Any())
                .UseBaseImplementation()
                .Callback(value =>
                {
                    callbackCount++;
                    callbackValue = value;
                });

            imposter.VirtualProperty.Getter().UseBaseImplementation();

            var instance = imposter.Instance();

            instance.VirtualProperty = "callback-value";
            instance.VirtualProperty.ShouldBe("callback-value");

            callbackCount.ShouldBe(1);
            callbackValue.ShouldBe("callback-value");
        }

        [Fact]
        public void GivenVirtualPropertySetter_WhenBaseSetterThrows_ShouldPropagateException()
        {
            var imposter = new AdvancedClassWithVirtualPropertiesImposter();

            imposter
                .ThrowingSetterVirtualProperty.Setter(Arg<string>.Any())
                .UseBaseImplementation();

            var instance = imposter.Instance();

            Should.Throw<InvalidOperationException>(() =>
                instance.ThrowingSetterVirtualProperty = "value"
            );
        }

        [Fact]
        public void GivenSetterOnlyVirtualProperty_WhenUseBaseImplementationCalled_ShouldUpdateBackingStore()
        {
            var imposter = new AdvancedClassWithVirtualPropertiesImposter();

            imposter.SetterOnlyVirtualProperty.Setter(Arg<string>.Any()).UseBaseImplementation();

            var instance = imposter.Instance();

            instance.SetterOnlyVirtualProperty = "setter-only";

            instance.SetterOnlyBacking.ShouldBe("setter-only");
        }

        [Fact]
        public void GivenVirtualProperty_WhenSetterUseBaseImplementationInteractsWithGetterSequence_ShouldRespectConfiguredOrder()
        {
            var imposter = new ClassWithVirtualPropertyImposter();

            imposter.VirtualProperty.Setter(Arg<string>.Any()).UseBaseImplementation();

            imposter
                .VirtualProperty.Getter()
                .UseBaseImplementation()
                .Then()
                .Returns("X")
                .Then()
                .UseBaseImplementation();

            var instance = imposter.Instance();

            instance.VirtualProperty.ShouldBe(null);

            instance.VirtualProperty = "Y";
            instance.VirtualProperty.ShouldBe("X");
            instance.VirtualProperty.ShouldBe("Y");
        }

        [Fact]
        public void GivenVirtualProperty_WhenReturnsAndUseBaseImplementationInterleave_ShouldFollowSequence()
        {
            var imposter = new ClassWithVirtualPropertyImposter();

            imposter.VirtualProperty.Setter(Arg<string>.Any()).UseBaseImplementation();

            imposter
                .VirtualProperty.Getter()
                .Returns("A")
                .Then()
                .UseBaseImplementation()
                .Then()
                .Returns("B")
                .Then()
                .UseBaseImplementation();

            var instance = imposter.Instance();

            instance.VirtualProperty.ShouldBe("A");

            instance.VirtualProperty = "base-1";
            instance.VirtualProperty.ShouldBe("base-1");
            instance.VirtualProperty.ShouldBe("B");
            instance.VirtualProperty = "base-2";
            instance.VirtualProperty.ShouldBe("base-2");
        }

        [Fact]
        public void GivenVirtualProperty_WhenGetterThrowsBetweenBaseSteps_ShouldResumeBaseAfterSetterUseBaseImplementation()
        {
            var imposter = new ClassWithVirtualPropertyImposter();

            imposter.VirtualProperty.Setter(Arg<string>.Any()).UseBaseImplementation();

            imposter
                .VirtualProperty.Getter()
                .UseBaseImplementation()
                .Then()
                .Throws(new InvalidOperationException("getter failure"))
                .Then()
                .UseBaseImplementation();

            var instance = imposter.Instance();

            instance.VirtualProperty = "before-throw";
            instance.VirtualProperty.ShouldBe("before-throw");

            Should.Throw<InvalidOperationException>(() => instance.VirtualProperty);

            instance.VirtualProperty = "after-throw";
            instance.VirtualProperty.ShouldBe("after-throw");
        }
    }

    public class ClassWithVirtualProperty
    {
        public virtual string VirtualProperty { get; set; } = default!;

        public virtual string VirtualPropertyWithInitializer { get; set; } = "initial-value";
    }

    public class AdvancedClassWithVirtualProperties
    {
        private string _getterOnlyBacking = "getter-only-value";
        private string _setterOnlyBacking = string.Empty;
        private string _throwingSetterBacking = string.Empty;

        public virtual string ThrowingGetterVirtualProperty =>
            throw new InvalidOperationException("Base getter failed.");

        public virtual string GetterOnlyVirtualProperty => _getterOnlyBacking;

        public virtual string SetterOnlyVirtualProperty
        {
            set => _setterOnlyBacking = value;
        }

        public virtual string ThrowingSetterVirtualProperty
        {
            get => _throwingSetterBacking;
            set => throw new InvalidOperationException("Base setter failed.");
        }

        public string SetterOnlyBacking => _setterOnlyBacking;

        public void SetGetterOnlyBacking(string value)
        {
            _getterOnlyBacking = value;
        }
    }
}
