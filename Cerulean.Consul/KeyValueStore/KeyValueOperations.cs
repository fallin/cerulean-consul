using System;
using System.Net.Http;
using System.Threading.Tasks;
using Cerulean.Consul.WebExtensions;
using Newtonsoft.Json.Linq;

namespace Cerulean.Consul.KeyValueStore
{
    public class KeyValueOperations : ServiceOperations
    {
        internal KeyValueOperations(HttpClient client, GlobalParameters globals)
            : base(client, globals)
        {
            UseGlobalParameters("dc", "token");
        }

        public Task<KeyValueResponse<KeyValue[]>> GetAllAsync()
        {
            return GetAsync(string.Empty, opt => opt.Recurse());
        }

        public async Task<KeyValueResponse<KeyValue[]>> GetAsync(string key, Action<KeyValueGetParameters> config = null)
        {
            var parameters = ConfigureParameters(config, new KeyValueGetParameters());
            string uri = ConstructUri(parameters, "v1/kv/{0}", key);

            HttpResponseMessage response = await Client.GetAsync(uri);
            var content = await response.ReadContentAsync<KeyValue[]>();
            var reply = new KeyValueResponse<KeyValue[]>(response, content);
            return reply;
        }

        // NOTE: returns string or string[] when recurse is set
        public async Task<KeyValueResponse<dynamic>> GetRawAsync(string key, Action<KeyValueGetRawParameters> config = null)
        {
            var parameters = ConfigureParameters(config, new KeyValueGetRawParameters());
            string uri = ConstructUri(parameters, "v1/kv/{0}", key);

            HttpResponseMessage response = await Client.GetAsync(uri);
            var content = await response.ReadContentAsync<dynamic>();
            var reply = new KeyValueResponse<dynamic>(response, content);
            return reply;
        }

        public async Task<KeyValueResponse<string[]>> GetKeysAsync(string key, Action<KeyValueGetKeysParameters> config = null)
        {
            var parameters = ConfigureParameters(config, new KeyValueGetKeysParameters());
            string uri = ConstructUri(parameters, "v1/kv/{0}", key);

            HttpResponseMessage response = await Client.GetAsync(uri);
            var content = await response.ReadContentAsync<string[]>();
            var reply = new KeyValueResponse<string[]>(response, content);
            return reply;
        }

        // TODO: is this worth keeping?
        internal async Task<KeyValueResponse<dynamic>> GetDynamicAsync(string key, dynamic query = null)
        {
            if (key == null) throw new ArgumentNullException("key");

            JObject jo = JObject.FromObject(query);
            var parameters = new JObjectParameters(jo);
            string uri = ConstructUri(parameters, "v1/kv/{0}", key);

            HttpResponseMessage response = await Client.GetAsync(uri);
            dynamic content = await response.ReadContentAsync<dynamic>();
            var reply = new KeyValueResponse<dynamic>(response, content);
            return reply;
        }

        public async Task<bool> PutAsync(string key, string value, Action<KeyValuePutParameters> config = null)
        {
            if (key == null) throw new ArgumentNullException("key");

            var parameters = ConfigureParameters(config, new KeyValuePutParameters());
            string uri = ConstructUri(parameters, "v1/kv/{0}", key);

            HttpResponseMessage response = await Client.PutAsync(uri, new StringContent(value));
            bool reply = await response.ReadContentAsync<bool>();
            return reply;
        }

        public async Task<bool> DeleteAsync(string key, Action<KeyValueDelParameters> config = null)
        {
            if (key == null) throw new ArgumentNullException("key");

            var parameters = ConfigureParameters(config, new KeyValueDelParameters());
            string uri = ConstructUri(parameters, "v1/kv/{0}", key);

            HttpResponseMessage response = await Client.DeleteAsync(uri);
            bool reply = await response.ReadContentAsync<bool>();
            return reply;
        }
    }
}