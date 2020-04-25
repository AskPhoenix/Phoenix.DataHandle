using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Phoenix.Service.Puller
{
    public class PullerBackgroundService : BackgroundService
    {
        private readonly CancellationTokenSource _shutdown = new CancellationTokenSource();
        private Task _backgroundTask;
        private readonly PullerBackgroundQueue _backgroundQueue;

        public PullerBackgroundService(PullerBackgroundQueue backgroundQueue)
        {
            this._backgroundQueue = backgroundQueue;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            this._backgroundTask = Task.Run(async () => 
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    await this.BackgroundProcessing();
                }
            }, stoppingToken);
            await this._backgroundTask;
        }

        private async Task ProcessSingleSubmission(PullerStream pullerStream, CancellationToken ct)
        {
            if (pullerStream == null)
                return;

            await pullerStream.action(ct);
        }

        private async Task BackgroundProcessing()
        {
            while (!this._shutdown.IsCancellationRequested)
            {
                var workItem = await this._backgroundQueue.DequeueAsync(this._shutdown.Token);
                try
                {
                    await this.ProcessSingleSubmission(workItem, this._shutdown.Token);
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            this._shutdown.Cancel();
            await Task.WhenAny(this._backgroundTask,
                Task.Delay(Timeout.Infinite, stoppingToken));
        }
    }
}
