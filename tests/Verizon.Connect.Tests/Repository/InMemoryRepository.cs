namespace Verizon.Connect.BaseTests.Repository
{
    using System.Collections.Generic;

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