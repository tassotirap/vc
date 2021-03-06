﻿namespace Verizon.Connect.Infra.CrossCutting.Bus.RabbitMQBus.Options
{
    public class RabbitMQOptions
    {
        public string HostName { get; set; }

        public string Exchange { get; set; }

        public string QueueName { get; set; }

        public int NumberOfConsumers { get; set; }

        public RabbitMQOptions()
        {
            this.NumberOfConsumers = 1;
        }
    }
}
