using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Phoenix.Service.Puller
{
    public class PullerStream
    {
        public Func<CancellationToken, Task> action;

    }
}
