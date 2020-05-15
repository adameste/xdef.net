using System;
using System.Collections.Generic;
using System.Text;
using xdef.net.Connection;

namespace xdef.net
{
    public sealed class XDPool : RemoteObject
    {
        public XDPool(int objectId, Client client) : base(objectId, client)
        {
        }
    }
}
