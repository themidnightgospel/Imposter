using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.EventImpersonation
{
    public class CallbackAndOrderingTests
    {
        private readonly IEventSetupSutImposter _sut =
#if USE_CSHARP14
        IEventSetupSut.Imposter();
#else
        new IEventSetupSutImposter();
#endif

        [Fact]
        public void GivenCallbacks_WhenRaise_ShouldInvokeCallbacksBeforeHandlers()
        {
            var sequence = new List<string>();
            _sut.SomethingHappened.Callback((s, e) => sequence.Add("callback1"));
            _sut.SomethingHappened.Callback((s, e) => sequence.Add("callback2"));

            _sut.Instance().SomethingHappened += (s, e) => sequence.Add("handler1");
            _sut.Instance().SomethingHappened += (s, e) => sequence.Add("handler2");

            _sut.SomethingHappened.Raise(this, EventArgs.Empty);

            sequence.ShouldBe(new[] { "callback1", "callback2", "handler1", "handler2" });
        }

        [Fact]
        public async Task GivenCallbacks_WhenRaiseAsync_ShouldInvokeCallbacksBeforeHandlers()
        {
            var sequence = new List<string>();
            _sut.AsyncSomethingHappened.Callback(
                (s, e) =>
                {
                    sequence.Add("callback1");
                    return Task.CompletedTask;
                }
            );
            _sut.AsyncSomethingHappened.Callback(
                (s, e) =>
                {
                    sequence.Add("callback2");
                    return Task.CompletedTask;
                }
            );

            _sut.Instance().AsyncSomethingHappened += (s, e) =>
            {
                sequence.Add("handler1");
                return Task.CompletedTask;
            };
            _sut.Instance().AsyncSomethingHappened += (s, e) =>
            {
                sequence.Add("handler2");
                return Task.CompletedTask;
            };

            await _sut.AsyncSomethingHappened.RaiseAsync(this, EventArgs.Empty);

            sequence.ShouldBe(new[] { "callback1", "callback2", "handler1", "handler2" });
        }

        [Fact]
        public void GivenMultipleCallbacks_WhenRaise_ShouldInvokeAllCallbacks()
        {
            int c1 = 0,
                c2 = 0;
            _sut.SomethingHappened.Callback((s, e) => c1++);
            _sut.SomethingHappened.Callback((s, e) => c2++);

            _sut.SomethingHappened.Raise(this, EventArgs.Empty);

            c1.ShouldBe(1);
            c2.ShouldBe(1);
        }
    }
}
