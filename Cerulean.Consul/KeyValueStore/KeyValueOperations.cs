using System;
using System.Net.Http;
using System.Threading.Tasks;
using Cerulean.Consul.WebExtensions;
using Newtonsoft.Json.Linq;

namespace Cerulean.Consul.KeyValueStore
{
    public class KeyValueOperations : ServiceOperations
    {
        internal KeyValueOperations(HttpClient client) : base(client)
        {
        }

        public Task<KeyValueResponse<KeyValue[]>> GetAllAsync()
        {
            return GetAsync(string.Empty, opt => opt.Recurse = true);
        }

        public async Task<KeyValueResponse<KeyValue[]>> GetAsync(string key, Action<KeyValueGetParameters> parameters = null)
        {
            string uri = ConstructUri(parameters, "v1/kv/{0}", key);

            HttpResponseMessage response = await Client.GetAsync(uri);
            var content = await response.ReadContentAsync<KeyValue[]>();
            var reply = new KeyValueResponse<KeyValue[]>(response, content);
            return reply;
        }

        public async Task<KeyValueResponse<string>> GetRawAsync(string key, Action<KeyValueGetRawParameters> parameters = null)
        {
            string uri = ConstructUri(parameters, "v1/kv/{0}", key);

            HttpResponseMessage response = await Client.GetAsync(uri);
            var content = await response.ReadContentAsync<string>();
            var reply = new KeyValueResponse<string>(response, content);
            return reply;
        }

        public async Task<KeyValueResponse<string[]>> GetKeysAsync(string key, Action<KeyValueGetKeysParameters> parameters = null)
        {
            string uri = ConstructUri(parameters, "v1/kv/{0}", key);

            HttpResponseMessage response = await Client.GetAsync(uri);
            var content = await response.ReadContentAsync<string[]>();
            var reply = new KeyValueResponse<string[]>(response, content);
            return reply;
        }

        public async Task<KeyValueResponse<dynamic>> GetDynamicAsync(string key, dynamic parameters = null)
        {
            if (key == null) throw new ArgumentNullException("key");

            string uri = Uri.EscapeUriString(String.Format("v1/kv/{0}", key));

            JObject jo = JObject.FromObject(parameters);
            JObjectQueryBuilder queryBuilder = new JObjectQueryBuilder(jo);
            AppendQueryParameters(ref uri, queryBuilder);

            HttpResponseMessage response = await Client.GetAsync(uri);
            dynamic content = await response.ReadContentAsync<dynamic>();
            var reply = new KeyValueResponse<dynamic>(response, content);
            return reply;
        }

        public async Task<bool> PutAsync(string key, string value, Action<KeyValuePutParameters> parameters = null)
        {
            if (key == null) throw new ArgumentNullException("key");

            string uri = ConstructUri(parameters, "v1/kv/{0}", key);

            HttpResponseMessage response = await Client.PutAsync(uri, new StringContent(value));
            bool reply = await response.ReadContentAsync<bool>();
            return reply;
        }

        public async Task<bool> DeleteAsync(string key, Action<KeyValueDelParameters> parameters = null)
        {
            if (key == null) throw new ArgumentNullException("key");

            string uri = ConstructUri(parameters, "v1/kv/{0}", key);

            HttpResponseMessage response = await Client.DeleteAsync(uri);
            bool reply = await response.ReadContentAsync<bool>();
            return reply;
        }
    }
}