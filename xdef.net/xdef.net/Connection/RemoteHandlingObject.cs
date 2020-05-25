using System;
using System.Collections.Generic;
using System.Text;

namespace xdef.net.Connection
{
    internal abstract class RemoteHandlingObject : RemoteObject
    {
        public RemoteHandlingObject(Client client) : base(0, client)
        {
        }

        protected override void DeleteRemoteObject()
        {
            // Do nothing
        }

        public abstract Request HandleRequest(Request request);
    }
}
