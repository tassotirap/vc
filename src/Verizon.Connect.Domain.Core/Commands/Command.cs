namespace Verizon.Connect.Domain.Core.Commands
{
    using System;
    using Verizon.Connect.Domain.Core.Messages;

    public abstract class Command : Message
    {
        public DateTime Timestamp { get; private set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public abstract bool IsValid();
    }
}
