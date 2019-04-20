﻿namespace Verizon.Connect.Receiver.WindowsService
{
    using System;
    using System.ServiceProcess;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Hosting;

    public class ServiceBaseLifetime : ServiceBase, IHostLifetime
    {
        private readonly IApplicationLifetime applicationLifetime;

        private readonly TaskCompletionSource<object> delayStart;

        public ServiceBaseLifetime(IApplicationLifetime applicationLifetime)
        {
            this.delayStart = new TaskCompletionSource<object>();
            this.applicationLifetime =
                applicationLifetime ?? throw new ArgumentNullException(nameof(applicationLifetime));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.Stop();
            return Task.CompletedTask;
        }

        public Task WaitForStartAsync(CancellationToken cancellationToken)
        {
            cancellationToken.Register(() => this.delayStart.TrySetCanceled());
            this.applicationLifetime.ApplicationStopping.Register(this.Stop);

            new Thread(this.Run).Start(); // Otherwise this would block and prevent IHost.StartAsync from finishing.
            return this.delayStart.Task;
        }

        // Called by base.Run when the service is ready to start.
        protected override void OnStart(string[] args)
        {
            this.delayStart.TrySetResult(null);
            base.OnStart(args);
        }

        // Called by base.Stop. This may be called multiple times by service Stop, ApplicationStopping, and StopAsync.
        // That's OK because StopApplication uses a CancellationTokenSource and prevents any recursion.
        protected override void OnStop()
        {
            this.applicationLifetime.StopApplication();
            base.OnStop();
        }

        private void Run()
        {
            try
            {
                Run(this); // This blocks until the service is stopped.
                this.delayStart.TrySetException(new InvalidOperationException("Stopped without starting"));
            }
            catch (Exception ex)
            {
                this.delayStart.TrySetException(ex);
            }
        }
    }
}