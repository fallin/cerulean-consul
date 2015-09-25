using System;

namespace Cerulean.Consul.Tests
{
    class TestableParameters : Parameters
    {
        public new void Add(string name)
        {
            base.Add(name, null);
        }

        public new void Add(string name, object value)
        {
            base.Add(name, value);
        }
    }
}