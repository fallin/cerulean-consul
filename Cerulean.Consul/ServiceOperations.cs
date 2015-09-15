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

        protected string ConstructUri<TParameters>(Action<TParameters> parameters, string uri) where TParameters : IQueryBuilder, new()
        {
            if (uri == null) throw new ArgumentNullException("uri");

            uri = Uri.EscapeUriString(uri);
            AppendQueryParameters(ref uri, CollectParameters(parameters));

            return uri;
        }

        protected string ConstructUri<TParameters>(Action<TParameters> parameters, string uri, params object[] args) where TParameters : IQueryBuilder, new()
        {
            if (uri == null) throw new ArgumentNullException("uri");

            uri = Uri.EscapeUriString(string.Format(uri, args));
            AppendQueryParameters(ref uri, CollectParameters(parameters));

            return uri;
        }

        protected void AppendQueryParameters(ref string uri, IQueryBuilder queryBuilder)
        {
            if (queryBuilder != null)
            {
                Query query = new Query();
                queryBuilder.BuildQuery(query);

                string queryString = query.ToQueryString();
                if (!string.IsNullOrEmpty(queryString))
                {
                    uri += "?" + queryString;
                }
            }
        }

        protected T CollectParameters<T>(Action<T> fn) where T : IQueryBuilder, new()
        {
            T options = default(T);
            if (fn != null)
            {
                options = new T();
                fn(options);
            }
            return options;
        }
    }
}