using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace xdef.net.Connection
{
    class ResponseWaiter : IDisposable
    {
        public int RequestId { get; set; }
        public Semaphore Semaphore { get; } = new Semaphore(0, 1);
        public Request Response { get; set; }

        public void Dispose()
        {
            Semaphore.Dispose();
        }
    }
}
