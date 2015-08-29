using System;
using System.Net;
using System.Net.Http;
using Wildflower.Consul.WebExtensions;

namespace Wildflower.Consul
{
    public class KeyValueResponse : ConsulResponse<KeyValueResource[]>
    {
        public KeyValueResponse(HttpResponseMessage responseMessage)
            : base(responseMessage)
        { }

        public int Index
        {
            get { return ResponseMessage.GetHeaderValueAs<int>("X-Consul-Index"); }
        }

        public bool KnownLeader
        {
            get { return ResponseMessage.GetHeaderValueAs<bool>("X-Consul-Knownleader"); }
        }

        public int LastContact
        {
            get { return ResponseMessage.GetHeaderValueAs<int>("X-Consul-Lastcontact");  }
        }

        public bool Found
        {
            get { return ResponseMessage.StatusCode == HttpStatusCode.OK; }
        }
    }
}