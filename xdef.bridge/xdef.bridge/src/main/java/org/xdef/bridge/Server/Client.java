package org.xdef.bridge.server;

import java.io.IOException;
import java.util.HashMap;
import java.util.Map;
import java.util.TreeMap;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.concurrent.locks.ReentrantLock;

import org.xdef.bridge.remoteObjects.RemoteHandlingObject;
import org.xdef.bridge.server.requests.ObjectlessRequestHandler;
import org.xdef.bridge.server.requests.RemoteCallException;
import org.xdef.bridge.server.requests.Request;
import org.xdef.bridge.server.requests.RequestWaiter;
import org.xdef.bridge.server.requests.Response;
import org.xdef.bridge.server.requests.ResponseException;

public abstract class Client {

    private int clientId;
    private int serverRequestId = 1;
    private int remoteObjectId = 1;
    private final Map<Integer, RequestWaiter> waitingRequests = new TreeMap<Integer, RequestWaiter>();
    private final Map<Integer, RemoteHandlingObject> remoteObjects = new HashMap<>();

    private final ExecutorService threadPool = Executors.newCachedThreadPool();

    private final ObjectlessRequestHandler objectlessRequestHandler;

    public Client(int clientId) {
        this.clientId = clientId;
        this.objectlessRequestHandler = new ObjectlessRequestHandler(this);
    }

    public int getClientId() {
        return clientId;
    }

    @Override
    protected void finalize() throws Throwable {
        threadPool.shutdown();
        super.finalize();
    }

    public abstract void disconnect();

    public abstract void listen() throws IOException;

    protected abstract void sendRequestData(Request request);

    private synchronized int getNextRequestId() {
        int id = serverRequestId;
        serverRequestId++;
        return id;
    }

    public void sendRequestWithoutResponse(Request request) {
        sendRequestData(request);
    }

    public Request sendRequestWithResponse(Request request) {
        int id = getNextRequestId();
        request.setServerRequestId(id);
        RequestWaiter waiter = new RequestWaiter(id);
        addWaitingRequest(waiter);
        sendRequestData(request);
        try {
            waiter.getSemaphore().acquire();
        } catch (InterruptedException e) {
            // Not expected to fail, do nothing and return null
        }
        if (ResponseException.isException(request)) {
            RemoteCallException ex = ResponseException.getException(request);
            System.err.println("Remote call exception thrown code: " + ex.getErrorCode() + " message: " + ex.getMessage());
        }
        return waiter.getResponse();
    }

    protected void handleRequest(Request request) {
        if (waitingRequests.containsKey(request.getServerRequestId())) {
            RequestWaiter waiter = waitingRequests.get(request.getServerRequestId());
            waiter.setResponse(request);
            removeWaitingRequest(waiter);
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

    public synchronized int registerRemoteObject(RemoteHandlingObject obj) {
        obj.setObjectId(remoteObjectId);
        remoteObjectId++;
        remoteObjects.put(obj.getObjectId(), obj);
        return obj.getObjectId();
    }

    public synchronized void deleteLocalObject(int objectId) {
        remoteObjects.remove(objectId);
    }

    private synchronized void addWaitingRequest(RequestWaiter waiter) {
        waitingRequests.put(waiter.getRequestId(), waiter);
    }

    private synchronized void removeWaitingRequest(RequestWaiter waiter) {
        waitingRequests.remove(waiter.getRequestId());
    }
    
    public RemoteHandlingObject getLocalObject(int objectId) {
        return remoteObjects.get(objectId);
    }
}
