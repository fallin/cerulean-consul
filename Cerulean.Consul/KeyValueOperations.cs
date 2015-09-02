using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Cerulean.Consul
{
    public class KeyValueOperations : ServiceOperations
    {
        readonly HttpClient _client;

        internal KeyValueOperations(HttpClient client)
        {
            _client = client;
        }

        public Task<KeyValueResponse<IList<KeyValue>>> GetAllAsync()
        {
            return GetAsync(string.Empty, opt => opt.Recurse = true);
        }

        public async Task<KeyValueResponse<IList<KeyValue>>> GetAsync(string key, Action<KeyValueGetOptions> opts = null)
        {
            var options = AssignOptions(opts);
            return await GetAsync<IList<KeyValue>>(key, options);
        }

        public async Task<KeyValueResponse<string>> GetRawAsync(string key, Action<KeyValueGetRawOptions> opts = null)
        {
            var options = AssignOptions(opts);
            return await GetAsync<string>(key, options);
        }

        public async Task<KeyValueResponse<IList<string>>> GetKeysAsync(string key, Action<KeyValueGetKeysOptions> opts = null)
        {
            var options = AssignOptions(opts);
            return await GetAsync<IList<string>>(key, options);
        }

        public async Task<KeyValueResponse<dynamic>> GetDynamicAsync(string key, dynamic options = null)
        {
            if (key == null) throw new ArgumentNullException("key");

            string uri = Uri.EscapeUriString(String.Format("v1/kv/{0}", key));

            JObject jo = JObject.FromObject(options);
            JObjectQueryProvider queryProvider = new JObjectQueryProvider(jo);
            AppendQueryParameters(ref uri, queryProvider);

            HttpResponseMessage response = await _client.GetAsync(uri);

            dynamic content = await ReadContentAsync<dynamic>(response);
            var reply = new KeyValueResponse<dynamic>(response, content);
            return reply;
        }

        public async Task<bool> PutAsync(string key, string value, Action<KeyValuePutOptions> opts = null)
        {
            if (key == null) throw new ArgumentNullException("key");

            string uri = Uri.EscapeUriString(String.Format("v1/kv/{0}", key));
            var options = AssignOptions(opts);
            AppendQueryParameters(ref uri, options);

            HttpContent content = new StringContent(value);
            HttpResponseMessage response = await _client.PutAsync(uri, content);

            bool reply = await ReadContentAsync<bool>(response);
            return reply;
        }

        public async Task<bool> DeleteAsync(string key, Action<KeyValueDelOptions> opts = null)
        {
            if (key == null) throw new ArgumentNullException("key");

            string uri = Uri.EscapeUriString(String.Format("v1/kv/{0}", key));
            var options = AssignOptions(opts);
            AppendQueryParameters(ref uri, options);
            
            HttpResponseMessage response = await _client.DeleteAsync(uri);

            bool reply = await ReadContentAsync<bool>(response);
            return reply;
        }

        async Task<KeyValueResponse<TContent>> GetAsync<TContent>(string key, KeyValueOptions options = null)
        {
            if (key == null) throw new ArgumentNullException("key");

            string uri = Uri.EscapeUriString(String.Format("v1/kv/{0}", key));
            AppendQueryParameters(ref uri, options);

            HttpResponseMessage response = await _client.GetAsync(uri);

            TContent content = await ReadContentAsync<TContent>(response);
            var reply = new KeyValueResponse<TContent>(response, content);
            return reply;
        }

        T AssignOptions<T>(Action<T> fn) where T : class, new()
        {
            T options = null;
            if (fn != null)
            {
                options = new T();
                fn(options);
            }
            return options;
        }
    }
}