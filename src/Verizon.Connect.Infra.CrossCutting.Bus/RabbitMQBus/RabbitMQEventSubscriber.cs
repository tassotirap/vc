namespace Verizon.Connect.Infra.CrossCutting.Bus.RabbitMQBus
{
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Verizon.Connect.Domain.Core.Bus;
    using Verizon.Connect.Domain.Core.Events;
    using Verizon.Connect.Infra.CrossCutting.Bus.RabbitMQBus.Options;

    public class RabbitMQEventSubscriber<T> :
       IEventSubscriber<T>,
       IDisposable
       where T : Event
    {
        private IConnection connection;
        private IModel channel;

        private readonly IList<IEventRecived<T>> listEventRecived;
        private readonly IOptions<RabbitMQOptions> options;

        public RabbitMQEventSubscriber(IOptions<RabbitMQOptions> options)
        {
            this.listEventRecived = new List<IEventRecived<T>>();
            this.options = options;
        }

        public void Dispose()
        {
            this.channel?.Dispose();
            this.connection?.Dispose();
        }

        public void Subscribe(IEventRecived<T> eventRecived)
        {
            this.listEventRecived.Add(eventRecived);
        }

        public void Subscribe(IEnumerable<IEventRecived<T>> eventReciveds)
        {
            foreach (IEventRecived<T> eventRecived in eventReciveds)
            {
                this.Subscribe(eventRecived);
            }
        }

        private void CreateChannel()
        {
            this.connection = new ConnectionFactory()
            {
                HostName = this.options.Value.HostName,
                DispatchConsumersAsync = true
            }.CreateConnection();

            this.channel = this.connection.CreateModel();
        }

        public void StartConsumer()
        {
            this.CreateChannel();

            this.channel.ExchangeDeclare(this.options.Value.Exchange, ExchangeType.Direct);

            this.channel.QueueDeclare(this.options.Value.QueueName, true, false, false, null);

            this.channel.QueueBind(this.options.Value.QueueName, this.options.Value.Exchange, string.Empty, null);

            this.channel.BasicQos(0, 100, false);

            AsyncEventingBasicConsumer consumer = new AsyncEventingBasicConsumer(this.channel);
            consumer.Received += async (ch, ea) =>
            {
                string body = Encoding.UTF8.GetString(ea.Body);

                T @object = JsonConvert.DeserializeObject<T>(body);

                foreach (IEventRecived<T> eventRecived in this.listEventRecived)
                {
                    await eventRecived.EventRecivedAsync(@object);
                }

                this.channel.BasicAck(ea.DeliveryTag, false);
            };

            this.channel.BasicConsume(this.options.Value.QueueName, false, consumer);
        }
    }
}
