using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JustScheduler;
using Microsoft.Extensions.Logging;

namespace Phoenix.Scheduler.App_Plugins.Services
{
    public class LectureService : IJob
    {
        private readonly ILogger _logger;

        public LectureService(ILogger<LectureService> logger)
        {
            this._logger = logger;
        }

        public Task Run(CancellationToken cancellationToken)
        {
            this._logger.LogTrace("Test");

            return Task.CompletedTask;
        }
    }
}
