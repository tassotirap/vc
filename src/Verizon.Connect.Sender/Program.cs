namespace Verizon.Connect.Sender
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Verizon.Connect.Sender.DI;

    class Program
    {
        static async Task Main(string[] args)
        {
            var (vehicle, interval) = GetVehicleAndInterval(args);
            if(vehicle == 0)
            {
                throw new ArgumentException("Vehicle or Iterval should greater than 0");
            }

            Console.Write("Working...");

            IConfiguration configuration = GetConfiguration();
            IServiceCollection serviceCollection = new ServiceCollection();

            DIBootStrapper.RegisterServices(serviceCollection, configuration);

            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            SenderApplication senderApplication = serviceProvider.GetService<SenderApplication>();
            await senderApplication.Start(vehicle, interval);
        }

        private static IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
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
