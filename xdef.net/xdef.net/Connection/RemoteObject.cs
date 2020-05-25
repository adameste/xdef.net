using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace xdef.net.Connection
{
    public abstract class RemoteObject
    {
        internal Client _client;
        internal int ObjectId { get; set; }

        internal RemoteObject(int objectId, Client client)
        {
            ObjectId = objectId;
            _client = client;
        }
        ~RemoteObject()
        {
            DeleteRemoteObject();
        }

        protected virtual void DeleteRemoteObject()
        {
            _client?.SendRequestWithoutResponse(new DeleteObjectRequest(ObjectId));
        }


        internal void SendRequest(Request request)
        {
            request.ObjectId = ObjectId;
            _client.SendRequestWithoutResponse(request);
        }

        internal Request SendRequestWithResponse(Request request)
        {
            request.ObjectId = ObjectId;
            return _client.SendRequestWithResponse(request);
        }
    }
}
