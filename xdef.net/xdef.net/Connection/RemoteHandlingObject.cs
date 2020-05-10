using System;
using System.Collections.Generic;
using System.Text;

namespace xdef.net.Connection
{
    public abstract class RemoteHandlingObject : RemoteObject
    {
        public RemoteHandlingObject(int objectId, Client client) : base(objectId, client)
        {
        }

        public abstract Request HandleRequest(Request request);
    }
}
