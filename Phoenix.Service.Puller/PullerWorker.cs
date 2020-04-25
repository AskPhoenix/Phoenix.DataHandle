using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Phoenix.Service.Puller
{
    public class PullerWorker : BackgroundService
    {
        private readonly ILogger<PullerWorker> _logger;
        private readonly PullerBackgroundQueue _pullerBackgroundQueue;

        public PullerWorker(PullerBackgroundQueue pullerBackgroundQueue, ILogger<PullerWorker> logger)
        {
            this._pullerBackgroundQueue = pullerBackgroundQueue;
            this._logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Random random = new Random();
            int c = 0;

            while (!stoppingToken.IsCancellationRequested)
            {
                c++;
                int c1 = c;
                int t = random.Next(50, 900);

                this._pullerBackgroundQueue.QueueSubmission(data: new PullerStream
                {
                    action = async cancellationToken =>
                    {
                        await Task.Delay(600, stoppingToken);

                        this._logger.LogInformation($"{c1} Worker running at: {DateTimeOffset.Now} in {t}");

                        //return Task.CompletedTask;
                    }
                });

                this._logger.LogDebug($"Added Work: {c1}");
                await Task.Delay(t, stoppingToken);
            }
        }
    }
}
