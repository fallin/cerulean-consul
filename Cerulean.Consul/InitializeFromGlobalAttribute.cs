using System;

namespace Cerulean.Consul
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    sealed class InitializeFromGlobalAttribute : Attribute
    {
        public InitializeFromGlobalAttribute(string parameterName)
        {
            ParameterName = parameterName;
        }

        public string ParameterName { get; private set; }
    }
}