using System;
using System.Collections.Generic;
using System.Text;
using xdef.net.Connection;

namespace xdef.net
{
    public class XDElement : RemoteObject
    {
        public XDElement(int objectId, Client client) : base(objectId, client)
        {
        }
    }
}
