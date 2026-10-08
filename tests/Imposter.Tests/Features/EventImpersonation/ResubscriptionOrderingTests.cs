using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.EventImpersonation
{
    // Each test applies the same subscriptions to an imposter and to an ordinary C# event, whose order the imposter
    // must reproduce: += appends a handler and -= removes its last subscription.
    public class ResubscriptionOrderingTests
    {
        private readonly IEventSetupSutImposter _sut = new IEventSetupSutImposter();
        private readonly List<string> _calls = new List<string>();

        [Fact]
        public void GivenHandlerResubscribed_WhenRaised_ShouldCallItAfterTheOtherHandler()
        {
            AssertSameOrderAsOrdinaryEvent("+A +B -A +A");
        }

        [Fact]
        public void GivenHandlerResubscribedTwice_WhenRaised_ShouldCallItTwiceAfterTheOtherHandler()
        {
            AssertSameOrderAsOrdinaryEvent("+A +B -A +A +A");
        }

        [Fact]
        public void GivenDuplicateHandlerUnsubscribed_WhenRaised_ShouldRemoveItsLastSubscription()
        {
            AssertSameOrderAsOrdinaryEvent("+A +B +A -A");
        }

        [Fact]
        public void GivenHandlersSubscribedInOrder_WhenRaised_ShouldCallThemInThatOrder()
        {
            AssertSameOrderAsOrdinaryEvent("+A +B");
        }

        [Fact]
        public async Task GivenAsyncHandlerResubscribed_WhenRaisedAsync_ShouldCallItAfterTheOtherHandler()
        {
            Func<object?, EventArgs, Task> a = (_, _) => Record("A");
            Func<object?, EventArgs, Task> b = (_, _) => Record("B");
            var instance = _sut.Instance();
            instance.AsyncSomethingHappened += a;
            instance.AsyncSomethingHappened += b;
            instance.AsyncSomethingHappened -= a;
            instance.AsyncSomethingHappened += a;

            await _sut.AsyncSomethingHappened.RaiseAsync(this, EventArgs.Empty);

            _calls.ShouldBe(new[] { "B", "A" });
        }

        private void AssertSameOrderAsOrdinaryEvent(string operations)
        {
            EventHandler a = (_, _) => _calls.Add("A");
            EventHandler b = (_, _) => _calls.Add("B");
            EventHandler? ordinaryEvent = null;
            var instance = _sut.Instance();

            foreach (var operation in operations.Split(' '))
            {
                var handler = operation[1] == 'A' ? a : b;
                if (operation[0] == '+')
                {
                    instance.SomethingHappened += handler;
                    ordinaryEvent += handler;
                }
                else
                {
                    instance.SomethingHappened -= handler;
                    ordinaryEvent -= handler;
                }
            }

            ordinaryEvent?.Invoke(this, EventArgs.Empty);
            var expected = _calls.ToArray();
            _calls.Clear();

            _sut.SomethingHappened.Raise(this, EventArgs.Empty);

            _calls.ShouldBe(expected);
        }

        private Task Record(string handler)
        {
            _calls.Add(handler);
            return Task.CompletedTask;
        }
    }
}
