using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;

namespace Cerulean.Consul
{
    public abstract class ServiceOperations
    {
        readonly HttpClient _client;
        readonly DefaultParameters _defaults;

        protected ServiceOperations(HttpClient client, DefaultParameters defaults)
        {
            if (client == null) throw new ArgumentNullException("client");

            _client = client;
            _defaults = defaults ?? new DefaultParameters();
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

            InitializeWithDefaults(parameters);
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

        void InitializeWithDefaults(Parameters parameters)
        {
            if (parameters == null) throw new ArgumentNullException("parameters");

            HashSet<string> initializable = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            BindingFlags binding = BindingFlags.Instance | BindingFlags.Public;
            MethodInfo[] methods = parameters.GetType().GetMethods(binding);
            foreach (MethodInfo method in methods.Where(m => m.ReturnType == typeof(void)))
            {
                var attributes = method.GetCustomAttributes<InitializeFromDefaultAttribute>(true);
                initializable.UnionWith(attributes.Select(a => a.ParameterName));
            }

            if (initializable.Any())
            {
                IEnumerable<KeyValuePair<string, object>> defaults = _defaults
                    .Where(pair => initializable.Contains(pair.Key))
                    .ToArray();
                if (defaults.Any())
                {
                    foreach (KeyValuePair<string, object> @default in defaults)
                    {
                        parameters.Add(@default.Key, @default.Value);
                    }
                }
            }
        }
    }
}