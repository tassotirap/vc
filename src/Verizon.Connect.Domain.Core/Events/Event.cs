namespace Verizon.Connect.Domain.Core.Events
{
    using System;

    public abstract class Event
    {
        public DateTime Timestamp { get; private set; }

        protected Event()
        {
            this.Timestamp = DateTime.Now;
        }
    }
}
