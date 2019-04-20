using System.Collections.Generic;

namespace Verizon.Connect.Tests.Repository
{
    public abstract class InMemoryRepository
    {
        public IDictionary<string, object> Items;

        public InMemoryRepository()
        {
            this.Items = new Dictionary<string, object>();
        }

        public void Add<T>(string key, T value)
        {
            this.Items[key] = value;
        }

        public T Get<T>(string key)
        {
            return this.Items.ContainsKey(key) ? (T)this.Items[key] : default(T);
        }
    }
}
