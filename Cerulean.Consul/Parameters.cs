using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Cerulean.Consul
{
    public abstract class Parameters : IEnumerable<KeyValuePair<String, object>>
    {
        readonly IDictionary<string, object> _parameters;

        protected Parameters()
        {
            _parameters = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);;
        }

        protected void Add(string name)
        {
            _parameters.Add(name, null);
        }

        protected void Add(string name, object value)
        {
            _parameters.Add(name, value);
        }

        public string ToQueryString()
        {
            IEnumerable<string> parts = _parameters.Where(ValidQueryItem).Select(WithAssignment);
            string query = string.Join("&", parts);
            return query;
        }

        static bool ValidQueryItem(KeyValuePair<string, object> pair)
        {
            return !string.IsNullOrWhiteSpace(pair.Key);
        }

        static string WithAssignment(KeyValuePair<string, object> pair)
        {
            string stringValue = (pair.Value == null)
                ? string.Empty
                : pair.Value.ToString().ToLowerInvariant();
            string key = pair.Key.ToLowerInvariant();

            var assignment = string.IsNullOrEmpty(stringValue)
                ? key
                : string.Format("{0}={1}", key, WebUtility.UrlEncode(stringValue));

            return assignment;
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _parameters.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _parameters).GetEnumerator();
        }
    }
}