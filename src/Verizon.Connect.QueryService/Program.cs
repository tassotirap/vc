﻿namespace Verizon.Connect.QueryService
{
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Hosting.WindowsServices;

    public class Program
    {
        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
        }

        public static void Main(string[] args)
        {
            var isService = !(Debugger.IsAttached || args.Contains("--console"));

            if (isService)
            {
                var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
                var pathToContentRoot = Path.GetDirectoryName(pathToExe);
                Directory.SetCurrentDirectory(pathToContentRoot);
            }

            var builder = CreateWebHostBuilder(args.Where(arg => arg != "--console").ToArray());

            var host = builder.Build();

            if (isService)
            {
                host.RunAsService();
            }
            else
            {
                host.Run();
            }
        }
    }
}