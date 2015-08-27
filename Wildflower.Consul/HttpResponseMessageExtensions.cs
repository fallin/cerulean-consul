using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Wildflower.Consul
{
    static class HttpResponseMessageExtensions
    {
        public static string GetHeader(this HttpResponseMessage response, string name)
        {
            string headerValue = null;
            IEnumerable<string> values;
            if (response.Headers.TryGetValues(name, out values))
            {
                headerValue = values.FirstOrDefault();
            }

            return headerValue;
        }
    }
}