using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;

namespace Wildflower.Consul.WebExtensions
{
    static class HttpResponseMessageExtensions
    {
        public static string GetHeaderValue(this HttpResponseMessage response, string name)
        {
            string headerValue = null;
            IEnumerable<string> values;
            if (response.Headers.TryGetValues(name, out values))
            {
                headerValue = values.FirstOrDefault();
            }

            return headerValue;
        }

        public static T GetHeaderValueAs<T>(this HttpResponseMessage response, string name)
        {
            T coercedValue = default(T);

            string headerValue = response.GetHeaderValue(name);
            if (headerValue != null)
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
                coercedValue = (T)converter.ConvertFromString(headerValue);
            }

            return coercedValue;
        }
    }
}