using System;
using System.Net.Http;

namespace Wildflower.Consul
{
    public class ConsulClient : IDisposable
    {
        bool _disposed;
        readonly HttpClient _client;
        readonly Lazy<KeyValueOperations> _storage;

        public ConsulClient()
            : this(new Uri("http://localhost:8500/"))
        {
            
        }

        public ConsulClient(Uri address)
        {
            _client = new HttpClient {BaseAddress = address};

            _storage = new Lazy<KeyValueOperations>(() => new KeyValueOperations(_client)); 
        }

        public KeyValueOperations KeyValue
        {
            get { return _storage.Value; }
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
