using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Cerulean.Consul.WebExtensions;

namespace Cerulean.Consul.KeyValueStore
{
    public class KeyValueOperations : ServiceOperations
    {
        readonly HashSet<string> _useGlobals; 

        internal KeyValueOperations(HttpClient client, GlobalParameters globals)
            : base(client, globals)
        {
            _useGlobals = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "dc", "token" };
        }

        public Task<KeyValueResponse<KeyValue[]>> GetAllAsync()
        {
            return GetAsync(string.Empty, opt => opt.Recurse());
        }

        public async Task<KeyValueResponse<KeyValue[]>> GetAsync(string key, Action<KeyValueGetParameters> config = null)
        {
            var parameters = ConfigureParameters(config, _useGlobals);
            string uri = ConstructUri(parameters, "v1/kv/{0}", key);

            HttpResponseMessage response = await Client.GetAsync(uri);
            var content = await response.ReadContentAsync<KeyValue[]>();
            var reply = new KeyValueResponse<KeyValue[]>(response, content);
            return reply;
        }

        // NOTE: returns string or string[] when recurse is set
        public async Task<KeyValueResponse<dynamic>> GetRawAsync(string key, Action<KeyValueGetRawParameters> config = null)
        {
            var parameters = ConfigureParameters(config, _useGlobals);
            string uri = ConstructUri(parameters, "v1/kv/{0}", key);

            HttpResponseMessage response = await Client.GetAsync(uri);
            var content = await response.ReadContentAsync<dynamic>();
            var reply = new KeyValueResponse<dynamic>(response, content);
            return reply;
        }

        public async Task<KeyValueResponse<string[]>> GetKeysAsync(string key, Action<KeyValueGetKeysParameters> config = null)
        {
            var parameters = ConfigureParameters(config, _useGlobals);
            string uri = ConstructUri(parameters, "v1/kv/{0}", key);

            HttpResponseMessage response = await Client.GetAsync(uri);
            var content = await response.ReadContentAsync<string[]>();
            var reply = new KeyValueResponse<string[]>(response, content);
            return reply;
        }

        internal async Task<KeyValueResponse<dynamic>> GetDynamicAsync(string key, Action<Parameters> config = null)
        {
            // The method signature changed. The dynamic 'query' parameter has been replaced by Action<Parameters>
            // because the Parameter class is flexible enough to allow any query parameters (by calling the Add(_)
            // or Add(_,_) methods). This method isn't really as useful as it used to be since it's functionally
            // equivalent to GetRawAsync.

            if (key == null) throw new ArgumentNullException("key");

            var parameters = ConfigureParameters(config, _useGlobals);
            string uri = ConstructUri(parameters, "v1/kv/{0}", key);

            HttpResponseMessage response = await Client.GetAsync(uri);
            dynamic content = await response.ReadContentAsync<dynamic>();
            var reply = new KeyValueResponse<dynamic>(response, content);
            return reply;
        }

        public async Task<bool> PutAsync(string key, string value, Action<KeyValuePutParameters> config = null)
        {
            if (key == null) throw new ArgumentNullException("key");

            var parameters = ConfigureParameters(config, _useGlobals);
            string uri = ConstructUri(parameters, "v1/kv/{0}", key);

            HttpResponseMessage response = await Client.PutAsync(uri, new StringContent(value));
            bool reply = await response.ReadContentAsync<bool>();
            return reply;
        }

        public async Task<bool> DeleteAsync(string key, Action<KeyValueDelParameters> config = null)
        {
            if (key == null) throw new ArgumentNullException("key");

            var parameters = ConfigureParameters(config, _useGlobals);
            string uri = ConstructUri(parameters, "v1/kv/{0}", key);

            HttpResponseMessage response = await Client.DeleteAsync(uri);
            bool reply = await response.ReadContentAsync<bool>();
            return reply;
        }
    }
}