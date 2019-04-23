namespace Verizon.Connect.Receiver
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    using Verizon.Connect.Domain.Core.Bus;
    using Verizon.Connect.Domain.Plot.Events;

    public class ReceiverService : IHostedService, IDisposable
    {
        private readonly IEventReceived<RegisterPlotEvent> eventReceived;

        private readonly ILogger<ReceiverService> logger;

        private readonly IEventSubscriber<RegisterPlotEvent> eventSubscriber;

        public ReceiverService(IEventSubscriber<RegisterPlotEvent> eventSubscriber,
                               IEventReceived<RegisterPlotEvent> eventReceived,
                               ILogger<ReceiverService> logger)
        {
            this.eventSubscriber = eventSubscriber;
            this.eventReceived = eventReceived;
            this.logger = logger;
        }

        public void Dispose()
        {
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.eventSubscriber.Subscribe(this.eventReceived);
            this.eventSubscriber.StartConsumer();

            this.logger.LogInformation("Receiver Started");

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.eventSubscriber.Dispose();

            this.logger.LogInformation("Receiver Stoped");

            return Task.CompletedTask;
        }
    }
}