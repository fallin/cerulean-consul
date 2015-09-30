﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Cerulean.Consul.Catalog;
using Cerulean.Consul.WebExtensions;
using Newtonsoft.Json;

namespace Cerulean.Consul.Agent
{
    public class AgentOperations : ServiceOperations
    {
        internal AgentOperations(HttpClient client, GlobalParameters globals)
            : base(client, globals)
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
        public async Task<AgentMember[]> GetMembersAsync(Action<AgentMembersParameters> config = null)
        {
            var parameters = ConfigureParameters(config, new AgentMembersParameters());
            string uri = ConstructUri(parameters, "v1/agent/members");

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

        public async Task AgentMaintenance(bool enable, Action<MaintenanceParameters> config = null)
        {
            var parameters = ConfigureParameters(config, new MaintenanceParameters());
            parameters.Add("enable", enable);

            string uri = ConstructUri(parameters, "v1/agent/maintenance");

            HttpResponseMessage response = await Client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
        }

        public async Task Join(string address, Action<AgentJoinParameters> config = null)
        {
            var parameters = ConfigureParameters(config, new AgentJoinParameters());
            string uri = ConstructUri(parameters, "v1/agent/join/{0}", address);

            HttpResponseMessage response = await Client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
        }

        public async Task ForceLeave(string node)
        {
            string uri = ConstructUri(null, "v1/agent/force-leave/{0}", node);

            HttpResponseMessage response = await Client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
        }

        public async Task RegisterCheck(CheckRegistrar check, Action<AgentRegisterCheckParameters> config = null)
        {
            var parameters = ConfigureParameters(config, new AgentRegisterCheckParameters());
            string uri = ConstructUri(parameters, "v1/agent/check/register");

            HttpContent content = new StringContent(JsonConvert.SerializeObject(check, Formatting.None));
            HttpResponseMessage response = await Client.PutAsync(uri, content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeregisterCheck(string checkID)
        {
            string uri = ConstructUri(null, "v1/agent/check/deregister/{0}", checkID);

            HttpResponseMessage response = await Client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
        }

        public async Task TtlCheckPass(string checkID, Action<AgentTtlCheckParameters> config = null)
        {
            var parameters = ConfigureParameters(config, new AgentTtlCheckParameters());
            string uri = ConstructUri(parameters, "v1/agent/check/pass/{0}", checkID);

            HttpResponseMessage response = await Client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
        }

        public async Task TtlCheckWarn(string checkID, Action<AgentTtlCheckParameters> config = null)
        {
            var parameters = ConfigureParameters(config, new AgentTtlCheckParameters());
            string uri = ConstructUri(parameters, "v1/agent/check/warn/{0}", checkID);

            HttpResponseMessage response = await Client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
        }

        public async Task TtlCheckFail(string checkID, Action<AgentTtlCheckParameters> config = null)
        {
            var parameters = ConfigureParameters(config, new AgentTtlCheckParameters());
            string uri = ConstructUri(parameters, "v1/agent/check/fail/{0}", checkID);

            HttpResponseMessage response = await Client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
        }

        public async Task RegisterService(ServiceRegistrar service, Action<RegisterServiceParameters> config = null)
        {
            var parameters = ConfigureParameters(config, new RegisterServiceParameters());
            string uri = ConstructUri(parameters, "v1/agent/service/register");

            HttpContent content = new StringContent(JsonConvert.SerializeObject(service, Formatting.None));
            HttpResponseMessage response = await Client.PutAsync(uri, content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeregisterService(string serviceID)
        {
            string uri = ConstructUri(null, "v1/agent/service/deregister/{0}", serviceID);

            HttpResponseMessage response = await Client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
        }

        public async Task ServiceMaintenance(string serviceID, bool enable, Action<MaintenanceParameters> config = null)
        {
            var parameters = ConfigureParameters(config, new MaintenanceParameters());
            parameters.Add("enable", enable);

            string uri = ConstructUri(parameters, "v1/agent/service/maintenance/{0}", serviceID);

            HttpResponseMessage response = await Client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
        }
    }
}