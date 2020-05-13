using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace xdef.net.Connection
{
    public abstract class Client : IDisposable
    {
        public abstract void Listen();
        public abstract void Disconnect();
        protected abstract void SendRequestData(Request request);

        private int _clientRequestId = 1;
        public object _clientReqIdLock = new object();
        private int _currentObjectId = 1;
        private ObjectlessRequestHandler _objectlessRequestHandler;


        private Dictionary<int, ResponseWaiter> _waitingForResponse = new Dictionary<int, ResponseWaiter>();
        private Dictionary<int, RemoteHandlingObject> _remoteObjects = new Dictionary<int, RemoteHandlingObject>();
        public Client()
        {
            _objectlessRequestHandler = new ObjectlessRequestHandler(this);
        }

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
            SendRequestData(request);
        }

        public Request SendRequestWithResponse(Request request)
        {
            lock (_clientReqIdLock)
            {
                request.ClientRequestId = _clientRequestId++;
            }
            var waiter = new ResponseWaiter()
            {
                RequestId = request.ClientRequestId
            };
            lock (_waitingForResponse)
            {
                _waitingForResponse[request.ClientRequestId] = waiter;
            }
            SendRequestData(request);
            waiter.Semaphore.WaitOne();
            var response = waiter.Response;
            if (ResponseException.IsResponseException(response))
            {
                throw ResponseException.GetException(response);
            }
            waiter.Dispose();
            return response;
        }

        protected void HandleRequest(Request request)
        {
            if (_waitingForResponse.TryGetValue(request.ClientRequestId, out var waiter))
            {
                waiter.Response = request;
                lock (_waitingForResponse)
                {
                    _waitingForResponse.Remove(waiter.RequestId);
                }
                waiter.Semaphore.Release();
            }
            else
            {
                Task.Factory.StartNew(() =>
                {
                    Request response = null;


                    if (request.ObjectId == 0)
                    {
                        response = _objectlessRequestHandler.HandleRequest(request);
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

        }

        internal int RegisterObject(RemoteHandlingObject obj)
        {
            lock (_remoteObjects)
            {
                _remoteObjects.Add(_currentObjectId, obj);
                obj.ObjectId = _currentObjectId;
                _currentObjectId++;
            }

            return obj.ObjectId;
        }

        internal void DeleteLocalObject(int objectId)
        {
            lock (_remoteObjects)
            {
                _remoteObjects.Remove(objectId);
            }
        }

    }
}
