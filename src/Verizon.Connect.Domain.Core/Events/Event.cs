namespace Verizon.Connect.Domain.Core.Events
{
    using MediatR;
    using System;
    using Verizon.Connect.Domain.Core.Messages;

    public abstract class Event : Message, INotification
    {
        public DateTime Timestamp { get; private set; }

        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}
