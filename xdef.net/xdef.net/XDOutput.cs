using System;
using System.Collections.Generic;
using System.Text;
using xdef.net.Connection;

namespace xdef.net
{
    public class XDOutput : RemoteObject
    {
        public XDOutput(int objectId, Client client) : base(objectId, client)
        {
        }
    }
}
