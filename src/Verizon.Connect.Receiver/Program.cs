namespace Verizon.Connect.Receiver
{
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    using Verizon.Connect.Receiver.DI;
    using Verizon.Connect.Receiver.WindowsService;

    public class Program
    {
        static async Task Main(string[] args)
        {
            var isService = !(Debugger.IsAttached || args.Contains("--console"));

            if (isService)
            {
                var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
                var pathToContentRoot = Path.GetDirectoryName(pathToExe);
                Directory.SetCurrentDirectory(pathToContentRoot);
            }

            var builder = new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        config.SetBasePath(Directory.GetCurrentDirectory());
                        config.AddJsonFile("appsettings.json", false, true);
                    })
                .ConfigureServices((hostContext, services) =>
                    {
                        services.AddHostedService<ReceiverService>();
                        services.AddReceiverServices(hostContext.Configuration);
                    })
                .ConfigureLogging((hostingContext, logging) =>
                    {
                        logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                        logging.AddConsole();
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