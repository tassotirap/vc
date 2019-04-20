namespace Verizon.Connect.Domain.Core.Bus
{
    using System.Threading.Tasks;
    using Verizon.Connect.Domain.Core.Events;

    public interface IEventRecived<T> where T : Event
    {
        Task<bool> EventReceived(T @event);
    }
}
