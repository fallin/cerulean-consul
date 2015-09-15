<Query Kind="Program">
  <Reference Relative="..\..\..\Cerulean.Consul\bin\Debug\Cerulean.Consul.dll">D:\Code\cerulean-consul\Cerulean.Consul\bin\Debug\Cerulean.Consul.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Numerics.dll</Reference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Cerulean.Consul</Namespace>
  <Namespace>Cerulean.Consul.Catalog</Namespace>
  <Namespace>Cerulean.Consul.KeyValueStore</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>System.Numerics</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
    MainAsync().Wait();
}

async Task MainAsync()
{
    using (var client = new ConsulClient())
    {
        var datacenters = await client.Catalog.GetDataCentersAsync();
        datacenters.Dump("data centers");
        
        var nodes = await client.Catalog.GetNodesAsync();
        nodes.Dump("registered nodes");
        
        var services = await client.Catalog.GetServicesAsync();
        services.Dump("registered services");
        
        var serviceNodes = await client.Catalog.GetNodesProvidingServiceAsync("web");
        serviceNodes.Dump("nodes providing service: web");
        
        string node = Environment.MachineName;
        var nodeServices = await client.Catalog.GetNodeServicesAsync(node);
        nodeServices.Dump("services provided by node");
    }
}