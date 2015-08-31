using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Wildflower.Consul
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
            return GetAsync(string.Empty, new KeyValueGetOptions {Recurse = true});
        }

        public async Task<KeyValueResponse<IList<KeyValue>>> GetAsync(string key, KeyValueGetOptions options = null)
        {
            return await GetAsync<IList<KeyValue>>(key, options);
        }

        public async Task<KeyValueResponse<string>> GetRawAsync(string key, KeyValueGetRawOptions options = null)
        {
            return await GetAsync<string>(key, options);
        }

        public async Task<KeyValueResponse<IList<string>>> GetKeysAsync(string key, KeyValueGetKeysOptions options = null)
        {
            return await GetAsync<IList<string>>(key, options);
        }

        async Task<KeyValueResponse<TContent>> GetAsync<TContent>(string key, Options options = null)
        {
            if (key == null) throw new ArgumentNullException("key");

            string uri = Uri.EscapeUriString(String.Format("v1/kv/{0}", key));
            AppendQueryParameters(ref uri, options);

            HttpResponseMessage response = await _client.GetAsync(uri);

            TContent content = await ReadContentAsync<TContent>(response);
            var reply = new KeyValueResponse<TContent>(response, content);
            return reply;
        }

        public async Task<bool> PutAsync(string key, string value, KeyValuePutOptions options = null)
        {
            if (key == null) throw new ArgumentNullException("key");

            string uri = Uri.EscapeUriString(String.Format("v1/kv/{0}", key));
            AppendQueryParameters(ref uri, options);

            HttpContent content = new StringContent(value);
            HttpResponseMessage response = await _client.PutAsync(uri, content);

            bool reply = await ReadContentAsync<bool>(response);
            return reply;
        }

        public async Task<bool> DeleteAsync(string key, KeyValueDelOptions options = null)
        {
            if (key == null) throw new ArgumentNullException("key");

            string uri = Uri.EscapeUriString(String.Format("v1/kv/{0}", key));
            AppendQueryParameters(ref uri, options);
            
            HttpResponseMessage response = await _client.DeleteAsync(uri);

            bool reply = await ReadContentAsync<bool>(response);
            return reply;
        }
    }
}