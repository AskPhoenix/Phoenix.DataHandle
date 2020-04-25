using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WordPressPCL;
using WordPressPCL.Models;

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
            while (!stoppingToken.IsCancellationRequested)
            {
                this._pullerBackgroundQueue.QueueSubmission(data: new PullerStream
                {
                    action = async cancellationToken =>
                    {
                        string baseUrl = "https://www.lingualab.dev-arts.com/wp-json/";

                        var client = new WordPressClient(baseUrl);
                        client.AuthMethod = AuthMethod.JWT;
                        await client.RequestJWToken("theofilos", "MnfbiCmFOvnCsu^bCP9hIm14");

                        var isValidToken = await client.IsValidJWToken();
                        var response = await client.Users.GetAll();

                        this._logger.LogTrace(response.Count().ToString());
                    }
                });

                this._logger.LogDebug($"Added Work: ");
                await Task.Delay(100000, stoppingToken);
            }
        }
    }
}
