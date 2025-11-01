using System.Collections.Generic;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.PropertySetup
{
    public class AutoPropertyTests
    {
        private readonly IPropertySetupSutImposter _sut = new IPropertySetupSutImposter();

        [Fact]
        public void GivenNoInteractions_WhenVerifying_ShouldWork()
        {
            // No interactions yet
            Should.NotThrow(() => _sut.Age.Getter().Called(Count.Never()));
            Should.NotThrow(() => _sut.Age.Setter(Arg<int>.Any()).Called(Count.Never()));
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