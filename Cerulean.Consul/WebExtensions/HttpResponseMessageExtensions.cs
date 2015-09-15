using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Cerulean.Consul.WebExtensions
{
    static class HttpResponseMessageExtensions
    {
        public static string GetHeaderValue(this HttpResponseMessage response, string name)
        {
            string headerValue = null;
            IEnumerable<string> values;
            if (response.Headers.TryGetValues(name, out values))
            {
                headerValue = values.FirstOrDefault();
            }

            return headerValue;
        }

        public static T GetHeaderValueAs<T>(this HttpResponseMessage response, string name)
        {
            T coercedValue = default(T);

            string headerValue = response.GetHeaderValue(name);
            if (headerValue != null)
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
                coercedValue = (T)converter.ConvertFromString(headerValue);
            }

            return coercedValue;
        }

        public static async Task<T> ReadContentAsync<T>(this HttpResponseMessage response)
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

                string mediaType = response.Content.Headers.ContentType.MediaType;
                if (mediaType == "application/json")
                {
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
                else if (mediaType == "text/plain")
                {
                    Stream stream = await response.Content.ReadAsStreamAsync();
                    using (StreamReader streamReader = new StreamReader(stream))
                    {
                        string text = streamReader.ReadToEnd();

                        if (typeof(T) == typeof(string) || typeof(T).IsAssignableFrom(typeof(IDynamicMetaObjectProvider)))
                        {
                            content = (T)(object)text;
                        }
                        else
                        {
                            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
                            content = (T)converter.ConvertFromString(text);
                        }
                    }
                }
                else
                {
                    throw new NotSupportedException(String.Format("Unsupported media type: {0}", mediaType));
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