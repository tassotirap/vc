namespace Verizon.Connect.Receiver
{
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Verizon.Connect.Domain.Core.Bus;
    using Verizon.Connect.Domain.Plot.Events;

    public class ReceiverService : IHostedService, IDisposable
    {
        private readonly IEventSubscriber<RegisterNewPlotEvent> eventSubscriber;
        private readonly IEventRecived<RegisterNewPlotEvent> eventRecived;

        public ReceiverService(
            IEventSubscriber<RegisterNewPlotEvent> eventSubscriber, 
            IEventRecived<RegisterNewPlotEvent> eventRecived)
        {
            this.eventSubscriber = eventSubscriber;
            this.eventRecived = eventRecived;
        }

        public void Dispose()
        {
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            eventSubscriber.Subscribe(eventRecived);
            eventSubscriber.StartConsumer();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            //TODO: unsubscribe
            return Task.CompletedTask;
        }
    }
}
