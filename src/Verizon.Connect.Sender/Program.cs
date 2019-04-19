namespace Verizon.Connect.Sender
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Threading.Tasks;
    using Verizon.Connect.Sender.DI;

    class Program
    {
        static async Task Main(string[] args)
        {
            (int vehicle, int interval) = GetVehicleAndInterval(args);
            if (vehicle == 0)
            {
                throw new ArgumentException("Vehicle or Iterval should greater than 0");
            }

            Console.Write("Working...");

            IConfiguration configuration = GetConfiguration();
            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddSenderServices(configuration);

            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            SenderService senderApplication = serviceProvider.GetService<SenderService>();
            await senderApplication.Start(vehicle, interval);
        }

        private static IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        private static (int, int) GetVehicleAndInterval(string[] args)
        {
            if (args.Length == 4)
            {
                if (int.TryParse(args[1], out int vehicle) && int.TryParse(args[3], out int interval))
                {
                    return (vehicle, interval);
                }
            }

            return (0, 0);

        }
    }
}
