using System;
using System.Threading;
using System.Threading.Tasks;

namespace Phoenix.WordPress.Puller
{
    public class PullerStream
    {
        public Func<CancellationToken, Task> action;
    }
}
