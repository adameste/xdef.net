using System;
using System.Collections.Generic;
using System.Text;
using xdef.net.Connection;

namespace xdef.net
{
    public class XDService : RemoteObject
    {
        internal XDService(int objectId, Client client) : base(objectId, client)
        {
        }
    }
}
