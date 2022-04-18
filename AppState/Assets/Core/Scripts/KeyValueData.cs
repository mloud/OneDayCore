using System.Collections.Generic;

namespace OneDay.Core
{
    public class KeyValueData
    {
        public static KeyValueData Create() => new();

        private Dictionary<string, object> internalDict = new Dictionary<string, object>();
        public KeyValueData Add<T>(string key, T value)
        {
            internalDict.Add(key, value);
            return this;
        }
        public T Get<T>(string key)
        {
            return (T)internalDict[key];
        }

        public bool Has(string key) => internalDict.ContainsKey(key);
    }
}
