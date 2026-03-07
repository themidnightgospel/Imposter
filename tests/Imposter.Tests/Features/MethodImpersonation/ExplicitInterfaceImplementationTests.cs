using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.MethodImpersonation
{
    public class ExplicitInterfaceImplementationTests
    {
        private readonly IMyCollectionSutImposter _sut = new IMyCollectionSutImposter();

        [Fact]
        public void GivenImposter_WhenCastToInterface_ShouldBeAssignable()
        {
            IMyCollectionSut instance = _sut.Instance();

            instance.ShouldNotBeNull();
        }

        [Fact]
        public void GivenImposter_WhenCastToGenericEnumerable_ShouldBeAssignable()
        {
            IEnumerable<string> instance = _sut.Instance();

            instance.ShouldNotBeNull();
        }

        [Fact]
        public void GivenImposter_WhenCastToNonGenericEnumerable_ShouldBeAssignable()
        {
            IEnumerable instance = _sut.Instance();

            instance.ShouldNotBeNull();
        }

        [Fact]
        public void GivenGenericGetEnumeratorSetup_WhenInvoked_ShouldReturnConfiguredEnumerator()
        {
            var expected = new List<string> { "a", "b", "c" };
            _sut.GetEnumerator().Returns(expected.GetEnumerator());

            var result = new List<string>();
            using (var enumerator = ((IEnumerable<string>)_sut.Instance()).GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    result.Add(enumerator.Current);
                }
            }

            result.ShouldBe(expected);
        }

        [Fact]
        public void GivenNonGenericGetEnumeratorSetup_WhenInvoked_ShouldReturnConfiguredEnumerator()
        {
            var expected = new List<string> { "x", "y" };
            _sut.GetEnumerator_1().Returns(expected.GetEnumerator());

            var enumerator = ((IEnumerable)_sut.Instance()).GetEnumerator();

            enumerator.MoveNext().ShouldBeTrue();
            enumerator.Current.ShouldBe("x");
            enumerator.MoveNext().ShouldBeTrue();
            enumerator.Current.ShouldBe("y");
            enumerator.MoveNext().ShouldBeFalse();
        }

        [Fact]
        public void GivenAddMethodSetup_WhenInvoked_ShouldNotThrow()
        {
            _sut.Add(Arg<string>.Any());

            Should.NotThrow(() => _sut.Instance().Add("hello"));
        }

        [Fact]
        public void GivenGenericGetEnumeratorSetup_WhenUsedWithLinq_ShouldWork()
        {
            var items = new List<string> { "one", "two", "three" };
            _sut.GetEnumerator().Returns(items.GetEnumerator());

            var result = _sut.Instance().First();

            result.ShouldBe("one");
        }

        [Fact]
        public void GivenGenericGetEnumeratorVerification_WhenCalled_ShouldTrackInvocations()
        {
            var items = new List<string> { "a" };
            _sut.GetEnumerator().Returns(items.GetEnumerator());

            _ = _sut.Instance().First();

            _sut.GetEnumerator().Called(Count.AtLeast(1));
        }

        [Fact]
        public void GivenNonGenericGetEnumeratorVerification_WhenCalled_ShouldTrackInvocations()
        {
            var items = new List<string> { "a" };
            _sut.GetEnumerator_1().Returns(items.GetEnumerator());

            ((IEnumerable)_sut.Instance()).GetEnumerator();

            _sut.GetEnumerator_1().Called(Count.Once());
        }
    }

    public class ExplicitInterfacePropertyTests
    {
        private readonly IConflictingPropertySutImposter _sut =
            new IConflictingPropertySutImposter();

        [Fact]
        public void GivenImposter_WhenCastToIHasStringValue_ShouldBeAssignable()
        {
            IHasStringValue instance = _sut.Instance();

            instance.ShouldNotBeNull();
        }

        [Fact]
        public void GivenImposter_WhenCastToIHasIntValue_ShouldBeAssignable()
        {
            IHasIntValue instance = _sut.Instance();

            instance.ShouldNotBeNull();
        }

        [Fact]
        public void GivenStringValueSetup_WhenAccessedViaIHasStringValue_ShouldReturnConfiguredValue()
        {
            _sut.Value.Getter().Returns("hello");

            var result = ((IHasStringValue)_sut.Instance()).Value;

            result.ShouldBe("hello");
        }

        [Fact]
        public void GivenIntValueSetup_WhenAccessedViaIHasIntValue_ShouldReturnConfiguredValue()
        {
            _sut.Value_1.Getter().Returns(42);

            var result = ((IHasIntValue)_sut.Instance()).Value;

            result.ShouldBe(42);
        }

        [Fact]
        public void GivenNonConflictingPropertySetup_WhenAccessed_ShouldReturnConfiguredValue()
        {
            _sut.Name.Getter().Returns("test");

            var result = _sut.Instance().Name;

            result.ShouldBe("test");
        }

        [Fact]
        public void GivenStringValueVerification_WhenCalled_ShouldTrackInvocations()
        {
            _sut.Value.Getter().Returns("tracked");

            _ = ((IHasStringValue)_sut.Instance()).Value;

            _sut.Value.Getter().Called(Count.Once());
        }

        [Fact]
        public void GivenIntValueVerification_WhenCalled_ShouldTrackInvocations()
        {
            _sut.Value_1.Getter().Returns(99);

            _ = ((IHasIntValue)_sut.Instance()).Value;

            _sut.Value_1.Getter().Called(Count.Once());
        }
    }

    public class ExplicitInterfaceEventTests
    {
        private readonly IConflictingEventSutImposter _sut = new IConflictingEventSutImposter();

        [Fact]
        public void GivenImposter_WhenCastToIHasActionChanged_ShouldBeAssignable()
        {
            IHasActionChanged instance = _sut.Instance();

            instance.ShouldNotBeNull();
        }

        [Fact]
        public void GivenImposter_WhenCastToIHasEventHandlerChanged_ShouldBeAssignable()
        {
            IHasEventHandlerChanged instance = _sut.Instance();

            instance.ShouldNotBeNull();
        }

        [Fact]
        public void GivenActionChangedSubscription_WhenRaised_ShouldInvokeHandler()
        {
            var raised = false;
            ((IHasActionChanged)_sut.Instance()).Changed += () => raised = true;

            _sut.Changed.Raise();

            raised.ShouldBeTrue();
        }

        [Fact]
        public void GivenEventHandlerChangedSubscription_WhenRaised_ShouldInvokeHandler()
        {
            var raised = false;
            ((IHasEventHandlerChanged)_sut.Instance()).Changed += (sender, args) => raised = true;

            _sut.Changed_1.Raise(this, EventArgs.Empty);

            raised.ShouldBeTrue();
        }

        [Fact]
        public void GivenNonConflictingEventSubscription_WhenRaised_ShouldInvokeHandler()
        {
            var raised = false;
            _sut.Instance().CustomEvent += () => raised = true;

            _sut.CustomEvent.Raise();

            raised.ShouldBeTrue();
        }

        [Fact]
        public void GivenActionChangedVerification_WhenSubscribed_ShouldTrack()
        {
            ((IHasActionChanged)_sut.Instance()).Changed += () => { };

            _sut.Changed.Subscribed(Arg<Action>.Any(), Count.Once());
        }

        [Fact]
        public void GivenEventHandlerChangedVerification_WhenSubscribed_ShouldTrack()
        {
            ((IHasEventHandlerChanged)_sut.Instance()).Changed += (sender, args) => { };

            _sut.Changed_1.Subscribed(Arg<EventHandler>.Any(), Count.Once());
        }
    }
}
