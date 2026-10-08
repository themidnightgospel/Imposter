using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

[assembly: GenerateImposter(typeof(HttpMessageHandler))]

namespace Imposter.Tests.Features.ClassImpersonation
{
    // HttpMessageHandler declares SendAsync as protected internal in another assembly, so the imposter overrides it
    // as protected.
    public class HttpMessageHandlerImposterTests
    {
        private readonly HttpMessageHandlerImposter _sut = new HttpMessageHandlerImposter();

        [Fact]
        public async Task GivenSendAsyncSetup_WhenHttpClientSendsRequest_ShouldReturnConfiguredResponse()
        {
            _sut.SendAsync(Arg<HttpRequestMessage>.Any(), Arg<CancellationToken>.Any())
                .Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.Accepted)));
            using var client = new HttpClient(_sut.Instance());

            var response = await client.GetAsync("https://example.test/");

            response.StatusCode.ShouldBe(HttpStatusCode.Accepted);
        }
    }
}
