package org.xdef.bridge.server;

import java.io.IOException;
import java.rmi.RemoteException;
import java.util.HashMap;
import java.util.Map;
import java.util.TreeMap;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.concurrent.locks.ReentrantLock;
import jdk.internal.jline.internal.Log;

import org.xdef.bridge.remoteObjects.RemoteHandlingObject;
import org.xdef.bridge.remoteObjects.RemoteObject;
import org.xdef.bridge.remoteObjects.RemoteObjectFactory;
import org.xdef.bridge.server.requests.ObjectlessRequestHandler;
import org.xdef.bridge.server.requests.RemoteCallException;
import org.xdef.bridge.server.requests.Request;
import org.xdef.bridge.server.requests.RequestWaiter;
import org.xdef.bridge.server.requests.Response;
import org.xdef.bridge.server.requests.ResponseException;
import org.xdef.bridge.utils.BinaryDataBuilder;
import sun.security.util.Debug;

public abstract class Client {

    private int clientId;
    private int serverRequestId = 1;
    private int remoteObjectId = 1;
    private final Map<Integer, RequestWaiter> waitingRequests = new TreeMap<Integer, RequestWaiter>();
    private final Map<Integer, RemoteHandlingObject> remoteObjects = new HashMap<>();

    private final ExecutorService threadPool = Executors.newCachedThreadPool();
    private final ReentrantLock sendLock = new ReentrantLock();
    private final ObjectlessRequestHandler objectlessRequestHandler;

    

    public Client(int clientId) {
        this.clientId = clientId;
        this.objectlessRequestHandler = new ObjectlessRequestHandler(this);
    }

    public int getClientId() {
        return clientId;
    }

    public abstract void disconnect();

    public abstract void listen() throws IOException;

    protected abstract void sendRequestData(Request request);

    public void sendRequestWithoutResponse(Request request) {
        sendLock.lock();
        request.setServerRequestId(serverRequestId++);
        sendRequestData(request);
        sendLock.unlock();
    }

    public Request sendRequestWithResponse(Request request) {
        RequestWaiter waiter = new RequestWaiter(serverRequestId);
        waitingRequests.put(serverRequestId, waiter);
        sendRequestWithoutResponse(request);
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
        threadPool.submit(() -> {
            Response response = null;
            if (waitingRequests.containsKey(request.getServerRequestId())) {
                RequestWaiter waiter = waitingRequests.get(request.getServerRequestId());
                waiter.setResponse(request);
                waitingRequests.remove(request.getServerRequestId());
                waiter.getSemaphore().release();
            } else if (request.getObjectId() == 0) {
                response = objectlessRequestHandler.handleRequest(request);
            } else {
                RemoteHandlingObject obj = remoteObjects.get(request.getObjectId());
                if (obj == null)
                    response = new ResponseException(ResponseException.ERROR_CODE_UNKNOWN_OBJECT, "Unknown remote object.");
                else
                    response = obj.handleRequest(request);
            }
            if (response != null) {
                response.setClientRequestId(request.getClientRequestId());
                response.setObjectId(request.getObjectId());
                sendRequestWithoutResponse(response);
            }
        });
    }

    public int registerRemoteObject(RemoteHandlingObject obj) {
        obj.setObjectId(remoteObjectId);
        remoteObjectId++;
        remoteObjects.put(obj.getObjectId(), obj);
        return obj.getObjectId();
    }
    
    public void deleteLocalObject(int objectId) {
        remoteObjects.remove(objectId);
    }
}
