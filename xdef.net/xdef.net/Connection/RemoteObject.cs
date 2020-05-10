using System;
using System.Collections.Generic;
using System.Text;

namespace xdef.net.Connection
{
    public abstract class RemoteObject
    {
        protected Client _client;
        protected int _objectId;

        public RemoteObject(int objectId, Client client)
        {
            _objectId = objectId;
            _client = client;
        }
        ~RemoteObject()
        {
            _client?.SendRequestWithoutResponse(new DeleteObjectRequest(_objectId));
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
