using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Render.Core
{
    public static class ResourceManager
    {
        private static readonly Dictionary<string, (string name, Assembly assembly)> resourceMap = new Dictionary<string, (string, Assembly)>();
        private static readonly HashSet<string> prefixes = new HashSet<string>();

        public static void RegisterResourceAssembly(string prefix, string @namespace, Assembly assembly)
        {
            if (prefixes.Contains(prefix))
                throw new InvalidOperationException("Already added an assembly with this prefix");

            prefixes.Add(prefix);
            foreach (var kvp in assembly.GetManifestResourceNames()
                                .ToDictionary(n => prefix + n.Substring(@namespace.Length),
                                              n => (n, assembly)))
                resourceMap.Add(kvp.Key, kvp.Value);
        }

        public static Func<Stream> ResourceGetter(string resourceName)
            => () => resourceMap[resourceName].assembly.GetManifestResourceStream(resourceMap[resourceName].name);

        static ResourceManager()
        {
            RegisterResourceAssembly("Core", "KelsonBall.Render.Core", typeof(ResourceManager).Assembly);
        }
    }
}
