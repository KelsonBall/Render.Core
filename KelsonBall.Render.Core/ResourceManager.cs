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
        private static readonly Dictionary<string, byte[]> assetCache = new Dictionary<string, byte[]>();

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

        public static byte[] Get(string resourceName)
        {
            if (assetCache.ContainsKey(resourceName))
                return assetCache[resourceName];
            else
            {
                using (var stream = ResourceGetter(resourceName)())
                {
                    byte[] data = new byte[stream.Length];
                    int index = 0;
                    int value = 0;
                    while ((value = stream.ReadByte()) >= 0)
                        data[index++] = (byte)value;
                    assetCache[resourceName] = data;
                }
                return assetCache[resourceName];
            }
        }

        static ResourceManager()
        {
            RegisterResourceAssembly("Core", "KelsonBall.Render.Core", typeof(ResourceManager).Assembly);
        }
    }
}
