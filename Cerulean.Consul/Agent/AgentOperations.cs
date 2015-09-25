using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Cerulean.Consul.Catalog;
using Cerulean.Consul.WebExtensions;

namespace Cerulean.Consul.Agent
{
    public class AgentOperations : ServiceOperations
    {
        internal AgentOperations(HttpClient client) : base(client)
        {
        }

        /// <summary>
        /// Get checks registered with the local agent.
        /// </summary>
        public async Task<IDictionary<string, AgentCheck>> GetChecksAsync()
        {
            string uri = ConstructUri(null, "v1/agent/checks");

            HttpResponseMessage response = await Client.GetAsync(uri);
            var reply = await response.ReadContentAsync<IDictionary<string, AgentCheck>>();
            return reply;
        }

        /// <summary>
        /// Get services registered with the local agent.
        /// </summary>
        public async Task<IDictionary<string, ServiceDescriptor>> GetServicesAsync()
        {
            string uri = ConstructUri(null, "v1/agent/services");

            HttpResponseMessage response = await Client.GetAsync(uri);
            var reply = await response.ReadContentAsync<IDictionary<string, ServiceDescriptor>>();
            return reply;
        }

        /// <summary>
        /// Get members the agent sees in the cluster gossip pool.
        /// </summary>
        public async Task<AgentMember[]> GetMembersAsync()
        {
            string uri = ConstructUri(null, "v1/agent/members");

            HttpResponseMessage response = await Client.GetAsync(uri);
            var reply = await response.ReadContentAsync<AgentMember[]>();
            return reply;
        }

        /// <summary>
        /// Get members the agent sees in the cluster gossip pool.
        /// </summary>
        public async Task<LocalAgent> GetSelfAsync()
        {
            string uri = ConstructUri(null, "v1/agent/self");

            HttpResponseMessage response = await Client.GetAsync(uri);
            var reply = await response.ReadContentAsync<LocalAgent>();
            return reply;
        }
    }
}