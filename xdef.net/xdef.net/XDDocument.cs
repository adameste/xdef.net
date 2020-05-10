using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using xdef.net.Connection;

namespace xdef.net
{
    class XDDocument : RemoteObject
    {

        public XDDocument(int objectId, Client client) : base(objectId, client)
        {
        }

    }
}
