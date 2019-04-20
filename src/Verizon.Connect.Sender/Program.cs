namespace Verizon.Connect.Sender
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using Verizon.Connect.Application.Interfaces;
    using Verizon.Connect.Sender.DI;

    public class Program
    {
        private static IServiceProvider serviceProvider;

        private static ILogger<Program> logger;

        public static async Task Start(int vehicleId, int interval)
        {
            logger.LogInformation("Sender started");

            var senderApplication = new SenderService(serviceProvider.GetService<IPlotAppService>(), serviceProvider.GetService<ILogger<SenderService>>());

            while (true)
            {
                await senderApplication.GenerateRandomPlot(vehicleId);
                await Task.Delay(interval);
            }
        }

        private static IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", false, true)
                .Build();
        }

        private static (int, int) GetVehicleAndInterval(string[] args)
        {
            if (args.Length == 4)
            {
                if (int.TryParse(args[1], out var vehicle) && int.TryParse(args[3], out var interval))
                {
                    return (vehicle, interval);
                }
            }

            return (0, 0);
        }

        private static async Task Main(string[] args)
        {
            var (vehicle, interval) = GetVehicleAndInterval(args);
            if (vehicle == 0)
            {
                throw new ArgumentException("Vehicle or Interval should greater than 0");
            }

            var configuration = GetConfiguration();

            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSenderServices(configuration);

            serviceCollection.AddLogging(configure => 
                configure
                    .AddConsole()
                    .AddConfiguration(configuration.GetSection("Logging")));

            serviceProvider = serviceCollection.BuildServiceProvider();

            logger = serviceProvider.GetService<ILogger<Program>>();

            await Start(vehicle, interval);
        }
    }
}