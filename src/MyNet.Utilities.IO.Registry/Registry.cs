// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using System.Linq;

namespace MyNet.Utilities.IO.Registry
{
    public class Registry<T> : IRegistry
    {
        public string Key { get; private set; }

        public string Parent { get; private set; }

        public T Item { get; }

        public Registry(string path, T item)
        {
            var split = path.Split('\\');
            Key = split.LastOrDefault() ?? string.Empty;
            Parent = string.Join('\\', split.Take(split.Length - 1));
            Item = item;
        }

        public Registry(string key, string parent, T item)
        {
            Key = key;
            Parent = parent;
            Item = item;
        }
    }
}
