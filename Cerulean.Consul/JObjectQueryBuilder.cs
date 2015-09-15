using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Cerulean.Consul
{
    public class JObjectQueryBuilder : IQueryBuilder
    {
        readonly JObject _jo;

        public JObjectQueryBuilder(JObject jo)
        {
            _jo = jo;
        }

        public void BuildQuery(Query query)
        {
            if (_jo != null)
            {
                IEnumerable<JProperty> properties = _jo.Properties();
                foreach (JProperty property in properties)
                {
                    query.Add(property.Name, property.Value);
                }
            }
        }
    }
}