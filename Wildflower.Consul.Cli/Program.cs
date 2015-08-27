using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wildflower.Consul.Cli
{
    class Program
    {
        static int Main(string[] args)
        {
            return Synchronous.Invoke(() => MainAsync(args));
        }

        static async Task<int> MainAsync(string[] args)
        {
            using (var client = new ConsulClient())
            {
                var response = await client.Storage.GetAllAsync();
                foreach (var entry in await response.Content())
                {
                    Console.WriteLine("{0} = {1}", entry.Key, entry.Value);
                }
            }

            return 0;
        }
    }
}
