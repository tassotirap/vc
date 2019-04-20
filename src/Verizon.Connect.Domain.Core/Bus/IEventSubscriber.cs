namespace Verizon.Connect.Domain.Core.Bus
{
    using System;

    using Verizon.Connect.Domain.Core.Events;

    public interface IEventSubscriber<T> : IDisposable
        where T : Event
    {
        void StartConsumer();

        void Subscribe(IEventRecived<T> eventRecived);
    }
}