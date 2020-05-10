using System;
using System.Collections.Generic;
using System.Text;

namespace xdef.net.Connection
{
    public abstract class Client
    {
        protected abstract void Listen();
        protected abstract void Disconnect();
        protected abstract void SendRequestData(Request request);
    }
}
