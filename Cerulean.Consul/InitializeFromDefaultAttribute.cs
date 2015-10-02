using System;

namespace Cerulean.Consul
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    sealed class InitializeFromDefaultAttribute : Attribute
    {
        public InitializeFromDefaultAttribute(string parameterName)
        {
            ParameterName = parameterName;
        }

        public string ParameterName { get; private set; }
    }
}