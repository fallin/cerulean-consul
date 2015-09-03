using System;
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

        public Task<KeyValueResponse<KeyValue[]>> GetAllAsync()
        {
            return GetAsync(string.Empty, opt => opt.Recurse = true);
        }

        public async Task<KeyValueResponse<KeyValue[]>> GetAsync(string key, Action<KeyValueGetOptions> options = null)
        {
            return await GetAsync<KeyValue[]>(key, AssignOptions(options));
        }

        public async Task<KeyValueResponse<string>> GetRawAsync(string key, Action<KeyValueGetRawOptions> options = null)
        {
            return await GetAsync<string>(key, AssignOptions(options));
        }

        public async Task<KeyValueResponse<string[]>> GetKeysAsync(string key, Action<KeyValueGetKeysOptions> options = null)
        {
            return await GetAsync<string[]>(key, AssignOptions(options));
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

        public async Task<bool> PutAsync(string key, string value, Action<KeyValuePutOptions> options = null)
        {
            if (key == null) throw new ArgumentNullException("key");

            string uri = Uri.EscapeUriString(String.Format("v1/kv/{0}", key));
            AppendQueryParameters(ref uri, AssignOptions(options));

            HttpContent content = new StringContent(value);
            HttpResponseMessage response = await _client.PutAsync(uri, content);

            bool reply = await ReadContentAsync<bool>(response);
            return reply;
        }

        public async Task<bool> DeleteAsync(string key, Action<KeyValueDelOptions> options = null)
        {
            if (key == null) throw new ArgumentNullException("key");

            string uri = Uri.EscapeUriString(String.Format("v1/kv/{0}", key));
            AppendQueryParameters(ref uri, AssignOptions(options));
            
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