using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoenix.DataHandle.Sms
{
    public interface ISmsService
    {
        Task SendAsync(string destination, string body);
    }
}
