using System;
using System.Linq;
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

        public async Task<KeyValueResponse<KeyValueResource[]>> GetAllAsync()
        {
            string uri = "v1/kv/?recurse";
            HttpResponseMessage response = await _client.GetAsync(uri);

            KeyValueResource[] content = await ReadContentAsync<KeyValueResource[]>(response);
            return new KeyValueResponse<KeyValueResource[]>(response, content);
        }

        public async Task<KeyValueResponse<KeyValueResource>> GetAsync(string key)
        {
            string uri = Uri.EscapeUriString(String.Format("v1/kv/{0}", key));
            HttpResponseMessage response = await _client.GetAsync(uri);

            KeyValueResource[] content = await ReadContentAsync<KeyValueResource[]>(response);
            return new KeyValueResponse<KeyValueResource>(response, content.FirstOrDefault());
        }

        public async Task<bool> PutAsync(string key, string value)
        {
            string uri = Uri.EscapeUriString(String.Format("v1/kv/{0}", key));
            HttpContent content = new StringContent(value);
            HttpResponseMessage response = await _client.PutAsync(uri, content);

            bool reply = await ReadContentAsync<bool>(response);
            return reply;
        }

        public async Task<bool> PutAsync(string key, string value, int flags)
        {
            string uri = Uri.EscapeUriString(String.Format("v1/kv/{0}?flags={1}", key, flags));
            HttpContent content = new StringContent(value);
            HttpResponseMessage response = await _client.PutAsync(uri, content);

            bool reply = await ReadContentAsync<bool>(response);
            return reply;
        }
    }
}