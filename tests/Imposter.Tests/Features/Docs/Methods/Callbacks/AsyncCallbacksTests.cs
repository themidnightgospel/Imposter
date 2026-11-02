using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Imposter.Tests.Features.Docs.Methods.Callbacks;
using Shouldly;
using Xunit;

[assembly: GenerateImposter(typeof(IAsyncCallbackService))]

namespace Imposter.Tests.Features.Docs.Methods.Callbacks
{
    public interface IAsyncCallbackService
    {
        Task<int> SaveAsync(string name);
    }

    public class AsyncCallbacksTests
    {
        [Fact]
        public async Task GivenAsyncMethodCallbacks_WhenAwaitingTheCall_ShouldCompleteAfterThemAndPropagateFailures()
        {
            var imposter = new IAsyncCallbackServiceImposter();
            var service = imposter.Instance();
            var saved = new List<string>();

            imposter
                .SaveAsync(Arg<string>.Any())
                .ReturnsAsync(1)
                .Callback(async name =>
                {
                    await Task.Yield();
                    saved.Add(name);
                });

            await service.SaveAsync("report"); // completes after the callback
            saved.ShouldBe(new[] { "report" });

            imposter
                .SaveAsync(Arg<string>.Is("broken"))
                .ReturnsAsync(1)
                .Callback(async _ =>
                {
                    await Task.Yield();
                    throw new InvalidOperationException("boom");
                });

            await Should.ThrowAsync<InvalidOperationException>(() => service.SaveAsync("broken"));
        }
    }
}
