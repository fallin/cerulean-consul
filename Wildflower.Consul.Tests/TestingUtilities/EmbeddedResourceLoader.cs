using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Cerulean.Consul.Tests.TestingUtilities
{
    public class EmbeddedResourceLoader
    {
        readonly Assembly _callingAssembly;
        readonly string _rootPath;

        public EmbeddedResourceLoader(string rootPath)
            : this(Assembly.GetCallingAssembly(), rootPath)
        {
            
        }

        public EmbeddedResourceLoader(Assembly resourceAssembly, string rootPath)
        {
            _callingAssembly = resourceAssembly;
            _rootPath = rootPath;
        }

        public Stream LoadEmbeddedStream(string name)
        {
            if (name == null) throw new ArgumentNullException("name");

            Assembly assembly = _callingAssembly;

            string assemblyName = assembly.GetName().Name;
            string resourceName = assemblyName;

            if (!string.IsNullOrEmpty(_rootPath))
            {
                resourceName += string.Format(".{0}", _rootPath);
            }
            resourceName += string.Format(".{0}", name);
            resourceName = Regex.Replace(resourceName, @"[\\/]+", ".");

            Stream stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
            {
                Trace.TraceWarning("Embedded resource [{0}] was not found. Make sure the 'Build Action' for the file is set to 'Embedded Resource'.", 
                    resourceName);
                string message = string.Format("Failed to load embedded resource file [{0}].", name);
                throw new Exception(message);
            }
            return stream;
        }

        public JToken LoadEmbeddedJson(string name)
        {
            if (name == null) throw new ArgumentNullException("name");
            JToken token;

            if (!name.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
            {
                name += ".json";
            }

            Stream stream = LoadEmbeddedStream(name);
            using (var streamReader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                token = JToken.Load(jsonReader);
            }

            return token;
        }

        public T LoadEmbeddedJsonAs<T>(string name) where T : class 
        {
            JToken token = LoadEmbeddedJson(name);
            T obj = (token != null) ? token.ToObject<T>() : null;
            return obj;
        }
    }
}