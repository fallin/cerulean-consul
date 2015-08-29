using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Wildflower.Consul
{
    public class ConsulResponse<TContent> : IDisposable
    {
        bool _disposed;
        readonly HttpResponseMessage _responseMessage;

        public ConsulResponse(HttpResponseMessage responseMessage)
        {
            if (responseMessage == null) throw new ArgumentNullException("responseMessage");

            _responseMessage = responseMessage;
        }

        public async Task<TContent> ContentAsync()
        {
            TContent content;

            if (_responseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                Type contentType = typeof (TContent);
                if (contentType.IsArray)
                {
                    Type elementType = contentType.GetElementType();
                    Array array = Array.CreateInstance(elementType, 0);
                    content = (TContent)(object)array;
                }
                else if (contentType.IsGenericType)
                {
                    Type elementType = contentType.GetGenericArguments()[0];
                    Array array = Array.CreateInstance(elementType, 0);
                    content = (TContent)(object)array;
                }
                else
                {
                    content = default(TContent);
                }
            }
            else
            {
                _responseMessage.EnsureSuccessStatusCode();
                content = await ReadAsJsonAsync<TContent>(_responseMessage.Content);
            }

            return content;
        }

        protected HttpResponseMessage ResponseMessage
        {
            get { return _responseMessage; }
        }

        static async Task<T> ReadAsJsonAsync<T>(HttpContent content)
        {
            if (content == null) throw new ArgumentNullException("content");

            T deserializedContent;
            JsonSerializer serializer = JsonSerializer.CreateDefault();

            Stream stream = await content.ReadAsStreamAsync();
            using (StreamReader streamReader = new StreamReader(stream))
            {
                using (JsonTextReader jsonReader = new JsonTextReader(streamReader))
                {
                    deserializedContent = serializer.Deserialize<T>(jsonReader);
                }
            }

            return deserializedContent;
        }

        #region Disposable Implementation

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ConsulResponse()
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
                    _responseMessage.Dispose();
                }

                // release any unmanaged objects
                // set the object references to null

                _disposed = true;
            }
        }

        #endregion
    }
}