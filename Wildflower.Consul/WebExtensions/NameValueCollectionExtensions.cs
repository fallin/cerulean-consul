using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Wildflower.Consul.WebExtensions
{
    static class NameValueCollectionExtensions
    {
        public static string ToQueryString(this NameValueCollection collection)
        {
            IEnumerable<string> parts = collection.AllPairs().Select(WithAssignment);
            string query = string.Join("&", parts);
            return query;
        }

        static IEnumerable<KeyValuePair<string, string>> AllPairs(this NameValueCollection collection)
        {
            if (collection != null)
            {
                foreach (string key in collection.Keys)
                {
                    if (string.IsNullOrEmpty(key)) continue;
                    
                    yield return new KeyValuePair<string, string>(key, collection[key]);
                }
            }
        }

        static string WithAssignment(KeyValuePair<string, string> queryParameter)
        {
            if (string.IsNullOrEmpty(queryParameter.Value))
            {
                return queryParameter.Key;
            }
            return string.Format("{0}={1}", queryParameter.Key, queryParameter.Value);
        }
    }
}