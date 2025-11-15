using System;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

[assembly: GenerateImposter(typeof(Imposter.Tests.Docs.Methods.MyService))]

namespace Imposter.Tests.Docs.Methods
{
    public class MyService
    {
        public virtual int Add(int a, int b) => a + b;

        public virtual Task ProcessAsync(string s) => Task.CompletedTask;

        public virtual int MightFail(int v) => throw new InvalidOperationException("fail");
    }

    public class BaseImplementationTests
    {
        [Fact]
        public void Methods_BaseImplementation_BasicExample()
        {
            var imp = new MyServiceImposter();
            var svc = imp.Instance();

            // Forward calls to the original implementation
            imp.Add(Arg<int>.Any(), Arg<int>.Any()).UseBaseImplementation();
            var sum = svc.Add(2, 5); // 7 (calls MyService.Add)
            sum.ShouldBe(7);
        }

        [Fact]
        public void Methods_BaseImplementation_Matchers_And_Fallback()
        {
            var imp = new MyServiceImposter();
            var svc = imp.Instance();

            // Otherwise return a specific value (fallback)
            imp.Add(Arg<int>.Any(), Arg<int>.Any()).Returns(-1);

            // Only forward when the first value is positive (more specific rule)
            imp.Add(Arg<int>.Is(x => x > 0), Arg<int>.Any()).UseBaseImplementation();

            svc.Add(2, 3).ShouldBe(5); // base
            svc.Add(-2, 3).ShouldBe(-1); // fallback
        }

        [Fact]
        public void Methods_BaseImplementation_Sequence_With_Then()
        {
            var imp = new MyServiceImposter();
            var svc = imp.Instance();

            imp.Add(Arg<int>.Any(), Arg<int>.Any()).UseBaseImplementation().Then().Returns(100);

            svc.Add(1, 1).ShouldBe(2); // base -> 2
            svc.Add(1, 1).ShouldBe(100); // override after Then()
        }

        [Fact]
        public async Task Methods_BaseImplementation_Async_And_ExceptionFlow()
        {
            var imp = new MyServiceImposter();
            var svc = imp.Instance();

            // Async forwarding
            imp.ProcessAsync(Arg<string>.Any()).UseBaseImplementation();
            await svc.ProcessAsync("x");

            // Exceptions from base flow to caller
            imp.MightFail(Arg<int>.Any()).UseBaseImplementation();
            Should.Throw<InvalidOperationException>(() => svc.MightFail(5));
        }
    }
}
