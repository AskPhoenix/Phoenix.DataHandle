using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Phoenix.DataHandle.Main.Models;
using Phoenix.WordPress.Puller.WorkerJobs;
using WordPressPCL;
using WordPressPCL.Models;

#pragma warning disable CS4014

namespace Phoenix.WordPress.Puller
{
    public class PullerWorker : BackgroundService
    {
        private readonly ILogger<PullerWorker> _logger;
        private readonly PullerBackgroundQueue _pullerBackgroundQueue;
        private readonly IConfiguration _configuration;
        private readonly IServiceScopeFactory _scopeFactory;

        public PullerWorker(
            PullerBackgroundQueue pullerBackgroundQueue,
            ILogger<PullerWorker> logger,
            IConfiguration configuration,
            IServiceScopeFactory scopeFactory)
        {
            this._pullerBackgroundQueue = pullerBackgroundQueue;
            this._logger = logger;
            this._configuration = configuration;
            this._scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _pullerBackgroundQueue.QueueSubmission(data: new PullerStream
                {
                    action = async cancellationToken =>
                    {
                        using (var scope = _scopeFactory.CreateScope())
                        {
                            var phoenixContext = scope.ServiceProvider.GetRequiredService<PhoenixContext>();
                            foreach (var school in phoenixContext.School)
                            {
                                //Setup
                                var client = new WordPressClient(new Uri(new Uri(school.Endpoint), "wp-json").ToString());
                                client.AuthMethod = AuthMethod.JWT;
                                await client.RequestJWToken(_configuration["WordPressAdmin:UserName"], _configuration["WordPressAdmin:Password"]);
                                if (!await client.IsValidJWToken())
                                    throw new Exception("Invalid JWToken.");
                                List<Task> tasks = new List<Task>(5);

                                //Start the updates of the entities
                                tasks.Add(BookJobs.UpdateAsync(client, phoenixContext));
                                tasks.Add(ClassroomJobs.UpdateAsync(client, phoenixContext));
                                tasks.Add(CourseJobs.UpdateAsync(client, phoenixContext));
                                tasks.Add(LectureJobs.UpdateAsync(client, phoenixContext));
                                tasks.Add(UserJobs.UpdateAsync(client, phoenixContext));

                                //Save changes to DB
                                Task.WaitAll(tasks.ToArray());
                                await phoenixContext.SaveChangesAsync();
                            }
                        }
                    }
                });

                _logger.LogDebug("Added Work}");
                await Task.Delay(100000, stoppingToken);
            }
        }
    }
}
