using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Wildflower.Consul
{
    public class StorageOperations
    {
        readonly HttpClient _client;

        internal StorageOperations(HttpClient client)
        {
            _client = client;
        }

        public async Task<ConsulResponse<IList<StorageValue>>> GetAllAsync()
        {
            HttpResponseMessage response = await _client.GetAsync("v1/kv/?recurse");
            response.EnsureSuccessStatusCode();

            var result = new ConsulResponse<IList<StorageValue>>(response);
            return result;
        }
    }
}