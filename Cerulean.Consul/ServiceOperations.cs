using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Cerulean.Consul
{
    public abstract class ServiceOperations
    {
        readonly HttpClient _client;
        readonly GlobalParameters _globals;

        protected ServiceOperations(HttpClient client, GlobalParameters globals)
        {
            if (client == null) throw new ArgumentNullException("client");

            _client = client;
            _globals = globals ?? new GlobalParameters();
        }

        protected HttpClient Client
        {
            get { return _client; }
        }

        protected HttpContent EmptyContent()
        {
            return new StringContent(string.Empty);
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

        protected T ConfigureParameters<T>(Action<T> fn) where T : Parameters, new()
        {
            T parameters = new T();

            HashSet<string> useGlobals = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            InitializeWithGlobals(parameters, useGlobals);
            if (fn != null)
            {
                fn(parameters);
            }

            return parameters;
        }

        void AppendQueryParameters(ref string uri, Parameters parameters)
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

        void InitializeWithGlobals(Parameters parameters, HashSet<string> useGlobals)
        {
            if (parameters == null) throw new ArgumentNullException("parameters");

            if (useGlobals != null && useGlobals.Any())
            {
                IEnumerable<KeyValuePair<string, object>> overrides = _globals
                    .Where(pair => useGlobals.Contains(pair.Key))
                    .ToArray();
                if (overrides.Any())
                {
                    foreach (KeyValuePair<string, object> @override in overrides)
                    {
                        parameters.Add(@override.Key, @override.Value);
                    }
                }
            }
        }
    }
}