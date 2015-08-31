using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Cerulean.Consul
{
    public class Query : IEnumerable<KeyValuePair<String, object>>
    {
        readonly IDictionary<string, object> _parameters;

        public Query()
        {
            _parameters = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);;
        }

        public void Add(string name)
        {
            _parameters.Add(name, null);
        }

        public void Add(string name, object value)
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
                : pair.Value.ToString();

            var assignment = string.IsNullOrEmpty(stringValue)
                ? pair.Key
                : string.Format("{0}={1}", pair.Key, WebUtility.UrlEncode(stringValue));

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