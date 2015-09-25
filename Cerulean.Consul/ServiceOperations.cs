using System;
using System.Net.Http;

namespace Cerulean.Consul
{
    public abstract class ServiceOperations
    {
        readonly HttpClient _client;

        protected ServiceOperations(HttpClient client)
        {
            if (client == null) throw new ArgumentNullException("client");

            _client = client;
        }

        protected HttpClient Client
        {
            get { return _client; }
        }

        protected string ConstructUri(Parameters parameters, string uri)
        {
            if (uri == null) throw new ArgumentNullException("uri");

            uri = Uri.EscapeUriString(uri);
            AppendQueryParameters(ref uri, parameters);

            return uri;
        }

        protected string ConstructUri(Parameters parameters, string uri, params object[] args)
        {
            if (uri == null) throw new ArgumentNullException("uri");

            uri = Uri.EscapeUriString(string.Format(uri, args));
            AppendQueryParameters(ref uri, parameters);

            return uri;
        }

        protected void AppendQueryParameters(ref string uri, Parameters parameters)
        {
            if (parameters != null)
            {
                string queryString = parameters.ToQueryString();
                if (!string.IsNullOrEmpty(queryString))
                {
                    uri += "?" + queryString;
                }
            }
        }

        protected T ConfigureParameters<T>(Action<T> fn, T parameters) where T : Parameters
        {
            if (fn != null)
            {
                fn(parameters);
            }
            return parameters;
        }
    }
}