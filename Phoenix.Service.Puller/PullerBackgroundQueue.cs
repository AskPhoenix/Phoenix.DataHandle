using System;
using System.Collections.Generic;
using Talagozis.AspNetCore;

namespace Phoenix.Service.Puller
{
    public class PullerBackgroundQueue : BackgroundQueue<PullerStream>
    {
        public PullerBackgroundQueue() { }
    }
}
