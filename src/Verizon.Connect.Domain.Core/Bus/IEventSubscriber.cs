namespace Verizon.Connect.Domain.Core.Bus
{
    using System.Collections.Generic;
    using Verizon.Connect.Domain.Core.Events;

    public interface IEventSubscriber<T> where T : Event
    {
        void Subscribe(IEventRecived<T> eventRecived);

        void Subscribe(IEnumerable<IEventRecived<T>> eventRecived);

        void StartConsumer();
    }
}
