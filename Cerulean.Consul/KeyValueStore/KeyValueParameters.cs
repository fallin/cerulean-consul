﻿using System;

namespace Cerulean.Consul.KeyValueStore
{
    public abstract class KeyValueParameters : Parameters
    {
        public void Datacenter(string dc)
        {
            if (!string.IsNullOrEmpty(dc))
            {
                Add("dc", dc);
            }
        }
    }
}