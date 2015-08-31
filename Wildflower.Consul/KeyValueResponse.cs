using System;
using System.Net.Http;
using Wildflower.Consul.WebExtensions;

namespace Wildflower.Consul
{
    public class KeyValueResponse<TContent> : ConsulResponse
    {
        public int Index { get; private set; }
        public bool KnownLeader { get; private set; }
        public int LastContact { get; private set; }
        public TContent Content { get; private set; }

        public KeyValueResponse(HttpResponseMessage responseMessage, TContent content) 
            : base(responseMessage)
        {
            Index = responseMessage.GetHeaderValueAs<int>("X-Consul-Index");
            KnownLeader = responseMessage.GetHeaderValueAs<bool>("X-Consul-Knownleader");
            LastContact = responseMessage.GetHeaderValueAs<int>("X-Consul-Lastcontact");

            Content = content;
        }
    }
}