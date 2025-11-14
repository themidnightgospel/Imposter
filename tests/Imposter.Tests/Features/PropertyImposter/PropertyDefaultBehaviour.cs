using System.Collections.Generic;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.PropertyImposter
{
    public class PropertyDefaultBehaviour
    {
        private readonly IPropertySetupSutImposter _sut =
#if USE_CSHARP14
            IPropertySetupSut.Imposter();
#else
            new IPropertySetupSutImposter();
#endif

        [Fact]
        public void GivenNoInteractions_WhenVerifying_ShouldWork()
        {
            // No interactions yet
            Should.NotThrow(() => _sut.Age.Getter().Called(Count.Never()));
            Should.NotThrow(() => _sut.Age.Setter(Arg<int>.Any()).Called(Count.Never()));
        }

        [Fact]
        public void GivenClassWithInitializer_WhenFirstRead_DefaultBehaviourReturnsInitialValue()
        {
            var classSut = new ClassWithInitialValuePropertyImposter();
            var instance = classSut.Instance();

            // Default behavior should use the initializer value on first read
            instance.A.ShouldBe(11);

            // And still behave like an auto-property until explicitly configured
            instance.A = 99;
            instance.A.ShouldBe(99);
        }

        [Fact]
        public void GivenAutoPropertyThenExplicitSetup_WhenPropertyIsAccessed_ShouldLoseAutoPropertyBehavior()
        {
            var instance = _sut.Instance();

            // Initially auto-property
            instance.Age = 25;
            instance.Age.ShouldBe(25);

            // Setup explicit return
            _sut.Age.Getter().Returns(42);

            // Now it's explicit - setting should not affect getter
            instance.Age = 100;
            instance.Age.ShouldBe(42); // Should still return 42, ignoring the set
        }

        [Fact]
        public void GivenClassWithInitializer_WhenSetBeforeAnyRead_DefaultBehaviourReturnsSetValueNotInitializer()
        {
            var classSut = new ClassWithInitialValuePropertyImposter();
            var instance = classSut.Instance();

            instance.A = 77;
            instance.A.ShouldBe(77);
        }

        [Fact]
        public void GivenClassWithInitializer_WhenMultipleReadsWithoutSet_ShouldRemainInitializerValue()
        {
            var classSut = new ClassWithInitialValuePropertyImposter();
            var instance = classSut.Instance();

            instance.A.ShouldBe(11);
            instance.A.ShouldBe(11);
        }

        [Fact]
        public void GivenClassWithInitializerString_WhenFirstRead_DefaultBehaviourReturnsInitialString()
        {
            var classSut = new ClassWithInitialValueStringPropertyImposter();
            var instance = classSut.Instance();

            instance.S.ShouldBe("hello");

            instance.S = "world";
            instance.S.ShouldBe("world");
        }

        [Fact]
        public void GivenExplicitSetup_WhenPropertyIsModified_ShouldNotRevertToAutoProperty()
        {
            var instance = _sut.Instance();

            // Setup as explicit
            _sut.Age.Getter().Returns(42);
            instance.Age.ShouldBe(42);

            // Set a value (this goes to backing field but won't be returned)
            instance.Age = 100;

            // Still returns explicit value, not set value
            instance.Age.ShouldBe(42);
        }

        [Fact]
        public void GivenComplexScenario_WhenTransitioningFromAutoToExplicitWithCallbacks_ShouldWorkCorrectly()
        {
            var instance = _sut.Instance();
            var callbackValues = new List<int>();

            // Start as auto property
            instance.Age = 25;
            instance.Age.ShouldBe(25);

            // Add setter callback
            _sut.Age.Setter(Arg<int>.Is(x => x > 30)).Callback(value => callbackValues.Add(value));

            // Still auto property
            instance.Age = 35;
            instance.Age.ShouldBe(35);
            callbackValues.ShouldContain(35);

            // Switch to explicit
            _sut.Age.Getter().Returns(100);

            // Now getter returns explicit value
            instance.Age.ShouldBe(100);

            // But setter still triggers callbacks
            instance.Age = 40;
            instance.Age.ShouldBe(100); // Still explicit return
            callbackValues.ShouldContain(40);

            // Verification works for all interactions
            _sut.Age.Setter(Arg<int>.Any()).Called(Count.Exactly(3));
            _sut.Age.Getter().Called(Count.AtLeast(4));
        }
    }
}
