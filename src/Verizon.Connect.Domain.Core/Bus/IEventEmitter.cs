namespace Verizon.Connect.Domain.Core.Bus
{
    using Verizon.Connect.Domain.Core.Events;

    public interface IEventEmitter<T> where T : Event
    {
        void Emit(T @event);
    }
}
