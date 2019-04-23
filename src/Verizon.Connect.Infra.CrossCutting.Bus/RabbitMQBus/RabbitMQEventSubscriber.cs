namespace Verizon.Connect.Infra.CrossCutting.Bus.RabbitMQBus
{
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.Extensions.Options;

    using Newtonsoft.Json;

    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;

    using Verizon.Connect.Domain.Core.Bus;
    using Verizon.Connect.Domain.Core.Events;
    using Verizon.Connect.Infra.CrossCutting.Bus.RabbitMQBus.Options;

    public class RabbitMQEventSubscriber<T> : IEventSubscriber<T>
        where T : Event
    {
        private readonly List<IModel> channels;

        private readonly List<IConnection> connections;

        private readonly IList<IEventReceived<T>> listEventReceived;

        private readonly IOptions<RabbitMQOptions> options;

        public RabbitMQEventSubscriber(IOptions<RabbitMQOptions> options)
        {
            this.listEventReceived = new List<IEventReceived<T>>();
            this.options = options;
            this.connections = new List<IConnection>();
            this.channels = new List<IModel>();
        }

        public void Dispose()
        {
            foreach (var channel in this.channels)
            {
                channel?.Dispose();
            }

            foreach (var connection in this.connections)
            {
                connection?.Dispose();
            }
        }

        public void StartConsumer()
        {
            for (var consumers = 0; consumers < this.options.Value.NumberOfConsumers; consumers++)
            {
                var channel = this.CreateChannel();

                channel.ExchangeDeclare(this.options.Value.Exchange, ExchangeType.Direct);

                channel.QueueDeclare(this.options.Value.QueueName, true, false, false, null);

                channel.QueueBind(this.options.Value.QueueName, this.options.Value.Exchange, string.Empty, null);

                channel.BasicQos(0, 100, false);

                var consumer = new AsyncEventingBasicConsumer(channel);
                consumer.Received += async (ch, ea) =>
                    {
                        var body = Encoding.UTF8.GetString(ea.Body);

                        var @object = JsonConvert.DeserializeObject<T>(body);

                        foreach (var eventReceived in this.listEventReceived)
                        {
                            await eventReceived.EventReceived(@object);
                        }

                        channel.BasicAck(ea.DeliveryTag, false);
                    };

                channel.BasicConsume(this.options.Value.QueueName, false, consumer);
            }
        }

        public void Subscribe(IEventReceived<T> eventReceived)
        {
            this.listEventReceived.Add(eventReceived);
        }

        private IModel CreateChannel()
        {
            var connection = new ConnectionFactory() { HostName = this.options.Value.HostName, DispatchConsumersAsync = true }.CreateConnection();

            var channel = connection.CreateModel();

            this.connections.Add(connection);
            this.channels.Add(channel);

            return channel;
        }
    }
}