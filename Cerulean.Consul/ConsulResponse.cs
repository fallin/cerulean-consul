using System;
using System.Net;
using System.Net.Http;

namespace Cerulean.Consul
{
    public class ConsulResponse
    {
        public HttpStatusCode StatusCode { get; private set; }
        public string ReasonPhrase { get; private set; }

        public ConsulResponse(HttpResponseMessage responseMessage)
        {
            if (responseMessage == null) throw new ArgumentNullException("responseMessage");

            StatusCode = responseMessage.StatusCode;
            ReasonPhrase = responseMessage.ReasonPhrase;
        }
    }
}