using System;
using System.Collections.Generic;
using System.Text;
using xdef.net.Connection;

namespace xdef.net
{
    internal class XDContainer : RemoteObject
    {
        internal XDContainer(int objectId, Client client) : base(objectId, client)
        {
        }
    }
}
