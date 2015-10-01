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
  <Namespace>Cerulean.Consul.Agent</Namespace>
</Query>

void Main()
{
    MainAsync().Wait();
}

async Task MainAsync()
{
    using (var client = new ConsulClient())
    {
        Console.WriteLine ("enter maintenance mode");
        await client.Agent.MaintenanceAsync(true, c => c.Reason("test"));
        
        Console.WriteLine ("leave maintenance mode");
        await client.Agent.MaintenanceAsync(false);
        
        Console.WriteLine ("join");
        await client.Agent.JoinAsync("127.0.0.1");
        
        Console.WriteLine ("force leave");
        await client.Agent.ForceLeaveAsync("node1");
        
        Console.WriteLine ("register check");
        await client.Agent.RegisterCheckAsync(new HttpCheckRegistar {
           Name = "web-check",
           Http = "http://localhost:8000/health",
           Interval = "10s"
        });
        
        Console.WriteLine ("deregister check");
        await client.Agent.DeregisterCheckAsync("web-check");
        
        Console.WriteLine ("register service");
        await client.Agent.RegisterServiceAsync(new ServiceRegistrar {
            ID = "redis1",
            Name = "redis",
            Tags = { "master", "v1" },
            Address = "127.0.0.1",
            Port = 8000,
            Check = new TtlCheckRegistrar {
                Ttl = "15s",
            }
        });
        
        Console.WriteLine ("signal TTL check pass");
        await client.Agent.TtlCheckPassAsync("service:redis1");
        
        Console.WriteLine ("deregister service");
        await client.Agent.DeregisterServiceAsync("redis1");
    }
}