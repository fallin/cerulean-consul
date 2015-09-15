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
        var checks = await client.Agent.GetChecksAsync();
        checks.Dump("checks");
        
        var services = await client.Agent.GetServicesAsync();
        services.Dump("services");
        
        var members = await client.Agent.GetMembersAsync();
        members.Dump("members");
        
        var localAgent = await client.Agent.GetSelfAsync();
        localAgent.Dump("local agent config");
    }
}