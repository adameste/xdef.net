using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace xdef.net.Connection
{
    public abstract class Client : IDisposable
    {
        public abstract void Listen();
        public abstract void Disconnect();
        protected abstract void SendRequestData(Request request);
        private object _sendLock = new object();
        private int _clientRequestId = 1;

        private Dictionary<int, ResponseWaiter> _waitingForResponse = new Dictionary<int, ResponseWaiter>();
        private Dictionary<int, RemoteObject> _remoteObjects = new Dictionary<int, RemoteObject>();

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Free managed resources
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Client()
        {
            Dispose(false);
        }

        public void SendRequestWithoutResponse(Request request)
        {
            lock(_sendLock)
            {
                request.ClientRequestId = _clientRequestId++;
                SendRequestData(request);
            }
        }

        public Request SendRequestWithResponse(Request request)
        {
            var waiter = new ResponseWaiter();
            _waitingForResponse[request.ClientRequestId] = waiter;
            SendRequestWithoutResponse(request);
            waiter.Semaphore.WaitOne();
            var response = waiter.Response;
            waiter.Dispose();
            return response;
        }

        protected void HandleRequest(Request request)
        {
            Task.Factory.StartNew(() =>
            {
                Request response = null;
                if (_waitingForResponse.TryGetValue(request.ClientRequestId, out var waiter))
                {
                    waiter.Response = request;
                    _waitingForResponse.Remove(waiter.RequestId);
                    waiter.Semaphore.Release();
                }
                else if (request.ObjectId == 0)
                {
                    response = HandleObjectlessRequest(request);
                }
                else
                {
                    if (_remoteObjects.TryGetValue(request.ObjectId, out var obj))
                    {
                        response = obj.HandleRequest(request);
                    }
                }
                if (response != null)
                {
                    response.ServerRequestId = request.ServerRequestId;
                    response.ObjectId = request.ObjectId;
                    SendRequestWithoutResponse(response);
                }
            });
        }

        private Request HandleObjectlessRequest(Request request)
        {
            return null;
        }
    }
}
