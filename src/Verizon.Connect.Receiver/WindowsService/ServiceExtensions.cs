﻿namespace Verizon.Connect.Receiver.WindowsService
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System.Threading;
    using System.Threading.Tasks;

    public static class ServiceExtensions
    {
        public static Task RunAsCustomService(this IHostBuilder hostBuilder, CancellationToken cancellationToken = default)
        {
            return hostBuilder.UseServiceBaseLifetime().Build().RunAsync(cancellationToken);
        }

        public static IHostBuilder UseServiceBaseLifetime(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices(
                (hostContext, services) => services.AddSingleton<IHostLifetime, ServiceBaseLifetime>());
        }
    }
}