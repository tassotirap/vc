namespace Verizon.Connect.Domain.Core.Bus
{
    using System.Threading.Tasks;
    using Verizon.Connect.Domain.Core.Commands;
    using Verizon.Connect.Domain.Core.Events;

    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
