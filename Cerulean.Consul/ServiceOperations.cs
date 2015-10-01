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
        readonly HashSet<string> _applicableGlobals = new HashSet<string>(StringComparer.OrdinalIgnoreCase); 

        protected ServiceOperations(HttpClient client, GlobalParameters globals)
        {
            if (client == null) throw new ArgumentNullException("client");

            _client = client;
            _globals = globals ?? new GlobalParameters();
        }

        protected void UseGlobalParameters(params string[] globals)
        {
            foreach (string global in globals)
            {
                _applicableGlobals.Add(global);
            }
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

            // Parameters provided by the callback function (fn) should always take
            // precedence over global parameters. Also, not all global parameters
            // apply to all endpoints. Therefore, we should only provide global
            // parameters if (1) they are applicable to the endpoint and (2) they
            // are missing from the caller-specified parameter list.
            ConfigureGlobalParameters(parameters);

            return parameters;
        }

        void ConfigureGlobalParameters<T>(T parameters) where T : Parameters
        {
            IEnumerable<KeyValuePair<string, object>> globals = _globals
                .Where(pair => _applicableGlobals.Contains(pair.Key))
                .ToArray();
            if (globals.Any())
            {
                foreach (KeyValuePair<string, object> global in globals.Where(g => parameters.IsMissing(g.Key)))
                {
                    parameters.Add(global.Key, global.Value);
                }
            }
        }

        protected HttpContent EmptyContent()
        {
            return new StringContent(string.Empty);
        }
    }
}