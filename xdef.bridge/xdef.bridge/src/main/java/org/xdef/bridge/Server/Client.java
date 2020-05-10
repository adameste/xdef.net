package org.xdef.bridge.server;

import java.io.IOException;
import java.util.HashMap;
import java.util.Map;
import java.util.TreeMap;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.concurrent.locks.ReentrantLock;

import org.xdef.bridge.remoteObjects.RemoteObject;
import org.xdef.bridge.remoteObjects.RemoteObjectFactory;
import org.xdef.bridge.server.requests.Request;
import org.xdef.bridge.server.requests.RequestWaiter;
import org.xdef.bridge.utils.BinaryDataBuilder;

public abstract class Client {

    private int clientId;
    private int serverRequestId = 1;
    private int remoteObjectId = 1;
    private final Map<Integer, RequestWaiter> waitingRequests = new TreeMap<Integer, RequestWaiter>();
    private final Map<Integer, RemoteObject> remoteObjects = new HashMap<>();

    private ExecutorService threadPool = Executors.newCachedThreadPool();
    private ReentrantLock sendLock = new ReentrantLock();

    private static final int FUNCTION_CREATE_OBJECT = 1;

    public Client(int clientId) {
        this.clientId = clientId;
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
        var waiter = new RequestWaiter(serverRequestId);
        waitingRequests.put(serverRequestId, waiter);
        sendRequestWithoutResponse(request);
        try {
            waiter.getSemaphore().acquire();
        } catch (InterruptedException e) {
            // Not expected to fail, do nothing and return null
        }
        return waiter.getResponse();
    }

    protected void handleRequest(Request request) {
        threadPool.submit(() -> {
            Request response = null;
            if (waitingRequests.containsKey(request.getServerRequestId())) {
                var waiter = waitingRequests.get(request.getServerRequestId());
                waiter.setResponse(request);
                waitingRequests.remove(request.getServerRequestId());
                waiter.getSemaphore().release();
            } else if (request.getObjectId() == 0) {
                response = handleObjectlessRequest(request);
            } else {
                var obj = remoteObjects.get(request.getObjectId());
                if (obj == null)
                    response = sendError();
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

    public int registerRemoteObject(RemoteObject obj) {
        obj.setObjectId(remoteObjectId++);
        remoteObjects.put(obj.getObjectId(), obj);
        return obj.getObjectId();

    }

    private Request sendError() {
        return null;
    }

    private Request handleObjectlessRequest(Request request) {
        switch (request.getFunction()) {
            case FUNCTION_CREATE_OBJECT:
                return createObject(request);
            default:
                return null;
        }
    }

    private Request createObject(Request request) {
        var remoteObjectFactory = new RemoteObjectFactory(this);
        var obj = remoteObjectFactory.createObject(request);
        registerRemoteObject(obj);
        var builder = new BinaryDataBuilder();
        builder.add(obj.getObjectId());
        var response = new Request(FUNCTION_CREATE_OBJECT, builder.build());
        return response;
    }

}
