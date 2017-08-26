**Note: this repository is no longer actively maintained.**

# cerulean-consul

.NET client API for Consul (https://www.consul.io/)

This library uses HttpClient and [Json.NET](http://www.newtonsoft.com/json) to provide a convenient (and hopefully) easy to use API to access Consul features. It currently only supports the Key-Value API, but that will be improved.

Note, this is a 0.x release, so expect breaking API changes without notice for a while...

The NuGet package, targeting .NET 4.5.1, is available here [Cerulean Consul API](https://www.nuget.org/packages/Cerulean.Consul/)

## Usage
The Consul website provides great documentation. The following are a few examples of performing the same operations as shown in the [Getting Started | Key/Value Data](http://www.consul.io/intro/getting-started/kv.html) documentation.

KeyValue.GetAllAsync() is equivalent to GET "v1/kv/?recurse". Because the key-value store is empty, Consul will return a 404 Not Found which is accessible on the response object.

Instead of returning the key-values (HTTP content) directly, the "Get" methods return KeyValueResponse&lt;KeyValue[]&gt; which provides access the HTTP status code, Consul headers, and key/value content.

```csharp
using (var client = new ConsulClient())
{
    // There's nothing in the key-value store yet...
    var response = await client.KeyValue.GetAllAsync();
    Debug.Assert(response.StatusCode == HttpStatusCode.NotFound);
    Debug.Assert(response.Content.Any() == false);
}
```

Write a few values to the store:

```csharp
using (var client = new ConsulClient())
{
    bool result;

    result = await client.KeyValue.PutAsync("web/key1", "test");
    Console.WriteLine(result);
    
    result = await client.KeyValue.PutAsync("web/key2", "test", o => o.Flags = 42);
    Console.WriteLine(result);
    
    result = await client.KeyValue.PutAsync("web/sub/key3", "test");
    Console.WriteLine(result);
}
```

Read the "key1" key:

```csharp
using (var client = new ConsulClient())
{
    var response = await client.KeyValue.GetAsync("web/sub", o => o.Recurse = true);
    Console.WriteLine(JsonConvert.SerializeObject(response.Content));
}
```

Write values with check-and-set semantics
```csharp
using (var client = new ConsulClient())
{
    var response = await client.KeyValue.GetAsync("web/key1");
    long cas = response.Index;

    bool success1 = await client.KeyValue.PutAsync("web/key1", "newval", o =>
    {
        o.CheckAndSet = cas;
    });
    Console.WriteLine(success1);
    
    bool success2 = await client.KeyValue.PutAsync("web/key1", "newval", o =>
    {
        o.CheckAndSet = cas;
    });
    Console.WriteLine(success2);
}
```
