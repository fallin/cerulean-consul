using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Wildflower.Consul
{
    public class ConsulResponse<TContent>
    {
        readonly HttpResponseMessage _response;

        public ConsulResponse(HttpResponseMessage response)
        {
            if (response == null) throw new ArgumentNullException("response");

            _response = response;
        }

        public HttpResponseMessage Response
        {
            get { return _response; }
        }

        public async Task<TContent> Content()
        {
            TContent content;
            JsonSerializer serializer = JsonSerializer.CreateDefault();

            Stream stream = await _response.Content.ReadAsStreamAsync();
            using (StreamReader streamReader = new StreamReader(stream))
            {
                using (JsonTextReader jsonReader = new JsonTextReader(streamReader))
                {
                    content = serializer.Deserialize<TContent>(jsonReader);
                }
            }

            return content;
        }
    }
}