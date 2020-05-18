package org.xdef.bridge.server;

import java.io.IOException;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.concurrent.atomic.AtomicInteger;

import org.xdef.bridge.remoteObjects.RemoteHandlingObject;
import org.xdef.bridge.server.requests.ObjectlessRequestHandler;
import org.xdef.bridge.server.requests.RemoteCallException;
import org.xdef.bridge.server.requests.Request;
import org.xdef.bridge.server.requests.RequestWaiter;
import org.xdef.bridge.server.requests.Response;
import org.xdef.bridge.server.requests.ResponseException;

public abstract class Client {

    private AtomicInteger serverRequestId = new AtomicInteger(0);
    private AtomicInteger remoteObjectId = new AtomicInteger(0);

    private final Map<Integer, RequestWaiter> waitingRequests = new ConcurrentHashMap<Integer, RequestWaiter>();
    private final Map<Integer, RemoteHandlingObject> remoteObjects = new ConcurrentHashMap<Integer, RemoteHandlingObject>();

    private final ExecutorService threadPool = Executors.newCachedThreadPool();

    private final ObjectlessRequestHandler objectlessRequestHandler;

    public Client() {
        this.objectlessRequestHandler = new ObjectlessRequestHandler(this);
    }

    @Override
    protected void finalize() throws Throwable {
        threadPool.shutdown();
    }

    public abstract void disconnect();

    public abstract void listen() throws IOException;

    protected abstract void sendRequestData(Request request);


    public void sendRequestWithoutResponse(Request request) {
        sendRequestData(request);
    }

    public Request sendRequestWithResponse(Request request) {
        int id = serverRequestId.incrementAndGet();
        request.setServerRequestId(id);
        RequestWaiter waiter = new RequestWaiter(id);
        waitingRequests.put(id, waiter);
        sendRequestData(request);
        try {
            waiter.getSemaphore().acquire();
        } catch (InterruptedException e) {
            return null;
        }
        if (ResponseException.isException(request)) {
            RemoteCallException ex = ResponseException.getException(request);
            System.err.println("Remote call exception thrown code: " + ex.getErrorCode() + " message: " + ex.getMessage());
        }
        return waiter.getResponse();
    }

    protected void handleRequest(Request request) {
        RequestWaiter waiter = waitingRequests.remove(request.getServerRequestId());
        if (waiter != null) {
            waiter.setResponse(request);
            waiter.getSemaphore().release();
        } else {
            threadPool.submit(() -> {
                Response response = null;
                if (request.getObjectId() == 0) {
                    response = objectlessRequestHandler.handleRequest(request);
                } else {
                    RemoteHandlingObject obj = remoteObjects.get(request.getObjectId());
                    if (obj == null) {
                        response = new ResponseException(ResponseException.ERROR_CODE_UNKNOWN_OBJECT, "Unknown remote object.");
                    } else {
                        response = obj.handleRequest(request);
                    }
                }
                if (response != null) {
                    response.setClientRequestId(request.getClientRequestId());
                    response.setObjectId(request.getObjectId());
                    sendRequestWithoutResponse(response);
                }
            });
        }
    }

    public int registerRemoteObject(RemoteHandlingObject obj) {
        int id = remoteObjectId.incrementAndGet();
        while (id == 0 || remoteObjects.containsKey(id)) // Prevent overflow issue with 0
            id = remoteObjectId.incrementAndGet();
        obj.setObjectId(id);
        remoteObjects.put(id, obj);
        return id;
    }

    public void deleteLocalObject(int objectId) {
        remoteObjects.remove(objectId);
    }
    
    public RemoteHandlingObject getLocalObject(int objectId) {
        return remoteObjects.get(objectId);
    }
}
