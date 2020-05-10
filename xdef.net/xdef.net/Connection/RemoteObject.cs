using System;
using System.Collections.Generic;
using System.Text;

namespace xdef.net.Connection
{
    public abstract class RemoteObject
    {
        private Client _client;
        protected int _objectId;

        public abstract Request HandleRequest(Request request);

        public RemoteObject(Client client)
        {
            _client = client;
        }

        protected void SendRequest(Request request)
        {
            request.ObjectId = _objectId;
            _client.SendRequestWithoutResponse(request);
        }

        protected Request SendRequestWithResponse(Request request)
        {
            request.ObjectId = _objectId;
            return _client.SendRequestWithResponse(request);
        }
    }
}
