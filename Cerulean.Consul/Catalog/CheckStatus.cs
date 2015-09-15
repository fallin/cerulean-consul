using System;
using System.Runtime.Serialization;

namespace Cerulean.Consul.Catalog
{
    public enum CheckStatus
    {
        [EnumMember(Value = "unknown")]
        Unknown,
        
        [EnumMember(Value = "passing")]
        Passing,
        
        [EnumMember(Value = "warning")]
        Warning,
        
        [EnumMember(Value = "critical")]
        Critical
    }
}