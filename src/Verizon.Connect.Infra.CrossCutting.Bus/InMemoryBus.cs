namespace Verizon.Connect.Infra.CrossCutting.Bus
{
    using MediatR;
    using System.Threading.Tasks;
    using Verizon.Connect.Domain.Core.Bus;
    using Verizon.Connect.Domain.Core.Commands;
    using Verizon.Connect.Domain.Core.Events;


    public sealed class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator mediator;

        public InMemoryBus(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public Task SendCommand<T>(T command) where T : Command
        {
            return mediator.Send(command);
        }

        public Task RaiseEvent<T>(T @event) where T : Event
        {
            return mediator.Publish(@event);
        }
    }
}
