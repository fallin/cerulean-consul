using System;
using System.Runtime.Serialization;

namespace Cerulean.Consul.Agent
{
    public enum WatchType
    {
        [EnumMember(Value = "key")] 
        Key,

        [EnumMember(Value = "keyprefix")]
        KeyPrefix,

        [EnumMember(Value = "services")]
        Services,

        [EnumMember(Value = "nodes")]
        Nodes,

        [EnumMember(Value = "service")]
        Service,

        [EnumMember(Value = "checks")]
        Checks,

        [EnumMember(Value = "event")]
        Event
    }
}