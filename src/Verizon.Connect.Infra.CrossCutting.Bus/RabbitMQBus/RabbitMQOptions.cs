namespace Verizon.Connect.Infra.CrossCutting.Bus.RabbitMQBus
{
    public class RabbitMQOptions
    {
        public string HostName { get; set; }

        public string Exchange { get; set; }

        public string QueueName { get; set; }

        public bool ConsumerEnabled { get; set; }
    }
}
