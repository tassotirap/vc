namespace Verizon.Connect.Receiver
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Verizon.Connect.Receiver.DI;

    class Program
    {
        static async Task Main(string[] args)
        {
            bool isService = !(Debugger.IsAttached || args.Contains("--console"));

            IHostBuilder builder = new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<ReceiverService>();
                    services.AddReceiverServices(hostContext.Configuration);
                });

            if (isService)
            {
                await builder.RunAsCustomService();
            }
            else
            {
                await builder.RunConsoleAsync();
            }

        }
    }
}
