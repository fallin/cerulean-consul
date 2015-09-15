using System;

namespace Cerulean.Consul.Catalog
{
    public class ServiceNode
    {
        public ServiceNode()
        {
            ServiceTags = new ServiceTags();
        }

        public string Node { get; set; }
        public string Address { get; set; }
        public string ServiceID { get; set; }
        public string ServiceName { get; set; }
        public ServiceTags ServiceTags { get; private set; }
        public string ServiceAddress { get; set; }
        public int ServicePort { get; set; }
    }
}