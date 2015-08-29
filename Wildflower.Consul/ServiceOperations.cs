using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Wildflower.Consul
{
    public abstract class ServiceOperations
    {
        protected async Task<T> ReadContentAsync<T>(HttpResponseMessage response)
        {
            if (response == null) throw new ArgumentNullException("response");

            T content;
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                content = CreateDefaultContent<T>();
            }
            else
            {
                response.EnsureSuccessStatusCode();
                
                JsonSerializer serializer = JsonSerializer.CreateDefault();
                Stream stream = await response.Content.ReadAsStreamAsync();
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    using (JsonTextReader jsonReader = new JsonTextReader(streamReader))
                    {
                        content = serializer.Deserialize<T>(jsonReader);
                    }
                }
            }

            return content;
        }

        static T CreateDefaultContent<T>()
        {
            T t;
            Type contentType = typeof(T);
            if (contentType.IsArray)
            {
                Type elementType = contentType.GetElementType();
                Array array = Array.CreateInstance(elementType, 0);
                t = (T)(object)array;
            }
            else if (contentType.IsGenericType)
            {
                Type elementType = contentType.GetGenericArguments()[0];
                Array array = Array.CreateInstance(elementType, 0);
                t = (T)(object)array;
            }
            else
            {
                t = default(T);
            }

            return t;
        }
    }
}