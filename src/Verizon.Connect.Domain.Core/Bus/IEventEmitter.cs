namespace Verizon.Connect.Domain.Core.Bus
{
    using Verizon.Connect.Domain.Core.Events;

    /// <summary>
    /// Emit a new event
    /// </summary>
    /// <typeparam name="T">Event object</typeparam>
    public interface IEventEmitter<in T> where T : Event
    {
        void Emit(T @event);
    }
}
