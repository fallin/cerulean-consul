using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;

namespace Wildflower.Consul.Tests.TestingUtilities
{
    static class HttpClientExtensions
    {
        public static Mock<HttpMessageHandler> WithResponse(this Mock<HttpMessageHandler> mock,
            HttpStatusCode statusCode,
            object payload)
        {
            var response = new HttpResponseMessage(statusCode);
            string json = JsonConvert.SerializeObject(payload);
            response.Content = new ByteArrayContent(Encoding.UTF8.GetBytes(json));

            // Interestingly, passing our orbit (vendor specific) media type to the MediaTypeHeaderValue
            // constructor will throw a FormatException. Using 'Parse' seems to work fine.
            response.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            object[] args = { ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>() };
            // Moq can fake virtual methods, but SendAsync is protected, so we need to use 'protected' mode
            mock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", args)
                .Returns(() => Task.FromResult(response))
                // Setup the response to refer to the original request (this is expected)
                .Callback<HttpRequestMessage, CancellationToken>((req, ctk) => response.RequestMessage = req);

            return mock;
        }
    }
}