using System;
using System.Collections.Generic;
using System.Text;
using xdef.net.Connection;

namespace xdef.net.Sys
{
    public sealed class Report : RemoteObject
    {
        public Report(int objectId, Client client) : base(objectId, client)
        {
        }
    }
}
