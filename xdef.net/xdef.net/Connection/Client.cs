using System;
using System.Collections.Concurrent;
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
        protected abstract Task SendRequestDataAsync(Request request);

        private int _clientRequestId = 0;
        private int _currentObjectId = 0;
        private ObjectlessRequestHandler _objectlessRequestHandler;


        private ConcurrentDictionary<int, ResponseWaiter> _waitingForResponse = new ConcurrentDictionary<int, ResponseWaiter>();
        private ConcurrentDictionary<int, RemoteHandlingObject> _remoteObjects = new ConcurrentDictionary<int, RemoteHandlingObject>();
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

        public async Task SendRequestWithoutResponseAsync(Request request)
        {
            await SendRequestDataAsync(request);
        }

        public Request SendRequestWithResponse(Request request)
        {
            request.ClientRequestId = Interlocked.Increment(ref _clientRequestId);
            var waiter = new ResponseWaiter()
            {
                RequestId = request.ClientRequestId
            };
            _waitingForResponse[request.ClientRequestId] = waiter;
            SendRequestData(request);
            waiter.Semaphore.Wait();
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
                _waitingForResponse.TryRemove(waiter.RequestId, out _);
                waiter.Semaphore.Release();
            }
            else
            {
                Task.Factory.StartNew(async () =>
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
                        await SendRequestWithoutResponseAsync(response);
                    }
                });
            }

        }

        internal int RegisterLocalObject(RemoteHandlingObject obj)
        {
            var objId = Interlocked.Increment(ref _currentObjectId);
            while (_remoteObjects.ContainsKey(objId) || objId == 0) // Prevent overflow of object ids with 0
                objId = Interlocked.Increment(ref _currentObjectId);
            _remoteObjects[obj.ObjectId] = obj;

            return obj.ObjectId;
        }

        internal void DeleteLocalObject(int objectId)
        {
                _remoteObjects.TryRemove(objectId, out _);
        }

    }
}
