namespace Verizon.Connect.Domain.Core.Bus
{
    using System.Threading.Tasks;
    using Verizon.Connect.Domain.Core.Events;

    public interface IEventEmitter<T> where T : Event
    {
        Task EmitAsync(T @event);

        void Emit(T @event);
    }
}
