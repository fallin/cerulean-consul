using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Cerulean.Consul
{
    public class JObjectParameters : Parameters
    {
        public JObjectParameters(JObject jo)
        {
            if (jo != null)
            {
                IEnumerable<JProperty> properties = jo.Properties();
                foreach (JProperty property in properties)
                {
                    Add(property.Name, property.Value);
                }
            }
        }
    }
}