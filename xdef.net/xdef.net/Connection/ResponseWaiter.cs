using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace xdef.net.Connection
{
    class ResponseWaiter : IDisposable
    {
        public int RequestId { get; set; }
        public SemaphoreSlim Semaphore { get; } = new SemaphoreSlim(0, 1);
        public Request Response { get; set; }

        public void Dispose()
        {
            Semaphore.Dispose();
        }
    }
}
