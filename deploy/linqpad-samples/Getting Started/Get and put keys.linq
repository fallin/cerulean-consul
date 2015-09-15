<Query Kind="Program">
  <Reference Relative="..\..\..\Cerulean.Consul\bin\Debug\Cerulean.Consul.dll">D:\Code\cerulean-consul\Cerulean.Consul\bin\Debug\Cerulean.Consul.dll</Reference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Cerulean.Consul</Namespace>
  <Namespace>Cerulean.Consul.KeyValueStore</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
    MainAsync().Wait();
}

// Define other methods and classes here
static async Task MainAsync()
{
    using (var client = new ConsulClient())
    {
        bool updated;
    
        updated = await client.KeyValue.PutAsync("web/key1", "test");
        Console.WriteLine("key updated: {0}", updated);
    
        updated = await client.KeyValue.PutAsync("web/key2", "test", p => p.Flags = 42);
        Console.WriteLine("key updated: {0}", updated);
    
        updated = await client.KeyValue.PutAsync("web/sub/key3", "test");
        Console.WriteLine("key updated: {0}", updated);
        
        (await client.KeyValue.GetAllAsync()).Dump();

        KeyValueResponse<KeyValue[]> response = await client.KeyValue.GetAsync("web/key1");
        long index = response.Index;
        
        updated = await client.KeyValue.PutAsync("web/key1", "hello", p => p.CheckAndSet = index);
        Console.WriteLine("key updated: {0} with modify-index: {1}", updated, index);
        
        updated = await client.KeyValue.PutAsync("web/key1", "hello", p => p.CheckAndSet = index);
        Console.WriteLine("key updated: {0} with modify-index: {1}", updated, index);
    }
}