using System;
using System.Collections.Generic;
using System.Text;
using xdef.net.Connection;

namespace xdef.net
{
    public class XDBuilder : RemoteObject
    {
        internal XDBuilder(int objectId, Client client) : base(objectId, client)
        {
        }
    }
}
