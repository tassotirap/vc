namespace Verizon.Connect.Infra.CrossCutting.Bus.RabbitMQBus
{
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using RabbitMQ.Client;
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using Verizon.Connect.Domain.Core.Bus;
    using Verizon.Connect.Domain.Core.Events;
    using Verizon.Connect.Infra.CrossCutting.Bus.RabbitMQBus.Options;

    public class RabbitMQEventEmitter<T> :
        IEventEmitter<T>,
        IDisposable
        where T : Event
    {
        private IConnection connection;
        private IModel channel;

        private readonly IOptions<RabbitMQOptions> options;

        public RabbitMQEventEmitter(IOptions<RabbitMQOptions> options)
        {
            this.options = options;
            this.CreateChannel();
            this.CreateExchange();
        }

        public void Dispose()
        {
            this.channel?.Dispose();
            this.connection?.Dispose();
        }

        public void Emit(T @event)
        {
            string jsonObject = JsonConvert.SerializeObject(@event);

            byte[] body = Encoding.UTF8.GetBytes(jsonObject);

            this.channel.BasicPublish(this.options.Value.Exchange, string.Empty, null, body);
        }

        private void CreateExchange()
        {
            this.channel.ExchangeDeclare(this.options.Value.Exchange, ExchangeType.Direct);

            this.channel.QueueDeclare(this.options.Value.QueueName, true, false, false, null);

            this.channel.QueueBind(this.options.Value.QueueName, this.options.Value.Exchange, string.Empty, null);
        }

        private void CreateChannel()
        {
            this.connection = new ConnectionFactory()
            {
                HostName = this.options.Value.HostName
            }.CreateConnection();

            this.channel = this.connection.CreateModel();
        }

        public Task EmitAsync(T @event)
        {
            return Task.Run(() =>
            {
                this.Emit(@event);
            });
        }
    }
}
