﻿using System.Threading;
using System.Threading.Tasks;

namespace StackExchange.BackgroundJobs.Scheduler
{
    public interface IScheduledTask
    {
        string Schedule { get; }

        Task ExecuteAsync(CancellationToken cancellationToken);
    }
}
