namespace Verizon.Connect.Domain.Core.Events
{
    using System;

    /// <summary>
    /// Event Base
    /// </summary>
    public abstract class Event
    {
        protected Event()
        {
            this.Timestamp = DateTime.Now;
        }

        public DateTime Timestamp { get; private set; }
    }
}