using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Wildflower.Consul
{
    public class KeyValueOperations
    {
        readonly HttpClient _client;

        internal KeyValueOperations(HttpClient client)
        {
            _client = client;
        }

        public async Task<KeyValueResponse> GetAllAsync()
        {
            string uri = "v1/kv/?recurse";
            HttpResponseMessage response = await _client.GetAsync(uri);
            KeyValueResponse reply = new KeyValueResponse(response);
            
            return reply;
        }

        public async Task<KeyValueResponse> GetAsync(string key)
        {
            string uri = Uri.EscapeUriString(String.Format("v1/kv/{0}", key));
            HttpResponseMessage response = await _client.GetAsync(uri);
            KeyValueResponse reply = new KeyValueResponse(response);

            return reply;
        }

        public async Task<bool> PutAsync(string key, string value)
        {
            string uri = Uri.EscapeUriString(String.Format("v1/kv/{0}", key));
            HttpContent content = new StringContent(value);
            HttpResponseMessage response = await _client.PutAsync(uri, content);

            ConsulResponse<bool> reply = new ConsulResponse<bool>(response);
            bool result = await reply.ContentAsync();

            return result;
        }

        public async Task<bool> PutAsync(string key, string value, int flags)
        {
            string uri = Uri.EscapeUriString(String.Format("v1/kv/{0}?flags={1}", key, flags));
            HttpContent content = new StringContent(value);
            HttpResponseMessage response = await _client.PutAsync(uri, content);

            ConsulResponse<bool> reply = new ConsulResponse<bool>(response);
            bool result = await reply.ContentAsync();

            return result;
        }
    }
}