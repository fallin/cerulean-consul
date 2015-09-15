﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Cerulean.Consul.WebExtensions;

namespace Cerulean.Consul.Catalog
{
    public class CatalogOperations : ServiceOperations
    {
        internal CatalogOperations(HttpClient client) : base(client)
        {
        }

        /// <summary>
        /// Get all data centers (DCs) 
        /// </summary>
        public async Task<string[]> GetDataCentersAsync()
        {
            string uri = "v1/catalog/datacenters";

            HttpResponseMessage response = await Client.GetAsync(uri);
            var reply = await response.ReadContentAsync<string[]>();
            return reply;
        }

        /// <summary>
        /// Get nodes registered in a given DC.
        /// </summary>
        public async Task<NodeDescriptor[]> GetNodesAsync(Action<GetNodesParameters> parameters = null)
        {
            string uri = ConstructUri(parameters, "v1/catalog/nodes");

            HttpResponseMessage response = await Client.GetAsync(uri);
            var reply = await response.ReadContentAsync<NodeDescriptor[]>();
            return reply;
        }

        /// <summary>
        /// Get the services registered in a given DC.
        /// </summary>
        public async Task<IDictionary<string, ServiceTags>> GetServicesAsync(Action<GetServicesParameters> parameters = null)
        {
            string uri = ConstructUri(parameters, "v1/catalog/services");

            HttpResponseMessage response = await Client.GetAsync(uri);
            var reply = await response.ReadContentAsync<IDictionary<string, ServiceTags>>();
            return reply;
        }

        /// <summary>
        /// Get the nodes providing a service in a given DC.
        /// </summary>
        public async Task<ServiceNode[]> GetNodesProvidingServiceAsync(string service, Action<GetNodesProvidingServiceParameters> parameters = null)
        {
            string uri = ConstructUri(parameters, "v1/catalog/service/{0}", service);

            HttpResponseMessage response = await Client.GetAsync(uri);
            var reply = await response.ReadContentAsync<ServiceNode[]>();
            return reply;
        }

        /// <summary>
        /// Get a node's registered services.
        /// </summary>
        public async Task<NodeServices> GetNodeServicesAsync(string node, Action<GetNodeServicesParameters> parameters = null)
        {
            string uri = ConstructUri(parameters, "v1/catalog/node/{0}", node);

            HttpResponseMessage response = await Client.GetAsync(uri);
            var reply = await response.ReadContentAsync<NodeServices>();
            return reply;
        }
    }
}