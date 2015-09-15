using System;
using System.Runtime.Serialization;

namespace Cerulean.Consul.Agent
{
    public enum AgentLogLevel
    {
        [EnumMember(Value = "TRACE")]
        Trace,

        [EnumMember(Value = "DEBUG")]
        Debug,

        [EnumMember(Value = "INFO")]
        Info,

        [EnumMember(Value = "WARN")]
        Warn,

        [EnumMember(Value = "ERR")]
        Error
    }
}