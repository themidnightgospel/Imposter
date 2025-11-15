using System;
using System.Collections.Generic;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

[assembly: GenerateImposter(typeof(Imposter.Tests.Docs.Methods.ICallbackService))]

namespace Imposter.Tests.Docs.Methods
{
    public interface ICallbackService
    {
        int GetNumber();
        int Increment(int v);
        int GenericAllRefKind(out int o, ref string r, in double d, bool[] args);
    }

    public class CallbacksTests
    {
        [Fact]
        public void Methods_Callbacks_Ordering_Relative_To_Results()
        {
            var imposter = new ICallbackServiceImposter();
            var service = imposter.Instance();

            var stages = new List<string>();

            imposter
                .GetNumber()
                .Returns(() =>
                {
                    stages.Add("return");
                    return 42;
                })
                .Callback(() => stages.Add("first"))
                .Callback(() => stages.Add("second"));

            service.GetNumber();
            stages.ShouldBe(new[] { "return", "first", "second" });
        }

        [Fact]
        public void Methods_Callbacks_Parameterized()
        {
            var imposter = new ICallbackServiceImposter();
            var service = imposter.Instance();

            var seen = new List<int>();
            imposter.Increment(Arg<int>.Any()).Returns(v => v + 1).Callback(v => seen.Add(v));

            service.Increment(5).ShouldBe(6);
            seen.ShouldBe(new[] { 5 });
        }

        [Fact]
        public void Methods_Callbacks_RefOutIn()
        {
            var imposter = new ICallbackServiceImposter();
            var service = imposter.Instance();

            imposter
                .GenericAllRefKind(
                    OutArg<int>.Any(),
                    Arg<string>.Any(),
                    Arg<double>.Any(),
                    Arg<bool[]>.Any()
                )
                .Returns(
                    (out int o, ref string r, in double d, bool[] args) =>
                    {
                        o = 5;
                        return 99;
                    }
                )
                .Callback(
                    (out int o, ref string r, in double d, bool[] args) =>
                    {
                        o = 5;
                    }
                );

            string r = "x";
            double d = 3.14;
            int result = service.GenericAllRefKind(out var o, ref r, in d, new bool[] { true });
            result.ShouldBe(99);
            o.ShouldBe(5);
        }

        [Fact]
        public void Methods_Callbacks_Sequence_Local()
        {
            var imposter = new ICallbackServiceImposter();
            var service = imposter.Instance();

            var seen = new List<string>();

            imposter
                .Increment(Arg<int>.Any())
                .Returns(_ => 10)
                .Callback(_ => seen.Add("first"))
                .Then()
                .Returns(_ => 20)
                .Callback(_ => seen.Add("second"));

            service.Increment(1);
            service.Increment(2);
            seen.ShouldBe(new[] { "first", "second" });
        }

        [Fact]
        public void Methods_Callbacks_That_Throw()
        {
            var imposter = new ICallbackServiceImposter();
            var service = imposter.Instance();

            imposter
                .GetNumber()
                .Returns(1)
                .Callback(() => throw new InvalidOperationException("boom"));

            // service.GetNumber() returns 1, then throws
            var ex = Should.Throw<InvalidOperationException>(() => service.GetNumber());
            ex.Message.ShouldBe("boom");
        }
    }
}
