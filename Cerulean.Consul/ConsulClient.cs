using System;
using System.Net.Http;
using Cerulean.Consul.Agent;
using Cerulean.Consul.Catalog;
using Cerulean.Consul.KeyValueStore;

namespace Cerulean.Consul
{
    public class ConsulClient : IDisposable
    {
        bool _disposed;
        readonly HttpClient _client;
        readonly Lazy<KeyValueOperations> _storage;
        readonly Lazy<CatalogOperations> _catalog;
        readonly Lazy<AgentOperations> _agent;
        readonly DefaultParameters _defaults = new DefaultParameters();

        public ConsulClient()
            : this(new Uri("http://localhost:8500/"))
        {
            
        }

        public ConsulClient(Uri address)
        {
            _client = new HttpClient {BaseAddress = address};

            _storage = new Lazy<KeyValueOperations>(() => new KeyValueOperations(_client, _defaults));
            _catalog = new Lazy<CatalogOperations>(() => new CatalogOperations(_client, _defaults));
            _agent = new Lazy<AgentOperations>(() => new AgentOperations(_client, _defaults));
        }

        public DefaultParameters Default
        {
            get { return _defaults; }
        }

        public KeyValueOperations KeyValue
        {
            get { return _storage.Value; }
        }

        public CatalogOperations Catalog
        {
            get { return _catalog.Value; }
        }

        public AgentOperations Agent
        {
            get { return _agent.Value; }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ConsulClient()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // free other managed objects that implement
                    // IDisposable only
                    _client.Dispose();
                }

                // release any unmanaged objects
                // set the object references to null

                _disposed = true;
            }
        }
    }
}
